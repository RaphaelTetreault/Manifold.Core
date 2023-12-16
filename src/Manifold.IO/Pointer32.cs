namespace Manifold.IO
{
    public sealed class Pointer32<TBinarySerializable> :
        IBinarySerializable,
        IPointer
        where TBinarySerializable : IBinarySerializable, new()
    {
        // FIELDS
        private Pointer pointer;
        private TBinarySerializable value;

        // PROPERTIES
        public int Address => pointer.address;
        public bool IsNotNull => pointer.IsNotNull;
        public bool IsNull => pointer.IsNull;
        public bool IsValueNull => value == null;
        public bool IsValueNotNull => value != null;
        public TBinarySerializable Value
        {
            get => value;
            set => this.value = value;
        }

        // OPERATORS
        public static implicit operator Pointer(Pointer32<TBinarySerializable> pointer32Generic)
        {
            return pointer32Generic.pointer;
        }
        public static implicit operator TBinarySerializable(Pointer32<TBinarySerializable> pointer32Generic)
        {
            return pointer32Generic.value;
        }
        public static implicit operator Pointer32<TBinarySerializable>(Pointer address)
        {
            var newValue = new Pointer32<TBinarySerializable>(address);
            return newValue;
        }
        public static implicit operator Pointer32<TBinarySerializable>(TBinarySerializable value)
        {
            var newValue = new Pointer32<TBinarySerializable>(value);
            return newValue;
        }

        // CONSTRUCTORS
        public Pointer32()
        {
            value = new TBinarySerializable();
        }
        public Pointer32(Pointer pointer) : this()
        {
            this.pointer = pointer;
        }
        public Pointer32(TBinarySerializable value)
        {
            this.value = value;
        }

        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref pointer);
        }

        public void DeserializeValue(EndianBinaryReader reader)
        {
            // Get address to return to after deserializing
            Pointer currentAddress = reader.GetPositionAsPointer();

            // Jump to address and deserialize values
            reader.JumpToAddress(pointer);
            reader.Read(ref value);

            // Return to initial address
            reader.JumpToAddress(currentAddress);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(pointer);
        }

        public void SerializeValue(EndianBinaryWriter writer)
        {
            // Ensure correct base address and length is stored
            pointer.address = writer.GetPositionAsPointer();

            // Write values
            writer.Write(value);
        }
    }
}
