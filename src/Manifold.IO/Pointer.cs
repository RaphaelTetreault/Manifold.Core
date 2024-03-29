using System;

namespace Manifold.IO
{
    [Serializable]
    public struct Pointer :
        IBinarySerializable,
        IEquatable<Pointer>,
        IPointer
    {
        // CONSTANTS
        public static readonly Pointer Null = 0;

        // FIELDS
        public int address;

        // CONSTRUCTORS
        public Pointer(int address)
        {
            this.address = address;
        }
        public Pointer(long address)
        {
            this.address = (int)address;
        }

        // PROPERTIES
        int IPointer.Address => address;
        public string PrintAddress => $"{address:x8}";
        public bool IsNotNull => address != 0;
        public bool IsNull => address == 0;



        // OPERATORS
        public static implicit operator int(Pointer pointer)
        {
            return pointer.address;
        }

        public static explicit operator uint(Pointer pointer)
        {
            return (uint)pointer.address;
        }

        public static implicit operator Pointer(int address)
        {
            return new Pointer(address);
        }

        public static implicit operator Pointer(long address)
        {
            return new Pointer((int)address);
        }

        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref address);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(address);
        }

        public override string ToString()
        {
            return $"Pointer({PrintAddress})";
        }

        public override bool Equals(object obj)
        {
            return Equals((Pointer)obj);
        }

        public bool Equals(Pointer obj)
        {
            return obj.address == address;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
