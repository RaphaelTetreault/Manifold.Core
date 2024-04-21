namespace Manifold.IO
{
    /// <summary>
    ///     
    /// </summary>
    /// <remarks>
    ///     Type is meant to replace older ArrayPointer.
    /// </remarks>
    public struct ArrayPointer32 :
        IBinarySerializable,
        IPointer
    {
        // FIELDS
        public int length;
        public int address;


        // CONSTRUCTORS
        public ArrayPointer32()
        {

        }
        public ArrayPointer32(int address, int length)
        {
            this.address = address;
            this.length = length;
        }


        // PROPERTIES
        readonly int IPointer.Address => address;
        public readonly bool IsNotNull => address != 0;
        public readonly bool IsNull => address == 0;
        public readonly Pointer Pointer => new(address);
        public readonly string PrintAddress => $"{address:x8}";


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref length);
            reader.Read(ref address);
        }

        public readonly void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(length);
            writer.Write(address);
        }
        public override readonly string ToString()
        {
            return $"Address: {PrintAddress}, Length: {length}, ";
        }


        // OPERATORS
        public static bool operator ==(ArrayPointer32 lhs, ArrayPointer32 rhs)
        {
            return lhs.address == rhs.address && lhs.length == rhs.length;
        }

        public static bool operator !=(ArrayPointer32 lhs, ArrayPointer32 rhs)
        {
            return lhs.address != rhs.address || lhs.length != rhs.length;
        }

        public override readonly bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(ArrayPointer32))
                return false;

            bool isEqual = (ArrayPointer32)obj == this;
            return isEqual;
        }

        public override readonly int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
