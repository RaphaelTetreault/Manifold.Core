//using System;
//using System.IO;
//using System.Text;

//namespace Manifold.IO
//{
//    public class PlainTextReaderDeprecated : StreamReader
//    {
//        public PlainTextReaderDeprecated(Stream stream) : base(stream)
//        {
//        }

//        public PlainTextReaderDeprecated(string path) : base(path)
//        {
//        }

//        public PlainTextReaderDeprecated(Stream stream, bool detectEncodingFromByteOrderMarks) : base(stream, detectEncodingFromByteOrderMarks)
//        {
//        }

//        public PlainTextReaderDeprecated(Stream stream, Encoding encoding) : base(stream, encoding)
//        {
//        }

//        public PlainTextReaderDeprecated(string path, bool detectEncodingFromByteOrderMarks) : base(path, detectEncodingFromByteOrderMarks)
//        {
//        }

//        public PlainTextReaderDeprecated(string path, FileStreamOptions options) : base(path, options)
//        {
//        }

//        public PlainTextReaderDeprecated(string path, Encoding encoding) : base(path, encoding)
//        {
//        }

//        public PlainTextReaderDeprecated(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : base(stream, encoding, detectEncodingFromByteOrderMarks)
//        {
//        }

//        public PlainTextReaderDeprecated(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks) : base(path, encoding, detectEncodingFromByteOrderMarks)
//        {
//        }

//        public PlainTextReaderDeprecated(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
//        {
//        }

//        public PlainTextReaderDeprecated(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) : base(path, encoding, detectEncodingFromByteOrderMarks, bufferSize)
//        {
//        }

//        public PlainTextReaderDeprecated(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, FileStreamOptions options) : base(path, encoding, detectEncodingFromByteOrderMarks, options)
//        {
//        }

//        public PlainTextReaderDeprecated(Stream stream, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = -1, bool leaveOpen = false) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen)
//        {
//        }

//        public long TextPosition { get; private set; }

//        public void SetCachedStreamPosition(long position)
//        {
//            BaseStream.Seek(position, SeekOrigin.Begin);
//            DiscardBufferedData();
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns>
//        ///     
//        /// </returns>
//        /// <remarks>
//        ///     If true, stream is positioned after marker.
//        ///     If false, stream position is unchanged.
//        /// </remarks>
//        public bool IsArrayEndMarker()
//        {
//            long position = BaseStream.Position;
//            string line = ReadLineIndent();
//            string marker = PlainTextWriter.ArrayMarker;

//            if (line.Length < marker.Length)
//            {
//                SetCachedStreamPosition(position);
//                return false;
//            }

//            bool isArrayMarker = line.Substring(0, marker.Length) == marker;
//            if (!isArrayMarker)
//            {
//                SetCachedStreamPosition(position);
//            }
//            return isArrayMarker;
//        }

//        public string ReadLineIndent()
//        {
//            // Throw is already at end of file
//            if (BaseStream.IsAtEndOfStream())
//            {
//                string msg = "Cannot read since stream is at end of file.";
//                throw new EndOfStreamException(msg);
//            }

//            // Read Next valid line if able
//            long currentPosition = BaseStream.Position;
//            while (currentPosition < BaseStream.Length)
//            {
//                // Read line
//                string line = ReadLine()!; // should not be !

//                /////////////////////////////////
//                // TODO: move reset to peek?
//                /////////////////////////////////

//                currentPosition += line.Length + 1; // +1 for newline
//                SetCachedStreamPosition(currentPosition);

//                // Skip empty
//                if (string.IsNullOrWhiteSpace(line))
//                    continue;

//                // Clean whitespace
//                line = line.Trim();

//                // Skip comments
//                if (line[0] == '#')
//                    continue;


//                // String processed, return
//                return line;
//            }

//            // Indicate no valid line could be read
//            {
//                string msg = "Could not find another line to read.";
//                throw new System.IO.EndOfStreamException(msg);
//            }
//        }
//        public string ReadValue()
//        {
//            string line = ReadLineIndent();
//            string[] segments = line.Split(':');
//            string valueText = segments[^1];
//            valueText = valueText.Trim();
//            return valueText;
//        }
//        public void ReadValue<T>(ref T value, Func<string, T> parse)
//        {
//            string valueText = ReadValue();
//            value = parse.Invoke(valueText);
//        }
//        public void ReadValue(ref string value)
//        {
//            value = ReadValue();
//        }
//        public void ReadValue(ref byte value)
//            => ReadValue(ref value, byte.Parse);
//        public void ReadValue(ref ushort value)
//            => ReadValue(ref value, ushort.Parse);
//        public void ReadValue(ref uint value)
//            => ReadValue(ref value, uint.Parse);
//        public void ReadValue(ref ulong value)
//            => ReadValue(ref value, ulong.Parse);
//        public void ReadValue(ref sbyte value)
//            => ReadValue(ref value, sbyte.Parse);
//        public void ReadValue(ref short value)
//            => ReadValue(ref value, short.Parse);
//        public void ReadValue(ref int value)
//            => ReadValue(ref value, int.Parse);
//        public void ReadValue(ref long value)
//            => ReadValue(ref value, long.Parse);
//        public void ReadValue(ref float value)
//            => ReadValue(ref value, float.Parse);
//        public void ReadValue(ref double value)
//            => ReadValue(ref value, double.Parse);
//        public void ReadValue<TEnum>(ref TEnum value, byte _ = 0)
//            where TEnum : struct, Enum
//        {
//            TEnum ParseEnum(string str)
//            {
//                return Enum.Parse<TEnum>(str, true);
//            };
//            ReadValue(ref value, ParseEnum);

//        }
//        public void ReadValue<TPlainTextSerializable>(ref TPlainTextSerializable value)
//            where TPlainTextSerializable : IPlainTextSerializable, new()
//        {
//            TPlainTextSerializable ParsePlainTextSerializable(string str)
//            {
//                var textSerializable = new TPlainTextSerializable();
//                //textSerializable.Deserialize(this);
//                return textSerializable;
//            };
//            ReadValue(ref value, ParsePlainTextSerializable);
//        }

//        public void SerializeIndent<TPlainTextSerializable>(TPlainTextSerializable serializable)
//            where TPlainTextSerializable : IPlainTextSerializable
//        {
//            //serializable.Deserialize(this);
//        }
//    }
//}
