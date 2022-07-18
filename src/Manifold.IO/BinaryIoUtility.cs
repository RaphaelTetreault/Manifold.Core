//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Runtime.CompilerServices;

//namespace Manifold.IO
//{
//    public static class BinaryIoUtility
//    {
//        // CONSTANTS
//        public const int SizeofBool = 1;
//        public const int SizeofInt8 = 1;
//        public const int SizeofInt16 = 2;
//        public const int SizeofInt32 = 4;
//        public const int SizeofInt64 = 8;
//        public const int SizeofUint8 = 1;
//        public const int SizeofUint16 = 2;
//        public const int SizeofUint32 = 4;
//        public const int SizeofUint64 = 8;
//        public const int SizeofFloat = 4;
//        public const int SizeofDouble = 8;
//        public const int SizeofDecimal = 16;

//        // FIELDS
//        private static Func<BinaryReader, ushort> fReadUInt16 = RequiresSwapEndianness ? ReadUInt16SwapEndianness : ReadUInt16SameEndianness;
//        private static Func<BinaryReader, uint> fReadUInt32 = RequiresSwapEndianness ? ReadUInt32SwapEndianness : ReadUInt32SameEndianness;
//        private static Func<BinaryReader, ulong> fReadUInt64 = RequiresSwapEndianness ? ReadUInt64SwapEndianness : ReadUInt64SameEndianness;
//        private static Func<BinaryReader, short> fReadInt16 = RequiresSwapEndianness ? ReadInt16SwapEndianness : ReadInt16SameEndianness;
//        private static Func<BinaryReader, int> fReadInt32 = RequiresSwapEndianness ? ReadInt32SwapEndianness : ReadInt32SameEndianness;
//        private static Func<BinaryReader, long> fReadInt64 = RequiresSwapEndianness ? ReadInt64SwapEndianness : ReadInt64SameEndianness;
//        private static Func<BinaryReader, float> fReadFloat = RequiresSwapEndianness ? ReadFloatSwapEndianness : ReadFloatSameEndianness;
//        private static Func<BinaryReader, double> fReadDouble = RequiresSwapEndianness ? ReadDoubleSwapEndianness : ReadDoubleSameEndianness;

//        private static Action<BinaryWriter, ushort> fWriteUInt16 = RequiresSwapEndianness ? WriteUInt16SwapEndianness : WriteUInt16SameEndianness;
//        private static Action<BinaryWriter, uint> fWriteUInt32 = RequiresSwapEndianness ? WriteUInt32SwapEndianness : WriteUInt32SameEndianness;
//        private static Action<BinaryWriter, ulong> fWriteUInt64 = RequiresSwapEndianness ? WriteUInt64SwapEndianness : WriteUInt64SameEndianness;
//        private static Action<BinaryWriter, short> fWriteInt16 = RequiresSwapEndianness ? WriteInt16SwapEndianness : WriteInt16SameEndianness;
//        private static Action<BinaryWriter, int> fWriteInt32 = RequiresSwapEndianness ? WriteInt32SwapEndianness : WriteInt32SameEndianness;
//        private static Action<BinaryWriter, long> fWriteInt64 = RequiresSwapEndianness ? WriteInt64SwapEndianness : WriteInt64SameEndianness;
//        private static Action<BinaryWriter, Half> fWriteHalf = RequiresSwapEndianness ? WriteHalfSwapEndianness : WriteHalfSameEndianness;
//        private static Action<BinaryWriter, float> fWriteFloat = RequiresSwapEndianness ? WriteFloatSwapEndianness : WriteFloatSameEndianness;
//        private static Action<BinaryWriter, double> fWriteDouble = RequiresSwapEndianness ? WriteDoubleSwapEndianness : WriteDoubleSameEndianness;
//        private static Action<BinaryWriter, decimal> fWriteDecimal = RequiresSwapEndianness ? WriteDecimalSwapEndianness : WriteDecimalSameEndianness;

//        private static void SetFunctionEndianness()
//        {
//            bool swapByteOrder = BitConverter.IsLittleEndian ^ IsLittleEndian;
//            if (swapByteOrder)
//            {
//                // READ
//                fReadInt16 = ReadInt16SwapEndianness;
//                fReadInt32 = ReadInt32SwapEndianness;
//                fReadInt64 = ReadInt64SwapEndianness;
//                fReadUInt16 = ReadUInt16SwapEndianness;
//                fReadUInt32 = ReadUInt32SwapEndianness;
//                fReadUInt64 = ReadUInt64SwapEndianness;
//                fReadFloat = ReadFloatSwapEndianness;
//                fReadDouble = ReadDoubleSwapEndianness;

//                // WRITE
//                fWriteInt16 = WriteInt16SwapEndianness;
//                fWriteInt32 = WriteInt32SwapEndianness;
//                fWriteInt64 = WriteInt64SwapEndianness;
//                fWriteUInt16 = WriteUInt16SwapEndianness;
//                fWriteUInt32 = WriteUInt32SwapEndianness;
//                fWriteUInt64 = WriteUInt64SwapEndianness;
//                fWriteHalf = WriteHalfSwapEndianness;
//                fWriteFloat = WriteFloatSwapEndianness;
//                fWriteDouble = WriteDoubleSwapEndianness;
//                fWriteDecimal = WriteDecimalSwapEndianness;
//            }
//            else
//            {
//                // READ
//                fReadInt16 = ReadInt16SameEndianness;
//                fReadInt32 = ReadInt32SameEndianness;
//                fReadInt64 = ReadInt64SameEndianness;
//                fReadUInt16 = ReadUInt16SameEndianness;
//                fReadUInt32 = ReadUInt32SameEndianness;
//                fReadUInt64 = ReadUInt64SameEndianness;
//                fReadFloat = ReadFloatSameEndianness;
//                fReadDouble = ReadDoubleSameEndianness;

//                // WRITE
//                fWriteInt16 = WriteInt16SameEndianness;
//                fWriteInt32 = WriteInt32SameEndianness;
//                fWriteInt64 = WriteInt64SameEndianness;
//                fWriteUInt16 = WriteUInt16SameEndianness;
//                fWriteUInt32 = WriteUInt32SameEndianness;
//                fWriteUInt64 = WriteUInt64SameEndianness;
//                fWriteHalf = WriteHalfSameEndianness;
//                fWriteFloat = WriteFloatSameEndianness;
//                fWriteDouble = WriteDoubleSameEndianness;
//                fWriteDecimal = WriteDecimalSameEndianness;
//            }
//        }


//        // PROPERTIES
//        /// <summary>
//        /// 
//        /// </summary>
//        private static bool RequiresSwapEndianness => BitConverter.IsLittleEndian ^ IsLittleEndian;

//        /// <summary>
//        /// The current endianness used for read/write operations
//        /// </summary>
//        public static Endianness Endianness { get; private set; } = Endianness.BigEndian;

//        /// <summary>
//        /// Returns true if Endianness is Little Endian
//        /// </summary>
//        public static bool IsLittleEndian => Endianness == Endianness.LittleEndian;

//        /// <summary>
//        /// Returns true if Endianness is Big Endian
//        /// </summary>
//        public static bool IsBigEndian => Endianness == Endianness.BigEndian;

//        // METHODS

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="endianness"></param>
//        public static void SetEndianness(Endianness endianness)
//        {
//            // Figure out if we need to change the function pointers
//            bool requiresFunctionChange = endianness != Endianness;
//            // Set active state to call value
//            Endianness = endianness;
//            // If we need to change functions, change them
//            if (requiresFunctionChange)
//            {
//                SetFunctionEndianness();
//            }
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static bool ReadBool(BinaryReader binaryReader)
//        {
//            return binaryReader.ReadBoolean();
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static byte ReadUInt8(BinaryReader binaryReader)
//        {
//            return binaryReader.ReadByte();
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static sbyte ReadInt8(BinaryReader binaryReader)
//        {
//            return binaryReader.ReadSByte();
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static short ReadInt16(BinaryReader binaryReader)
//        {
//            return fReadInt16.Invoke(binaryReader);
//        }
//        internal static short ReadInt16SameEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofInt16);
//            return BitConverter.ToInt16(bytes, 0);
//        }
//        internal static short ReadInt16SwapEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofInt16);
//            Array.Reverse(bytes);
//            return BitConverter.ToInt16(bytes, 0);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static ushort ReadUInt16(BinaryReader binaryReader)
//        {
//            return fReadUInt16.Invoke(binaryReader);
//        }
//        internal static ushort ReadUInt16SameEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofUint16);
//            return BitConverter.ToUInt16(bytes, 0);
//        }
//        internal static ushort ReadUInt16SwapEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofUint16);
//            Array.Reverse(bytes);
//            return BitConverter.ToUInt16(bytes, 0);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static int ReadInt32(BinaryReader binaryReader)
//        {
//            return fReadInt32.Invoke(binaryReader);
//        }
//        internal static int ReadInt32SameEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofInt32);
//            return BitConverter.ToInt32(bytes, 0);
//        }
//        internal static int ReadInt32SwapEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofInt32);
//            Array.Reverse(bytes);
//            return BitConverter.ToInt32(bytes, 0);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static uint ReadUInt32(BinaryReader binaryReader)
//        {
//            return fReadUInt32.Invoke(binaryReader);
//        }
//        internal static uint ReadUInt32SameEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofUint32);
//            return BitConverter.ToUInt32(bytes, 0);
//        }
//        internal static uint ReadUInt32SwapEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofUint32);
//            Array.Reverse(bytes);
//            return BitConverter.ToUInt32(bytes, 0);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static long ReadInt64(BinaryReader binaryReader)
//        {
//            return fReadInt64.Invoke(binaryReader);
//        }
//        internal static long ReadInt64SameEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofInt64);
//            Array.Reverse(bytes);
//            return BitConverter.ToInt64(bytes, 0);
//        }
//        internal static long ReadInt64SwapEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofInt64);
//            Array.Reverse(bytes);
//            return BitConverter.ToInt64(bytes, 0);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static ulong ReadUInt64(BinaryReader binaryReader)
//        {
//            return fReadUInt64.Invoke(binaryReader);
//        }
//        internal static ulong ReadUInt64SameEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofUint64);
//            return BitConverter.ToUInt64(bytes, 0);
//        }
//        internal static ulong ReadUInt64SwapEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofUint64);
//            Array.Reverse(bytes);
//            return BitConverter.ToUInt64(bytes, 0);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static float ReadFloat(BinaryReader binaryReader)
//        {
//            return fReadFloat.Invoke(binaryReader);
//        }
//        internal static float ReadFloatSameEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofFloat);
//            return BitConverter.ToSingle(bytes, 0);
//        }
//        internal static float ReadFloatSwapEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofFloat);
//            Array.Reverse(bytes);
//            return BitConverter.ToSingle(bytes, 0);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static double ReadDouble(BinaryReader binaryReader)
//        {
//            return fReadDouble.Invoke(binaryReader);
//        }
//        internal static double ReadDoubleSameEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofDouble);
//            return BitConverter.ToDouble(bytes, 0);
//        }
//        internal static double ReadDoubleSwapEndianness(BinaryReader binaryReader)
//        {
//            byte[] bytes = binaryReader.ReadBytes(SizeofDouble);
//            Array.Reverse(bytes);
//            return BitConverter.ToDouble(bytes, 0);
//        }


//        public static string ReadString(BinaryReader binaryReader, int lengthBytes, Encoding encoding)
//        {
//            byte[] bytes = ReadUInt8Array(binaryReader, lengthBytes);
//            string value = encoding.GetString(bytes);
//            return value;
//        }

//        public static T ReadBinarySerializable<T>(BinaryReader binaryReader) where T : IBinarySerializable, new()
//        {
//            T value = new T();
//            value.Deserialize(binaryReader);
//            return value;
//        }

//        public static TEnum ReadEnum<TEnum>(BinaryReader binaryReader) where TEnum : Enum
//        {
//            var type = Enum.GetUnderlyingType(typeof(TEnum));
//            switch (type)
//            {
//                // Ordered by my best guess as to which is most common
//                // int is the default backing type
//                case Type _ when type == typeof(int): return (TEnum)(object)ReadInt32(binaryReader);
//                // I often override the backing type to not have negatives
//                case Type _ when type == typeof(uint): return (TEnum)(object)ReadUInt32(binaryReader);
//                // byte and ushort are smaller/compress enums (also no negatives
//                case Type _ when type == typeof(byte): return (TEnum)(object)ReadUInt8(binaryReader);
//                case Type _ when type == typeof(ushort): return (TEnum)(object)ReadUInt16(binaryReader);
//                // Unlikely but perhaps userful to have 64 bits to work with
//                case Type _ when type == typeof(ulong): return (TEnum)(object)ReadUInt64(binaryReader);
//                // These are unordered: I know I don't use them as backing types
//                case Type _ when type == typeof(sbyte): return (TEnum)(object)ReadInt8(binaryReader);
//                case Type _ when type == typeof(short): return (TEnum)(object)ReadInt16(binaryReader);
//                case Type _ when type == typeof(long): return (TEnum)(object)ReadInt64(binaryReader);

//                default: throw new NotImplementedException("Unsupported Enum backing type used!");
//            }
//        }



//        // 2022/02/15: fastest is to have internal methods inlined, these methods not inlined.

//        public static bool Read(BinaryReader binaryReader, ref bool value)
//        {
//            return value = binaryReader.ReadBoolean();
//        }

//        public static byte Read(BinaryReader binaryReader, ref byte value)
//        {
//            return value = binaryReader.ReadByte();
//        }

//        public static sbyte Read(BinaryReader binaryReader, ref sbyte value)
//        {
//            return value = binaryReader.ReadSByte();
//        }

//        public static short Read(BinaryReader binaryReader, ref short value)
//        {
//            return value = ReadInt16(binaryReader);
//        }

//        public static ushort Read(BinaryReader binaryReader, ref ushort value)
//        {
//            return value = ReadUInt16(binaryReader);
//        }

//        public static int Read(BinaryReader binaryReader, ref int value)
//        {
//            return value = ReadInt32(binaryReader);
//        }

//        public static uint Read(BinaryReader binaryReader, ref uint value)
//        {
//            return value = ReadUInt32(binaryReader);
//        }

//        public static long Read(BinaryReader binaryReader, ref long value)
//        {
//            return value = ReadInt64(binaryReader);
//        }

//        public static ulong Read(BinaryReader binaryReader, ref ulong value)
//        {
//            return value = ReadUInt64(binaryReader);
//        }

//        public static float Read(BinaryReader binaryReader, ref float value)
//        {
//            return value = ReadFloat(binaryReader);
//        }

//        public static double Read(BinaryReader binaryReader, ref double value)
//        {
//            return value = ReadDouble(binaryReader);
//        }

//        public static string Read(BinaryReader binaryReader, ref string value, Encoding encoding, int lengthBytes)
//        {
//            value = ReadString(binaryReader, lengthBytes, encoding);
//            return value;
//        }
//        public static string Read(BinaryReader binaryReader, ref string value, Encoding encoding)
//        {
//            var lengthBytes = binaryReader.ReadInt32();
//            value = Read(binaryReader, ref value, encoding, lengthBytes);
//            return value;
//        }

//        public static TBinarySerializable ReadBinarySerializable<TBinarySerializable>(BinaryReader binaryReader, ref TBinarySerializable value) where TBinarySerializable : IBinarySerializable, new()
//        {
//            return value = ReadBinarySerializable<TBinarySerializable>(binaryReader);
//        }

//        public static TEnum Read<TEnum>(BinaryReader binaryReader, ref TEnum value, byte _ = 0) where TEnum : Enum
//        {
//            return value = ReadEnum<TEnum>(binaryReader);
//        }



//        public static T[] ReadArray<T>(BinaryReader binaryReader, int length, Func<BinaryReader, T> deserializeMethod)
//        {
//            T[] array = new T[length];
//            for (int i = 0; i < array.Length; ++i)
//            {
//                array[i] = deserializeMethod(binaryReader);
//            }
//            return array;
//        }

//        public static bool[] ReadBoolArray(BinaryReader binaryReader, int length)
//        {
//            return ReadArray(binaryReader, length, ReadBool);
//        }

//        public static byte[] ReadUInt8Array(BinaryReader binaryReader, int length)
//        {
//            return binaryReader.ReadBytes(length);
//        }

//        public static sbyte[] ReadInt8Array(BinaryReader binaryReader, int length)
//        {
//            return ReadArray(binaryReader, length, ReadInt8);
//        }

//        public static short[] ReadInt16Array(BinaryReader binaryReader, int length)
//        {
//            return ReadArray(binaryReader, length, ReadInt16);
//        }

//        public static ushort[] ReadUInt16Array(BinaryReader binaryReader, int length)
//        {
//            return ReadArray(binaryReader, length, ReadUInt16);
//        }

//        public static int[] ReadInt32Array(BinaryReader binaryReader, int length)
//        {
//            return ReadArray(binaryReader, length, ReadInt32);
//        }

//        public static uint[] ReadUInt32Array(BinaryReader binaryReader, int length)
//        {
//            return ReadArray(binaryReader, length, ReadUInt32);
//        }

//        public static long[] ReadInt64Array(BinaryReader binaryReader, int length)
//        {
//            return ReadArray(binaryReader, length, ReadInt64);
//        }

//        public static ulong[] ReadUInt64Array(BinaryReader binaryReader, int length)
//        {
//            return ReadArray(binaryReader, length, ReadUInt64);
//        }

//        public static float[] ReadFloatArray(BinaryReader binaryReader, int length)
//        {
//            return ReadArray(binaryReader, length, ReadFloat);
//        }

//        public static double[] ReadDoubleArray(BinaryReader binaryReader, int length)
//        {
//            return ReadArray(binaryReader, length, ReadDouble);
//        }

//        public static string[] ReadStringArray(BinaryReader binaryReader, int length, Encoding encoding)
//        {
//            var array = new string[length];
//            for (int i = 0; i < length; i++)
//            {
//                var lengthBytes = binaryReader.ReadInt32();
//                array[i] = ReadString(binaryReader, lengthBytes, encoding);
//            }
//            return array;
//        }

//        public static TBinarySerializable[] ReadBinarySerializableArray<TBinarySerializable>(BinaryReader binaryReader, int length) where TBinarySerializable : IBinarySerializable, new()
//        {
//            return ReadArray(binaryReader, length, ReadBinarySerializable<TBinarySerializable>);
//        }

//        public static TEnum[] ReadEnumArray<TEnum>(BinaryReader binaryReader, int length) where TEnum : Enum
//        {
//            TEnum[] array = new TEnum[length];

//            for (int i = 0; i < array.Length; ++i)
//                array[i] = ReadEnum<TEnum>(binaryReader);

//            return array;
//        }



//        public static bool[] Read(BinaryReader binaryReader, int length, ref bool[] value)
//        {
//            return value = ReadBoolArray(binaryReader, length);
//        }

//        public static byte[] Read(BinaryReader binaryReader, int length, ref byte[] value)
//        {
//            return value = binaryReader.ReadBytes(length);
//        }

//        public static sbyte[] Read(BinaryReader binaryReader, int length, ref sbyte[] value)
//        {
//            return value = ReadInt8Array(binaryReader, length);
//        }

//        public static short[] Read(BinaryReader binaryReader, int length, ref short[] value)
//        {
//            return value = ReadInt16Array(binaryReader, length);
//        }

//        public static ushort[] Read(BinaryReader binaryReader, int length, ref ushort[] value)
//        {
//            return value = ReadUInt16Array(binaryReader, length);
//        }

//        public static int[] Read(BinaryReader binaryReader, int length, ref int[] value)
//        {
//            return value = ReadInt32Array(binaryReader, length);
//        }

//        public static uint[] Read(BinaryReader binaryReader, int length, ref uint[] value)
//        {
//            return value = ReadUInt32Array(binaryReader, length);
//        }

//        public static long[] Read(BinaryReader binaryReader, int length, ref long[] value)
//        {
//            return value = ReadInt64Array(binaryReader, length);
//        }

//        public static ulong[] Read(BinaryReader binaryReader, int length, ref ulong[] value)
//        {
//            return value = ReadUInt64Array(binaryReader, length);
//        }

//        public static float[] Read(BinaryReader binaryReader, int length, ref float[] value)
//        {
//            return value = ReadFloatArray(binaryReader, length);
//        }

//        public static double[] Read(BinaryReader binaryReader, int length, ref double[] value)
//        {
//            return value = ReadDoubleArray(binaryReader, length);
//        }

//        public static string[] Read(BinaryReader binaryReader, int length, ref string[] value, Encoding encoding)
//        {
//            return value = ReadStringArray(binaryReader, length, encoding);
//        }

//        public static TBinarySerializable[] Read<TBinarySerializable>(BinaryReader binaryReader, int length, ref TBinarySerializable[] value) where TBinarySerializable : IBinarySerializable, new()
//        {
//            return value = ReadArray(binaryReader, length, ReadBinarySerializable<TBinarySerializable>);
//        }

//        public static TEnum[] Read<TEnum>(BinaryReader binaryReader, int length, ref TEnum[] value, byte _ = 0) where TEnum : Enum
//        {
//            return value = ReadEnumArray<TEnum>(binaryReader, length);
//        }




//        public static void Write(EndianBinaryWriter writer, bool value)
//        {
//            writer.Write(value);
//        }

//        public static void Write(EndianBinaryWriter writer, byte value)
//        {
//            writer.Write(value);
//        }

//        public static void Write(EndianBinaryWriter writer, sbyte value)
//        {
//            writer.Write(value);
//        }

//        public static void Write(EndianBinaryWriter writer, ushort value)
//        {
//            fWriteUInt16.Invoke(writer, value);
//        }
//        public static void WriteUInt16SameEndianness(EndianBinaryWriter writer, ushort value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            writer.Write(bytes);
//        }
//        public static void WriteUInt16SwapEndianness(EndianBinaryWriter writer, ushort value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            Array.Reverse(bytes);
//            writer.Write(bytes);
//        }

//        public static void Write(EndianBinaryWriter writer, short value)
//        {
//            fWriteInt16.Invoke(writer, value);
//        }
//        public static void WriteInt16SameEndianness(EndianBinaryWriter writer, short value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            writer.Write(bytes);
//        }
//        public static void WriteInt16SwapEndianness(EndianBinaryWriter writer, short value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            Array.Reverse(bytes);
//            writer.Write(bytes);
//        }

//        public static void Write(EndianBinaryWriter writer, uint value)
//        {
//            fWriteUInt32.Invoke(writer, value);
//        }
//        public static void WriteUInt32SameEndianness(EndianBinaryWriter writer, uint value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            writer.Write(bytes);
//        }
//        public static void WriteUInt32SwapEndianness(EndianBinaryWriter writer, uint value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            Array.Reverse(bytes);
//            writer.Write(bytes);
//        }

//        public static void Write(EndianBinaryWriter writer, int value)
//        {
//            fWriteInt32.Invoke(writer, value);
//        }
//        public static void WriteInt32SameEndianness(EndianBinaryWriter writer, int value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            writer.Write(bytes);
//        }
//        public static void WriteInt32SwapEndianness(EndianBinaryWriter writer, int value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            Array.Reverse(bytes);
//            writer.Write(bytes);
//        }

//        public static void Write(EndianBinaryWriter writer, ulong value)
//        {
//            fWriteUInt64.Invoke(writer, value);
//        }
//        public static void WriteUInt64SameEndianness(EndianBinaryWriter writer, ulong value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            writer.Write(bytes);
//        }
//        public static void WriteUInt64SwapEndianness(EndianBinaryWriter writer, ulong value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            Array.Reverse(bytes);
//            writer.Write(bytes);
//        }

//        public static void Write(EndianBinaryWriter writer, long value)
//        {
//            fWriteInt64.Invoke(writer, value);
//        }
//        public static void WriteInt64SameEndianness(EndianBinaryWriter writer, long value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            writer.Write(bytes);
//        }
//        public static void WriteInt64SwapEndianness(EndianBinaryWriter writer, long value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            Array.Reverse(bytes);
//            writer.Write(bytes);
//        }

//        public static void Write(EndianBinaryWriter writer, Half value)
//        {
//            fWriteHalf.Invoke(writer, value);
//        }
//        public static void WriteHalfSameEndianness(EndianBinaryWriter writer, Half value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            writer.Write(bytes);
//        }
//        public static void WriteHalfSwapEndianness(EndianBinaryWriter writer, Half value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            Array.Reverse(bytes);
//            writer.Write(bytes);
//        }

//        public static void Write(EndianBinaryWriter writer, float value)
//        {
//            fWriteFloat.Invoke(writer, value);
//        }
//        public static void WriteFloatSameEndianness(EndianBinaryWriter writer, float value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            writer.Write(bytes);
//        }
//        public static void WriteFloatSwapEndianness(EndianBinaryWriter writer, float value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            Array.Reverse(bytes);
//            writer.Write(bytes);
//        }

//        public static void Write(EndianBinaryWriter writer, double value)
//        {
//            fWriteDouble.Invoke(writer, value);
//        }
//        public static void WriteDoubleSameEndianness(EndianBinaryWriter writer, double value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            writer.Write(bytes);
//        }
//        public static void WriteDoubleSwapEndianness(EndianBinaryWriter writer, double value)
//        {
//            byte[] bytes = BitConverter.GetBytes(value);
//            Array.Reverse(bytes);
//            writer.Write(bytes);
//        }

//        public static void Write(EndianBinaryWriter writer, decimal value)
//        {
//            fWriteDecimal.Invoke(writer, value);
//        }
//        public static void WriteDecimalSameEndianness(EndianBinaryWriter writer, decimal value)
//        {
//            var ints = decimal.GetBits(value);
//            var decimalBytes = new byte[16];
//            for (int i = 0; i < 4; i++)
//            {
//                var bytes = BitConverter.GetBytes(ints[i]);
//                bytes.CopyTo(decimalBytes, i * 4);
//            }    
//            writer.Write(decimalBytes);
//        }
//        public static void WriteDecimalSwapEndianness(EndianBinaryWriter writer, decimal value)
//        {
//            var ints = decimal.GetBits(value);
//            var decimalBytes = new byte[16];
//            for (int i = 0; i < 4; i++)
//            {
//                var bytes = BitConverter.GetBytes(ints[i]);
//                Array.Reverse(bytes);
//                bytes.CopyTo(decimalBytes, i * 4);
//            }
//            writer.Write(decimalBytes);
//        }

//        public static void Write(EndianBinaryWriter writer, string value, Encoding encoding, bool writeLengthBytes)
//        {
//            byte[] bytes = encoding.GetBytes(value);

//            if (writeLengthBytes)
//                Write(writer, bytes.Length);

//            Write(writer, bytes);
//        }

//        public static void Write<TBinarySerializable>(EndianBinaryWriter writer, TBinarySerializable value) where TBinarySerializable : IBinarySerializable
//        {
//            value.Serialize(writer);
//        }

//        public static void Write<TEnum>(EndianBinaryWriter writer, TEnum value, byte _ = 0) where TEnum : Enum
//        {
//            switch (value.GetTypeCode())
//            {
//                // Ordered by my best guess as to which is most common
//                // int is the default backing type
//                case TypeCode.Int32: Write(writer, (int)(object)value); break;
//                // I often override the backing type to not have negatives
//                case TypeCode.UInt32: Write(writer, (uint)(object)value); break;
//                // byte and ushort are smaller/compressed enums (also no negatives)
//                case TypeCode.Byte: Write(writer, (byte)(object)value); break;
//                case TypeCode.UInt16: Write(writer, (ushort)(object)value); break;
//                // Unlikely but perhaps userful to have 64 bits to work with
//                case TypeCode.UInt64: Write(writer, (ulong)(object)value); break;
//                // These are unordered: I know I don't use them as backing types
//                case TypeCode.SByte: Write(writer, (sbyte)(object)value); break;
//                case TypeCode.Int16: Write(writer, (short)(object)value); break;
//                case TypeCode.Int64: Write(writer, (long)(object)value); break;

//                default: throw new NotImplementedException("Unsupported Enum backing type used!");
//            }
//        }

//        public static void WriteArray<T>(EndianBinaryWriter writer, T[] value, Action<BinaryWriter, T> serializeMethod)
//        {
//            foreach (var item in value)
//            {
//                serializeMethod(writer, item);
//            }
//        }

//        public static void Write(EndianBinaryWriter writer, bool[] value)
//            => WriteArray(writer, value, Write);

//        public static void Write(EndianBinaryWriter writer, byte[] value)
//            => writer.Write(value);

//        public static void Write(EndianBinaryWriter writer, sbyte[] value)
//            => WriteArray(writer, value, Write);

//        public static void Write(EndianBinaryWriter writer, ushort[] value)
//            => WriteArray(writer, value, Write);

//        public static void Write(EndianBinaryWriter writer, short[] value)
//            => WriteArray(writer, value, Write);

//        public static void Write(EndianBinaryWriter writer, uint[] value)
//            => WriteArray(writer, value, Write);

//        public static void Write(EndianBinaryWriter writer, int[] value)
//            => WriteArray(writer, value, Write);

//        public static void Write(EndianBinaryWriter writer, ulong[] value)
//            => WriteArray(writer, value, Write);

//        public static void Write(EndianBinaryWriter writer, long[] value)
//            => WriteArray(writer, value, Write);

//        public static void Write(EndianBinaryWriter writer, float[] value)
//            => WriteArray(writer, value, Write);

//        public static void Write(EndianBinaryWriter writer, double[] value)
//            => WriteArray(writer, value, Write);

//        public static void Write(EndianBinaryWriter writer, string[] value, Encoding encoding)
//        {
//            foreach (string str in value)
//            {
//                Write(writer, str, encoding, true);
//            }
//        }

//        public static void Write<TBinarySerializable>(EndianBinaryWriter writer, TBinarySerializable[] value) where TBinarySerializable : IBinarySerializable
//        {
//            foreach (var binarySerializable in value)
//            {
//                Write(writer, binarySerializable);
//            }
//        }

//        public static void Write<TEnum>(EndianBinaryWriter writer, TEnum[] values, byte _ = 0) where TEnum : Enum
//        {
//            foreach (var @enum in values)
//            {
//                Write(writer, @enum);
//            }
//        }

//    }
//}