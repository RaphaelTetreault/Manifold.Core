using System.Text;

namespace Manifold.IO
{
    /// <summary>
    ///     Simple wrapper class for string to encode and decode a C-style null-terminated string.
    /// </summary>
    /// <remarks>
    ///     The inheritor must define the encoding used by the string.
    /// </remarks>
    [Serializable]
    public abstract class CString :
        IBinaryAddressable,
        IBinarySerializable,
        IEquatable<CString>
    {
        // CONSTANTS
        public const byte NullTerminator = 0x00;
        private const int ByteBufferDefaultSize = 1024;

        // FIELDS
        private static readonly ThreadLocal<List<byte>> readBuffer = new ThreadLocal<List<byte>>(CreateByteBuffer);
        private string value;

        // CONSTRUCTORS
        public CString()
        {
            value = string.Empty;
        }

        public CString(string value)
        {
            this.value = value;
        }


        // PROPERTIES
        public AddressRange AddressRange { get; set; }
        public int Length => Value.Length;
        public abstract Encoding Encoding { get; }
        public string Value { get => value; set => this.value = value; }


        // OPERATORS
        public static implicit operator string(CString cstr) => cstr.Value;


        // METHODS        
        public static string ReadCString(EndianBinaryReader reader, Encoding encoding)
        {
            readBuffer.Value.Clear();
            while (!reader.IsAtEndOfStream())
            {
                var @byte = reader.ReadByte();

                // If a null character is read, stop
                if (@byte is NullTerminator)
                    break;

                readBuffer.Value.Add(@byte);
            }
            var str = encoding.GetString(readBuffer.Value.ToArray());
            return str;
        }

        public static void WriteCString(EndianBinaryWriter writer, string value, Encoding encoding)
        {
            writer.Write(value, encoding, false);
            writer.Write(NullTerminator);
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            this.RecordStartAddress(reader);
            {
                Value = ReadCString(reader, Encoding);
            }
            this.RecordEndAddress(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            this.RecordStartAddress(writer);
            {
                WriteCString(writer, Value, Encoding);
            }
            this.RecordEndAddress(writer);
        }

        public sealed override string ToString() => value;

        public bool Equals(CString? other)
        {
            if (other is null)
                return false;

            // Compares strings
            bool isSameValue = Value == other.Value;
            return isSameValue;
        }

        private static List<byte> CreateByteBuffer() => new List<byte>(ByteBufferDefaultSize);
    }
}
