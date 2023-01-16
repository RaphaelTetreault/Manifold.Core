using System.Text;

namespace Manifold.IO
{
    /// <summary>
    /// Simple wrapper class for string to encode and decode a C-style null-terminated string.
    /// The inheritor must define the encoding used by the string.
    /// </summary>
    [Serializable]
    public abstract class CString :
        IBinaryAddressable,
        IBinarySerializable,
        IEquatable<CString>
    {
        // CONSTANTS
        public const byte nullTerminator = 0x00;

        // FIELDS
        private static readonly List<byte> buffer = new List<byte>(64);
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


        // METHODS
        public static string ReadCString(EndianBinaryReader reader, Encoding encoding)
        {
            buffer.Clear();
            while (!reader.IsAtEndOfStream())
            {
                var @byte = reader.ReadByte();

                // If a null character is read, stop
                if (@byte is nullTerminator)
                    break;

                buffer.Add(@byte);
            }
            var str = encoding.GetString(buffer.ToArray());
            return str;
        }

        public static void WriteCString(EndianBinaryWriter writer, string value, Encoding encoding)
        {
            writer.Write(value, encoding, false);
            writer.Write(nullTerminator);
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


        public static implicit operator string(CString cstr) => cstr.Value;

        //public static implicit operator CString(string str) => str;
        public sealed override string ToString() => value;

        public bool Equals(CString? other)
        {
            if (other is null)
                return false;

            // Compares strings
            bool isSameValue = Value == other.Value;
            return isSameValue;
        }
    }
}
