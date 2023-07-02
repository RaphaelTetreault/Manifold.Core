using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Manifold.IO
{
    public class EndianBinaryReader : BinaryReader
    {
        // CONSTANTS
        public const int SizeofBool = 1;
        public const int SizeofInt8 = 1;
        public const int SizeofInt16 = 2;
        public const int SizeofInt32 = 4;
        public const int SizeofInt64 = 8;
        public const int SizeofInt128 = 16;
        public const int SizeofUint8 = 1;
        public const int SizeofUint16 = 2;
        public const int SizeofUint32 = 4;
        public const int SizeofUint64 = 8;
        public const int SizeofUint128 = 16;
        public const int SizeofHalf = 2;
        public const int SizeofFloat = 4;
        public const int SizeofDouble = 8;
        public const int SizeofDecimal = 16;

        // FIELDS
        // Endianness
        private readonly Endianness endianness;
        private readonly Func<ushort> fReadUInt16;
        private readonly Func<uint> fReadUInt32;
        private readonly Func<ulong> fReadUInt64;
        private readonly Func<UInt128> fReadUInt128;
        private readonly Func<short> fReadInt16;
        private readonly Func<int> fReadInt32;
        private readonly Func<long> fReadInt64;
        private readonly Func<Int128> fReadInt128;
        private readonly Func<Half> fReadHalf;
        private readonly Func<float> fReadFloat;
        private readonly Func<double> fReadDouble;
        // Endian-less functions
        private readonly Func<byte> fReadByte;
        private readonly Func<int, byte[]> fReadBytes;
        private readonly Func<bool> fReadBool;
        private readonly Func<sbyte> fReadSByte;
        // TODO: ReadChar
        // TODO: ReadChars
        // TODO: ReadString


        // PROPERTIES
        /// <summary>
        /// The current endianness used for read operations
        /// </summary>
        public Endianness Endianness => endianness;

        /// <summary>
        /// Returns true if Endianness is Little Endian
        /// </summary>
        public bool IsLittleEndian => endianness == Endianness.LittleEndian;

        /// <summary>
        /// Returns true if Endianness is Big Endian
        /// </summary>
        public bool IsBigEndian => endianness == Endianness.BigEndian;


        // CONSTRUCTORS
        public EndianBinaryReader(Stream input, Endianness endianness) : this(input, endianness, Encoding.UTF8, false) { }
        public EndianBinaryReader(Stream input, Endianness endianness, Encoding encoding) : this(input, endianness, encoding, false) { }
        public EndianBinaryReader(Stream input, Endianness endianness, Encoding encoding, bool leaveOpen) : this(input, endianness, encoding, leaveOpen, Endianness.LittleEndian) { }
        public EndianBinaryReader(Stream input, Endianness endianness, Encoding encoding, bool leaveOpen, Endianness bitEndianess) : base(input, encoding, leaveOpen)
        {
            // Select the appropriate serialization functions, store function reference
            this.endianness = endianness;
            bool requiresSwapEndianness = BitConverter.IsLittleEndian ^ IsLittleEndian;
            fReadUInt16 = requiresSwapEndianness ? ReadUInt16SwapEndianness : ReadUInt16SameEndianness;
            fReadUInt32 = requiresSwapEndianness ? ReadUInt32SwapEndianness : ReadUInt32SameEndianness;
            fReadUInt64 = requiresSwapEndianness ? ReadUInt64SwapEndianness : ReadUInt64SameEndianness;
            fReadUInt128 = requiresSwapEndianness ? ReadUInt128SwapEndianness : ReadUInt128SameEndianness;
            fReadInt16 = requiresSwapEndianness ? ReadInt16SwapEndianness : ReadInt16SameEndianness;
            fReadInt32 = requiresSwapEndianness ? ReadInt32SwapEndianness : ReadInt32SameEndianness;
            fReadInt64 = requiresSwapEndianness ? ReadInt64SwapEndianness : ReadInt64SameEndianness;
            fReadInt128 = requiresSwapEndianness ? ReadInt128SwapEndianness : ReadInt128SameEndianness;
            fReadHalf = requiresSwapEndianness ? ReadHalfSwapEndianness : ReadHalfSameEndianness;
            fReadFloat = requiresSwapEndianness ? ReadFloatSwapEndianness : ReadFloatSameEndianness;
            fReadDouble = requiresSwapEndianness ? ReadDoubleSwapEndianness : ReadDoubleSameEndianness;
            //
            fReadBool = base.ReadBoolean;
            fReadByte = base.ReadByte;
            fReadSByte = base.ReadSByte;
            fReadBytes = base.ReadBytes;
            // Unmodified functions.
            // TODO: ReadChar
            // TODO: ReadChars
            // TODO: ReadString
        }

        // READ
        public override bool ReadBoolean() => fReadBool();
        public bool ReadBool() => fReadBool();
        public override byte ReadByte() => fReadByte();
        public override byte[] ReadBytes(int count) => fReadBytes(count);
        public byte ReadUInt8() => fReadByte();
        public override ushort ReadUInt16() => fReadUInt16.Invoke();
        public override uint ReadUInt32() => fReadUInt32.Invoke();
        public override ulong ReadUInt64() => fReadUInt64.Invoke();
        public UInt128 ReadUInt128() => fReadUInt128.Invoke();
        public override sbyte ReadSByte() => fReadSByte();
        public sbyte ReadInt8() => ReadSByte();
        public override short ReadInt16() => fReadInt16.Invoke();
        public override int Read() => fReadInt32.Invoke();
        public override int ReadInt32() => fReadInt32.Invoke();
        public override long ReadInt64() => fReadInt64.Invoke();
        public Int128 ReadInt128() => fReadInt128.Invoke();
        public override Half ReadHalf() => fReadHalf.Invoke();
        public override float ReadSingle() => fReadFloat.Invoke();
        public float ReadFloat() => fReadFloat.Invoke();
        public override double ReadDouble() => fReadDouble.Invoke();
        public override decimal ReadDecimal()
        {
            int[] ints = ReadInt32Array(4);
            decimal value = new decimal(ints);
            return value;
        }
        public override char ReadChar() => base.ReadChar();
        public override char[] ReadChars(int count) => base.ReadChars(count);
        public override string ReadString() => base.ReadString();
        public string ReadString(int lengthBytes, Encoding encoding)
        {
            byte[] bytes = ReadUInt8Array(lengthBytes);
            string value = encoding.GetString(bytes);
            return value;
        }
        public T ReadBinarySerializable<T>() where T : IBinarySerializable, new()
        {
            T value = new T();
            value.Deserialize(this);
            return value;
        }
        public TEnum ReadEnum<TEnum>() where TEnum : Enum
        {
            var typeCode = ((TEnum)Enum.ToObject(typeof(TEnum), 0)).GetTypeCode();
            switch (typeCode)
            {
                // Ordered by my best guess as to which is most common
                // int is the default backing type
                case TypeCode.Int32: return (TEnum)(object)ReadInt32();
                // I often override the backing type to not have negatives
                case TypeCode.UInt32: return (TEnum)(object)ReadUInt32();
                // byte and ushort are smaller/compress enums (also no negatives
                case TypeCode.Byte: return (TEnum)(object)ReadUInt8();
                case TypeCode.UInt16: return (TEnum)(object)ReadUInt16();
                // Unlikely but perhaps userful to have 64 bits to work with
                case TypeCode.UInt64: return (TEnum)(object)ReadUInt64();
                // These are unordered: I know I don't use them as backing types
                case TypeCode.SByte: return (TEnum)(object)ReadInt8();
                case TypeCode.Int16: return (TEnum)(object)ReadInt16();
                case TypeCode.Int64: return (TEnum)(object)ReadInt64();

                default: throw new NotImplementedException("Unsupported Enum backing type used!");
            }
        }
        /// <summary>
        /// Read <paramref name="count"/> bytes from base stream. All bytes will be reversed
        /// if the base stream is not the same endianness as the reader.
        /// </summary>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns></returns>
        public byte[] ReadBytesEndianness(int count)
        {
            var bytes = ReadBytes(count);
            if (BitConverter.IsLittleEndian ^ IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }


        // READ ARRAY
        public T[] ReadArray<T>(int length, Func<T> deserializeMethod)
        {
            T[] array = new T[length];
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = deserializeMethod();
            }
            return array;
        }


        public bool[] ReadBoolArray(int length) => ReadArray(length, ReadBool);
        public byte[] ReadUInt8Array(int length) => fReadBytes(length);
        public sbyte[] ReadInt8Array(int length) => ReadArray(length, ReadInt8);
        public short[] ReadInt16Array(int length) => ReadArray(length, ReadInt16);
        public ushort[] ReadUInt16Array(int length) => ReadArray(length, ReadUInt16);
        public int[] ReadInt32Array(int length) => ReadArray(length, ReadInt32);
        public uint[] ReadUInt32Array(int length) => ReadArray(length, ReadUInt32);
        public long[] ReadInt64Array(int length) => ReadArray(length, ReadInt64);
        public ulong[] ReadUInt64Array(int length) => ReadArray(length, ReadUInt64);
        public Int128[] ReadInt128Array(int length) => ReadArray(length, ReadInt128);
        public UInt128[] ReadUInt128Array(int length) => ReadArray(length, ReadUInt128);
        public Half[] ReadHalfArray(int length) => ReadArray(length, ReadHalf);
        public float[] ReadFloatArray(int length) => ReadArray(length, ReadFloat);
        public double[] ReadDoubleArray(int length) => ReadArray(length, ReadDouble);
        public decimal[] ReadDecimalArray(int length) => ReadArray(length, ReadDecimal);
        public string[] ReadStringArray(int length, Encoding encoding)
        {
            var array = new string[length];
            for (int i = 0; i < length; i++)
            {
                var lengthBytes = ReadInt32();
                array[i] = ReadString(lengthBytes, encoding);
            }
            return array;
        }
        public TBinarySerializable[] ReadBinarySerializableArray<TBinarySerializable>(int length)
            where TBinarySerializable : IBinarySerializable, new()
                => ReadArray(length, ReadBinarySerializable<TBinarySerializable>);
        public TEnum[] ReadEnumArray<TEnum>(int length)
            where TEnum : Enum
                => ReadArray(length, ReadEnum<TEnum>);

        // READ - REF
        public void Read(ref bool value) => value = ReadBoolean();
        public void Read(ref byte value) => value = ReadByte();
        public void Read(ref sbyte value) => value = ReadSByte();
        public void Read(ref short value) => value = ReadInt16();
        public void Read(ref ushort value) => value = ReadUInt16();
        public void Read(ref int value) => value = ReadInt32();
        public void Read(ref uint value) => value = ReadUInt32();
        public void Read(ref long value) => value = ReadInt64();
        public void Read(ref ulong value) => value = ReadUInt64();
        public void Read(ref Int128 value) => value = ReadInt128();
        public void Read(ref UInt128 value) => value = ReadUInt128();
        public void Read(ref Half value) => value = ReadHalf();
        public void Read(ref float value) => value = ReadFloat();
        public void Read(ref double value) => value = ReadDouble();
        public void Read(ref decimal value) => value = ReadDecimal();
        public void Read(ref string value, Encoding encoding, int lengthBytes) => value = ReadString(lengthBytes, encoding);
        public void Read(ref string value, Encoding encoding) => Read(ref value, encoding, ReadInt32());


        // READ ARRAY - REF
        public void Read(ref bool[] value, int length) => value = ReadBoolArray(length);
        public void Read(ref byte[] value, int length) => value = ReadBytes(length);
        public void Read(ref sbyte[] value, int length) => value = ReadInt8Array(length);
        public void Read(ref short[] value, int length) => value = ReadInt16Array(length);
        public void Read(ref ushort[] value, int length) => value = ReadUInt16Array(length);
        public void Read(ref int[] value, int length) => value = ReadInt32Array(length);
        public void Read(ref uint[] value, int length) => value = ReadUInt32Array(length);
        public void Read(ref long[] value, int length) => value = ReadInt64Array(length);
        public void Read(ref ulong[] value, int length) => value = ReadUInt64Array(length);
        public void Read(ref Int128[] value, int length) => value = ReadInt128Array(length);
        public void Read(ref UInt128[] value, int length) => value = ReadUInt128Array(length);
        public void Read(ref Half[] value, int length) => value = ReadHalfArray(length);
        public void Read(ref float[] value, int length) => value = ReadFloatArray(length);
        public void Read(ref double[] value, int length) => value = ReadDoubleArray(length);
        public void Read(ref decimal[] value, int length) => value = ReadDecimalArray(length);
        public void Read(ref string[] value, int length, Encoding encoding) => value = ReadStringArray(length, encoding);

        // consider returning a small struct with more info
        public bool ReadPadding(byte padding, int count)
        {
            if (count < 0)
            {
                string msg = "Cannot read padding of negative size.";
                throw new ArgumentException(msg);
            }

            for (int i = 0; i < count; i++)
            {
                byte value = ReadByte();
                if (value != padding)
                    return false;
            }
            return true;
        }


        internal short ReadInt16SameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofInt16);
            return BitConverter.ToInt16(bytes, 0);
        }
        internal short ReadInt16SwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofInt16);
            Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        internal ushort ReadUInt16SameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofUint16);
            return BitConverter.ToUInt16(bytes, 0);
        }
        internal ushort ReadUInt16SwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofUint16);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        internal int ReadInt32SameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofInt32);
            return BitConverter.ToInt32(bytes, 0);
        }
        internal int ReadInt32SwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofInt32);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        internal uint ReadUInt32SameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofUint32);
            return BitConverter.ToUInt32(bytes, 0);
        }
        internal uint ReadUInt32SwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofUint32);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        internal long ReadInt64SameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofInt64);
            return BitConverter.ToInt64(bytes, 0);
        }
        internal long ReadInt64SwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofInt64);
            Array.Reverse(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        internal ulong ReadUInt64SameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofUint64);
            return BitConverter.ToUInt64(bytes, 0);
        }
        internal ulong ReadUInt64SwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofUint64);
            Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        internal Int128 ReadInt128SameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofInt128);
            ulong upper = BitConverter.ToUInt64(bytes, 0);
            ulong lower = BitConverter.ToUInt64(bytes, SizeofInt64);
            Int128 value = new Int128(upper, lower);
            return value;
        }
        internal Int128 ReadInt128SwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofInt128);
            Array.Reverse(bytes);
            ulong upper = BitConverter.ToUInt64(bytes, 0);
            ulong lower = BitConverter.ToUInt64(bytes, SizeofInt64);
            Int128 value = new Int128(upper, lower);
            return value;
        }

        internal UInt128 ReadUInt128SameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofInt128);
            ulong upper = BitConverter.ToUInt64(bytes, 0);
            ulong lower = BitConverter.ToUInt64(bytes, SizeofUint64);
            UInt128 value = new UInt128(upper, lower);
            return value;
        }
        internal UInt128 ReadUInt128SwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofInt128);
            Array.Reverse(bytes);
            ulong upper = BitConverter.ToUInt64(bytes, 0);
            ulong lower = BitConverter.ToUInt64(bytes, SizeofUint64);
            UInt128 value = new UInt128(upper, lower);
            return value;
        }

        internal Half ReadHalfSameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofHalf);
            return BitConverter.ToHalf(bytes, 0);
        }
        internal Half ReadHalfSwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofHalf);
            Array.Reverse(bytes);
            return BitConverter.ToHalf(bytes, 0);
        }

        internal float ReadFloatSameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofFloat);
            return BitConverter.ToSingle(bytes, 0);
        }
        internal float ReadFloatSwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofFloat);
            Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        internal double ReadDoubleSameEndianness()
        {
            byte[] bytes = ReadBytes(SizeofDouble);
            return BitConverter.ToDouble(bytes, 0);
        }
        internal double ReadDoubleSwapEndianness()
        {
            byte[] bytes = ReadBytes(SizeofDouble);
            Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }


        /// <summary>
        /// Peeks the specified type from the base stream.
        /// </summary>
        /// <typeparam name="T">The type to peek</typeparam>
        /// <param name="deserializationMethod">The method used to deserialize the value from stream</param>
        /// <returns>
        /// 
        /// </returns>
        public T PeekValue<T>(Func<T> deserializationMethod)
        {
            long streamPosition = BaseStream.Position;
            T value = deserializationMethod();
            BaseStream.Seek(streamPosition, SeekOrigin.Begin);
            return value;
        }

        // System type names
        public byte PeekUInt8() => PeekValue(ReadUInt8);
        public ushort PeekUInt16() => PeekValue(fReadUInt16);
        public uint PeekUInt32() => PeekValue(fReadUInt32);
        public ulong PeekUInt64() => PeekValue(fReadUInt64);
        public UInt128 PeekUInt128() => PeekValue(fReadUInt128);
        public sbyte PeekInt8() => PeekValue(ReadInt8);
        public short PeekInt16() => PeekValue(fReadInt16);
        public int PeekInt32() => PeekValue(fReadInt32);
        public long PeekInt64() => PeekValue(fReadInt64);
        public Int128 PeekInt128() => PeekValue(fReadInt128);

        // Basic type names
        public byte PeekByte() => PeekValue(ReadByte);
        public ushort PeekUshort() => PeekValue(fReadUInt16);
        public uint PeekUInt() => PeekValue(fReadUInt32);
        public ulong PeekUlong() => PeekValue(fReadUInt64);
        public sbyte PeekSbyte() => PeekValue(ReadSByte);
        public short PeekShort() => PeekValue(fReadInt16);
        public int PeekInt() => PeekValue(fReadInt32);
        public long PeekLong() => PeekValue(fReadInt64);

        // Floating point numbers
        public Half PeekHalf() => PeekValue(fReadHalf);
        public float PeekFloat() => PeekValue(fReadFloat);
        public double PeekDouble() => PeekValue(fReadDouble);
        public decimal PeekDecimal() => PeekValue(ReadDecimal);
    }

    public static class EndianBinaryReaderCompilerWorkaround
    {
        public static void Read<TBinarySerializable>(this EndianBinaryReader reader, ref TBinarySerializable value)
            where TBinarySerializable : IBinarySerializable, new()
                => value = reader.ReadBinarySerializable<TBinarySerializable>();

        public static void Read<TEnum>(this EndianBinaryReader reader, ref TEnum value, byte _ = 0)
            where TEnum : Enum
            => value = reader.ReadEnum<TEnum>();

        public static void Read<TBinarySerializable>(this EndianBinaryReader reader, ref TBinarySerializable[] value, int length)
            where TBinarySerializable : IBinarySerializable, new()
                => value = reader.ReadBinarySerializableArray<TBinarySerializable>(length);

        public static void Read<TEnum>(this EndianBinaryReader reader, ref TEnum[] value, int length, byte _ = 0)
            where TEnum : Enum
                => value = reader.ReadEnumArray<TEnum>(length);

        public static T[] ReadArray<T>(this EndianBinaryReader reader, int length, Func<EndianBinaryReader, T> deserializeMethod)
        {
            T[] array = new T[length];
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = deserializeMethod(reader);
            }
            return array;
        }
    }

}
