using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
        IEquatable<CString>,
        IComparable<CString>
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
        private static string ReadCString(EndianBinaryReader reader, Encoding encoding)
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

        private static void WriteCString(EndianBinaryWriter writer, string value, Encoding encoding)
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

        public int GetSerializedLength()
        {
            byte[] bytes = Encoding.GetBytes(value);
            int length = bytes.Length + 1; // +1 for null
            return length;
        }

        public sealed override string ToString()
            => value;

        public bool Equals(CString? other)
        {
            if (other is null)
                return false;

            // Compares strings
            bool isSameValue = Value == other.Value;
            return isSameValue;
        }

        private static List<byte> CreateByteBuffer()
            => new List<byte>(ByteBufferDefaultSize);

        public int CompareTo(CString? other)
        {
            return string.Compare(this, other);
        }

        public bool IsEquivilentTo(CString other)
        {
            bool isEquivilent = CompareTo(other) == 0;
            return isEquivilent;
        }

        public static void MergeReferences<TCString>(ref TCString[] strings)
            where TCString : CString
        {
            // For loops written such that:
            //  Value A does not compare against A
            //  Values B and A are evaluated
            //  Values A and B are NOT evaluated

            for (int i = 0; i < strings.Length; i++)
            {
                ref TCString a = ref strings[i];
                
                for (int j = 0; j <= i; j++)
                {
                    // skip comparing self
                    if (i == j)
                        continue;

                    ref TCString b = ref strings[j];

                    // Mutate reference such that B points to A
                    if (b.IsEquivilentTo(a))
                    {
                        b = a;
                    }
                }
            }
        }
    }
}
