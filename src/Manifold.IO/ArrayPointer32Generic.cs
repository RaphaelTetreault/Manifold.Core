namespace Manifold.IO
{
    public class ArrayPointer32<TBinarySerializable> :
        IBinarySerializable,
        IPointer
        where TBinarySerializable : IBinarySerializable, new()
    {
        // FIELDS
        private ArrayPointer arrayPointer;
        private TBinarySerializable[] array;

        // PROPERTIES
        public int Address => arrayPointer.address;
        public ArrayPointer ArrayPtr => arrayPointer;
        public bool IsNotNull => arrayPointer.IsNotNull;
        public bool IsNull => arrayPointer.IsNull;
        public int Length => arrayPointer.length;
        public TBinarySerializable[] Array
        {
            get => array;
            set => array = value;
        }

        // INDEXERS
        public TBinarySerializable this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        // OPERATORS
        public static implicit operator ArrayPointer(ArrayPointer32<TBinarySerializable> arrayPointer32Generic)
        {
            return arrayPointer32Generic.arrayPointer;
        }
        public static implicit operator TBinarySerializable[](ArrayPointer32<TBinarySerializable> pointer32Generic)
        {
            return pointer32Generic.array;
        }
        public static implicit operator ArrayPointer32<TBinarySerializable>(ArrayPointer arrayPointer)
        {
            var newValue = new ArrayPointer32<TBinarySerializable>(arrayPointer.address, arrayPointer.length);
            return newValue;
        }
        public static implicit operator ArrayPointer32<TBinarySerializable>(TBinarySerializable value)
        {
            var newValue = new ArrayPointer32<TBinarySerializable>(value);
            return newValue;
        }

        // CONSTRUCTORS
        public ArrayPointer32()
        {
            array = System.Array.Empty<TBinarySerializable>();
        }
        public ArrayPointer32(ArrayPointer arrayPointer) : this()
        {
            this.arrayPointer = arrayPointer;
        }
        public ArrayPointer32(int address, int length) : this(new ArrayPointer(length, address))
        {

        }
        public ArrayPointer32(params TBinarySerializable[] values)
        {
            arrayPointer = new ArrayPointer(values.Length, Pointer.Null);
            array = values;
        }

        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref arrayPointer);
        }

        public void DeserializeArray(EndianBinaryReader reader)
        {
            array = GetDeserializedArray(reader);

            //// Get address to return to after deserializing
            //Pointer currentAddress = reader.GetPositionAsPointer();

            //// Jump to address and deserialize values
            //reader.JumpToAddress(arrayPointer);
            //reader.Read(ref array, arrayPointer.length);

            //// Return to initial address
            //reader.JumpToAddress(currentAddress);
        }

        public TBinarySerializable[] GetDeserializedArray(EndianBinaryReader reader)
        {
            // Get address to return to after deserializing
            Pointer currentAddress = reader.GetPositionAsPointer();

            // Jump to address and deserialize values
            reader.JumpToAddress(arrayPointer);
            var localArray = System.Array.Empty<TBinarySerializable>();
            reader.Read(ref localArray, arrayPointer.length);

            // Return to initial address
            reader.JumpToAddress(currentAddress);

            // return array
            return localArray;
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(arrayPointer);
        }

        public void SerializeArray(EndianBinaryWriter writer)
        {
            // Ensure correct base address and length is stored
            arrayPointer.address = writer.GetPositionAsPointer();
            arrayPointer.length = array.Length;

            // Write all values
            writer.Write(array);
        }

    }
}
