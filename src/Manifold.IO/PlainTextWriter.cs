using System;
using System.IO;
using System.Text;

namespace Manifold.IO
{
    public class PlainTextWriter : StreamWriter
    {
        public static readonly string ArrayMarker = "$ ARRAY_END";

        public int IndentLevel { get; set; }

        public PlainTextWriter(Stream stream) : base(stream)
        {
        }

        public PlainTextWriter(string path) : base(path)
        {
        }

        public PlainTextWriter(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        public PlainTextWriter(string path, bool append) : base(path, append)
        {
        }

        public PlainTextWriter(string path, FileStreamOptions options) : base(path, options)
        {
        }

        public PlainTextWriter(Stream stream, Encoding encoding, int bufferSize) : base(stream, encoding, bufferSize)
        {
        }

        public PlainTextWriter(string path, bool append, Encoding encoding) : base(path, append, encoding)
        {
        }

        public PlainTextWriter(string path, Encoding encoding, FileStreamOptions options) : base(path, encoding, options)
        {
        }

        public PlainTextWriter(Stream stream, Encoding? encoding = null, int bufferSize = -1, bool leaveOpen = false) : base(stream, encoding, bufferSize, leaveOpen)
        {
        }

        public PlainTextWriter(string path, bool append, Encoding encoding, int bufferSize) : base(path, append, encoding, bufferSize)
        {
        }


        public void IncrementIndent()
        {
            IndentLevel++;
        }
        public void DecrementIndent()
        {
            if (IndentLevel > 0)
                IndentLevel--;
        }
        public void ResetIndent()
        {
            IndentLevel = 0;
        }

        public void WriteIndents()
        {
            for (int i = 0; i < IndentLevel; i++)
            {
                Write('\t');
            }
        }

        public void WriteLineIndent(string value)
        {
            WriteIndents();
            WriteLine(value);
        }
        public void WriteLineIndent(object value)
            => WriteLineIndent(value.ToString()!);
        public void WriteLineIndent()
            => WriteIndents();

        public void WriteLineValue(string name, string? value)
        {
            if (value == null && value!.ToString() == null)
                throw new ArgumentNullException(nameof(value));

            WriteIndents();
            Write(name);
            Write(": ");
            Write(value);
            Write('\n');
        }
        public void WriteLineValue(string name, object value)
            => WriteLineValue(name, value.ToString());

        public void WriteLineComment(string value)
        {
            WriteIndents();
            Write("# ");
            WriteLine(value);
        }
        public void WriteLineComment(object value)
        {
            if (value == null && value!.ToString() == null)
                throw new ArgumentNullException(nameof(value));

            WriteLineComment(value.ToString()!);
        }
        public void WriteLineComment()
        {
            WriteLineComment(string.Empty);
        }

        public void WriteLineArrayEnd()
        {
            WriteLineIndent(ArrayMarker);
        }

        public void SerializeIndent<TPlainTextSerializable>(TPlainTextSerializable serializable)
            where TPlainTextSerializable : IPlainTextSerializable
        {
            IncrementIndent();
            serializable.Serialize(this);
            DecrementIndent();
        }
    }
}
