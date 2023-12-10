using System;
using System.Collections.Generic;
using System.IO;

namespace Manifold.IO
{
    public class PlainTextReader : IDisposable
    {
        public int CurrentChar { get; private set; }
        public int CurrentLine { get; private set; }
        public int CharCount => Lines[CurrentLine].Length;
        public int LineCount => Lines.Length;
        public string[] Lines { get; }


        public PlainTextReader(string path)
        {
            Lines = File.ReadAllLines(path);
        }
        public PlainTextReader(Stream stream, int listCapacity = 256)
        {
            List<string> lines = new(listCapacity);
            using StreamReader reader = new(stream);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                lines.Add(line);
            }
            Lines = lines.ToArray();
        }

        public void SetChar(int charIndex)
        {
            if (charIndex < 0)
                CurrentChar = 0;
        }
        public void SetLine(int lineIndex)
        {
            if (lineIndex < 0)
                CurrentLine = 0;

            CurrentLine = lineIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     
        /// </returns>
        /// <remarks>
        ///     If true, stream is positioned after marker.
        ///     If false, stream position is unchanged.
        /// </remarks>
        public bool IsArrayEndMarker()
        {
            int startLine = CurrentLine;
            string line = ReadLineIndented();
            string marker = PlainTextWriter.ArrayMarker;

            if (line.Length < marker.Length)
            {
                CurrentLine = startLine;
                return false;
            }

            bool isArrayMarker = line.Substring(0, marker.Length) == marker;
            if (!isArrayMarker)
            {
                CurrentLine = startLine;
            }
            return isArrayMarker;
        }


        public char Read()
        {
            string line = Lines[CurrentLine];

            if (CurrentChar > line.Length)
                return '\0'; // null character

            char c = line[CurrentChar];
            CurrentChar++;

            return c;
        }
        public string ReadLine()
        {
            if (CurrentLine >= LineCount)
            {
                string msg = "At end of file.";
                throw new EndOfStreamException(msg);
            }

            string line = Lines[CurrentLine];
            CurrentLine++;

            return line;
        }
        public string ReadLineIndented()
        {
            while (CurrentLine < LineCount)
            {
                string line = ReadLine();

                // Skip empty
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Clean whitespace
                line = line.Trim();

                // Skip comments
                if (line[0] == '#')
                    continue;

                // String processed, return
                return line;
            }

            // Indicate no valid line could be read
            {
                string msg = "No value-based line remaining in file.";
                throw new EndOfStreamException(msg);
            }
        }

        public string ReadValue()
        {
            string line = ReadLineIndented();
            string[] segments = line.Split(':');
            string valueText = segments[^1];
            valueText = valueText.Trim();
            return valueText;
        }
        public void ReadValue<T>(ref T value, Func<string, T> parse)
        {
            string valueText = ReadValue();
            value = parse.Invoke(valueText);
        }
        public void ReadValue(ref string value)
        {
            value = ReadValue();
        }
        public void ReadValue(ref byte value)
            => ReadValue(ref value, byte.Parse);
        public void ReadValue(ref ushort value)
            => ReadValue(ref value, ushort.Parse);
        public void ReadValue(ref uint value)
            => ReadValue(ref value, uint.Parse);
        public void ReadValue(ref ulong value)
            => ReadValue(ref value, ulong.Parse);
        public void ReadValue(ref sbyte value)
            => ReadValue(ref value, sbyte.Parse);
        public void ReadValue(ref short value)
            => ReadValue(ref value, short.Parse);
        public void ReadValue(ref int value)
            => ReadValue(ref value, int.Parse);
        public void ReadValue(ref long value)
            => ReadValue(ref value, long.Parse);
        public void ReadValue(ref float value)
            => ReadValue(ref value, float.Parse);
        public void ReadValue(ref double value)
            => ReadValue(ref value, double.Parse);
        public void ReadValue<TEnum>(ref TEnum value, byte _ = 0)
            where TEnum : struct, Enum
        {
            TEnum ParseEnum(string str)
            {
                return Enum.Parse<TEnum>(str, true);
            };
            ReadValue(ref value, ParseEnum);

        }
        public void ReadValue<TPlainTextSerializable>(ref TPlainTextSerializable value)
            where TPlainTextSerializable : IPlainTextSerializable, new()
        {
            TPlainTextSerializable ParsePlainTextSerializable(string str)
            {
                var textSerializable = new TPlainTextSerializable();
                textSerializable.Deserialize(this);
                return textSerializable;
            };
            ReadValue(ref value, ParsePlainTextSerializable);
        }
        public void DerializeIndented<TPlainTextSerializable>(TPlainTextSerializable serializable)
            where TPlainTextSerializable : IPlainTextSerializable
        {
            serializable.Deserialize(this);
        }

        public void Dispose()
        {
            // "Fake" implementation to stay consistent with Writer
        }
    }
}
