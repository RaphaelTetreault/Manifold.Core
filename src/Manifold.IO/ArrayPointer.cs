namespace Manifold.IO
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public struct ArrayPointer :
        IBinarySerializable,
        IPointer
    {
        // FIELDS
        public int length;
        public int address;


        // CONSTRUCTORS
        public ArrayPointer(int length = 0, int address = 0)
        {
            this.length = length;
            this.address = address;
        }


        // PROPERTIES
        int IPointer.Address => address;
        public bool IsNotNull => address != 0;
        public bool IsNull => address == 0;
        public Pointer Pointer => new(address);
        public string PrintAddress => $"{address:x8}";


        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref length);
            reader.Read(ref address);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(length);
            writer.Write(address);
        }
        public override string ToString()
        {
            return $"Length: {length}, Address: {PrintAddress}";
        }


        // OPERATORS
        public static bool operator ==(ArrayPointer lhs, ArrayPointer rhs)
        {
            return lhs.address == rhs.address && lhs.length == rhs.length;
        }

        public static bool operator !=(ArrayPointer lhs, ArrayPointer rhs)
        {
            return lhs.address != rhs.address || lhs.length != rhs.length;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ArrayPointer))
                return false;

            return (ArrayPointer)obj == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
