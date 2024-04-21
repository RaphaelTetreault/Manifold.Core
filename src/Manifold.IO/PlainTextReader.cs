using System;
using System.IO;
using System.Text;

namespace Manifold.IO
{
    public class PlainTextReader : StreamReader
    {
        public PlainTextReader(Stream stream) : base(stream)
        {
        }

        public PlainTextReader(string path) : base(path)
        {
        }

        public PlainTextReader(Stream stream, bool detectEncodingFromByteOrderMarks) : base(stream, detectEncodingFromByteOrderMarks)
        {
        }

        public PlainTextReader(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        public PlainTextReader(string path, bool detectEncodingFromByteOrderMarks) : base(path, detectEncodingFromByteOrderMarks)
        {
        }

        public PlainTextReader(string path, FileStreamOptions options) : base(path, options)
        {
        }

        public PlainTextReader(string path, Encoding encoding) : base(path, encoding)
        {
        }

        public PlainTextReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : base(stream, encoding, detectEncodingFromByteOrderMarks)
        {
        }

        public PlainTextReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks) : base(path, encoding, detectEncodingFromByteOrderMarks)
        {
        }

        public PlainTextReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
        {
        }

        public PlainTextReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) : base(path, encoding, detectEncodingFromByteOrderMarks, bufferSize)
        {
        }

        public PlainTextReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, FileStreamOptions options) : base(path, encoding, detectEncodingFromByteOrderMarks, options)
        {
        }

        public PlainTextReader(Stream stream, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = -1, bool leaveOpen = false) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>
        /// </returns>
        /// <remarks>
        ///     If true, stream is after marker.
        ///     If false, stream position is unchanged.
        /// </remarks>
        public bool IsArrayEndMarker()
        {
            var position = BaseStream.Position;
            string line = ReadLineIndent();

            const string marker = "$ ARRAY_END";
            if (line.Length < marker.Length)
            {
                BaseStream.Position = position;
                return false;
            }

            bool isArrayMarker = line.Substring(0, marker.Length) == marker;
            if (!isArrayMarker)
            {
                BaseStream.Position = position;
            }
            return isArrayMarker;
        }

        public string ReadLineIndent()
        {
            // Throw is already at end of file
            if (BaseStream.IsAtEndOfStream())
            {
                string msg = "Cannot read, stream at end of file.";
                throw new EndOfStreamException(msg);
            }

            // Read Next valid line if able
            long currentPosition = BaseStream.Position;
            while (!BaseStream.IsAtEndOfStream())
            {
                // Read line
                string line = ReadLine()!; // should not be !

                /////////////////////////////////
                // TODO: move reset to peek?
                /////////////////////////////////

                currentPosition += line.Length + 1;
                DiscardBufferedData();
                BaseStream.Seek(currentPosition, SeekOrigin.Begin);

                // Skip empty
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // clean whitespace
                line = line.Trim();

                // skip comments
                if (line[0] == '#')
                    continue;

                // String processed, return
                return line;
            }

            // Indicate no valid line could be read
            {
                string msg = "Could not find another line to read.";
                throw new System.IO.EndOfStreamException(msg);
            }
        }
        public string ReadLineValue()
        {
            string line = ReadLineIndent();
            string[] segments = line.Split(':');
            string valueText = segments[^1];
            valueText = valueText.Trim();
            return valueText;
        }
        public void ReadLineValue<T>(ref T value, Func<string, T> parse)
        {
            string valueText = ReadLineValue();
            value = parse.Invoke(valueText);
        }
        public void ReadLineValue(ref string value)
        {
            value = ReadLineValue();
        }
        public void ReadLineValue(ref byte value)
            => ReadLineValue(ref value, byte.Parse);
        public void ReadLineValue(ref ushort value)
            => ReadLineValue(ref value, ushort.Parse);
        public void ReadLineValue(ref uint value)
            => ReadLineValue(ref value, uint.Parse);
        public void ReadLineValue(ref ulong value)
            => ReadLineValue(ref value, ulong.Parse);
        public void ReadLineValue(ref sbyte value)
            => ReadLineValue(ref value, sbyte.Parse);
        public void ReadLineValue(ref short value)
            => ReadLineValue(ref value, short.Parse);
        public void ReadLineValue(ref int value)
            => ReadLineValue(ref value, int.Parse);
        public void ReadLineValue(ref long value)
            => ReadLineValue(ref value, long.Parse);
        public void ReadLineValue(ref float value)
            => ReadLineValue(ref value, float.Parse);
        public void ReadLineValue(ref double value)
            => ReadLineValue(ref value, double.Parse);
        public void ReadLineValue<TEnum>(ref TEnum value, byte _ = 0)
            where TEnum : struct, Enum
            => ReadLineValue(ref value,
                (string str) => { return Enum.Parse<TEnum>(str, true); }
            );
        public void ReadLineValue<TPlainTextSerializable>( ref TPlainTextSerializable value)
            where TPlainTextSerializable : IPlainTextSerializable, new()
            => ReadLineValue(ref value,
                (string str) =>
                {
                    var textSerializable = new TPlainTextSerializable();
                    textSerializable.Deserialize(this);
                    return textSerializable;
                }
            );

        public void SerializeIndent<TPlainTextSerializable>(TPlainTextSerializable serializable)
            where TPlainTextSerializable : IPlainTextSerializable
        {
            serializable.Deserialize(this);
        }
    }
}
