using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Manifold.IO
{
    public class EndianBinaryWriter : BinaryWriter
    {
        private readonly Endianness endianness;
        private readonly Action<ushort> WriteUInt16;
        private readonly Action<uint> WriteUInt32;
        private readonly Action<ulong> WriteUInt64;
        private readonly Action<UInt128> WriteUInt128;
        private readonly Action<short> WriteInt16;
        private readonly Action<int> WriteInt32;
        private readonly Action<long> WriteInt64;
        private readonly Action<Int128> WriteInt128;
        private readonly Action<Half> WriteHalf;
        private readonly Action<float> WriteFloat;
        private readonly Action<double> WriteDouble;
        private readonly Action<decimal> WriteDecimal;


        /// <summary>
        /// The current endianness used for write operations
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


        public EndianBinaryWriter(Stream output, Endianness endianness) : this(output, endianness, Encoding.UTF8, false) { }
        public EndianBinaryWriter(Stream output, Endianness endianness, Encoding encoding) : this(output, endianness, encoding, false) { }
        public EndianBinaryWriter(Stream output, Endianness endianness, Encoding encoding, bool leaveOpen) : base(output, encoding, leaveOpen)
        {
            // Store endianness
            this.endianness = endianness;
            // Select the appropriate serialization functions, store function reference
            bool requiresSwapEndianness = BitConverter.IsLittleEndian ^ IsLittleEndian;
            WriteUInt16 = requiresSwapEndianness ? WriteUInt16SwapEndianness : WriteUInt16SameEndianness;
            WriteUInt32 = requiresSwapEndianness ? WriteUInt32SwapEndianness : WriteUInt32SameEndianness;
            WriteUInt64 = requiresSwapEndianness ? WriteUInt64SwapEndianness : WriteUInt64SameEndianness;
            WriteUInt128 = requiresSwapEndianness ? WriteUInt128SwapEndianness : WriteUInt128SameEndianness;
            WriteInt16 = requiresSwapEndianness ? WriteInt16SwapEndianness : WriteInt16SameEndianness;
            WriteInt32 = requiresSwapEndianness ? WriteInt32SwapEndianness : WriteInt32SameEndianness;
            WriteInt64 = requiresSwapEndianness ? WriteInt64SwapEndianness : WriteInt64SameEndianness;
            WriteInt128 = requiresSwapEndianness ? WriteInt128SwapEndianness : WriteInt128SameEndianness;
            WriteHalf = requiresSwapEndianness ? WriteHalfSwapEndianness : WriteHalfSameEndianness;
            WriteFloat = requiresSwapEndianness ? WriteFloatSwapEndianness : WriteFloatSameEndianness;
            WriteDouble = requiresSwapEndianness ? WriteDoubleSwapEndianness : WriteDoubleSameEndianness;
            WriteDecimal = requiresSwapEndianness ? WriteDecimalSwapEndianness : WriteDecimalSameEndianness;
        }

        // Write single values
        public override void Write(bool value) => base.Write(value);
        public override void Write(byte value) => base.Write(value);
        public override void Write(ushort value) => WriteUInt16.Invoke(value);
        public override void Write(uint value) => WriteUInt32.Invoke(value);
        public override void Write(ulong value) => WriteUInt64.Invoke(value);
        //public void Write(UInt128 value) => WriteUInt128.Invoke(value);
        public override void Write(sbyte value) => base.Write(value);
        public override void Write(short value) => WriteInt16.Invoke(value);
        public override void Write(int value) => WriteInt32.Invoke(value);
        public override void Write(long value) => WriteInt64.Invoke(value);
        //public void Write(Int128 value) => WriteInt128.Invoke(value);
        public override void Write(Half value) => WriteHalf.Invoke(value);
        public override void Write(float value) => WriteFloat.Invoke(value);
        public override void Write(double value) => WriteDouble.Invoke(value);
        public override void Write(decimal value) => WriteDecimal.Invoke(value);
        public override void Write(char value) => base.Write(value);
        public override void Write(string value) => base.Write(value);
        public void Write(string value, Encoding encoding, bool writeLengthBytes)
        {
            byte[] bytes = encoding.GetBytes(value);

            if (writeLengthBytes)
                Write(bytes.Length);

            Write(bytes);
        }

        // Write array values
        public void Write(bool[] value) => WriteArray(value, base.Write);
        public override void Write(byte[] buffer) => base.Write(buffer);
        public override void Write(byte[] buffer, int index, int count) => base.Write(buffer, index, count);
        public override void Write(ReadOnlySpan<byte> buffer) => base.Write(buffer);
        public void Write(ushort[] value) => WriteArray(value, WriteUInt16);
        public void Write(uint[] value) => WriteArray(value, WriteUInt32);
        public void Write(ulong[] value) => WriteArray(value, WriteUInt64);
        //public void Write(UInt128[] value) => WriteArray(value, WriteUInt128);
        public void Write(sbyte[] value) => WriteArray(value, base.Write);
        public void Write(short[] value) => WriteArray(value, WriteInt16);
        public void Write(int[] value) => WriteArray(value, WriteInt32);
        public void Write(long[] value) => WriteArray(value, WriteInt64);
        //public void Write(Int128[] value) => WriteArray(value, WriteInt128);
        public void Write(Half[] value) => WriteArray(value, WriteHalf);
        public void Write(float[] value) => WriteArray(value, WriteFloat);
        public void Write(double[] value) => WriteArray(value, WriteDouble);
        public void Write(decimal[] value) => WriteArray(value, WriteDecimal);
        public override void Write(char[] chars) => base.Write(chars);
        public override void Write(char[] chars, int index, int count) => base.Write(chars, index, count);
        public override void Write(ReadOnlySpan<char> chars) => base.Write(chars);
        public void Write(string[] value, Encoding encoding)
        {
            foreach (string str in value)
                Write(str, encoding, true);
        }


        public void WritePadding(byte padding, int count)
        {
            if (count < 0)
            {
                string msg = "Cannot produce padding of negative size.";
                throw new ArgumentException(msg);
            }

            for (int i = 0; i < count; i++)
            {
                Write(padding);
            }
        }

        internal void WriteUInt16SameEndianness(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            base.Write(bytes);
        }
        internal void WriteUInt16SwapEndianness(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }

        internal void WriteInt16SameEndianness(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            base.Write(bytes);
        }
        internal void WriteInt16SwapEndianness(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }

        internal void WriteUInt32SameEndianness(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            base.Write(bytes);
        }
        internal void WriteUInt32SwapEndianness(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }


        internal void WriteInt32SameEndianness(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            base.Write(bytes);
        }
        internal void WriteInt32SwapEndianness(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }


        internal void WriteUInt64SameEndianness(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            base.Write(bytes);
        }
        internal void WriteUInt64SwapEndianness(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }

        internal void WriteInt64SameEndianness(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            base.Write(bytes);
        }
        internal void WriteInt64SwapEndianness(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }

        internal void WriteUInt128SameEndianness(UInt128 value)
        {
            ReadOnlySpan<byte> bytes = MemoryMarshal.Cast<UInt128, byte>(MemoryMarshal.CreateReadOnlySpan(ref value, 1));
            base.Write(bytes);
        }
        internal void WriteUInt128SwapEndianness(UInt128 value)
        {
            byte[] bytes = MemoryMarshal.Cast<UInt128, byte>(MemoryMarshal.CreateReadOnlySpan(ref value, 1)).ToArray();
            Array.Reverse(bytes);
            base.Write(bytes);
        }

        internal void WriteInt128SameEndianness(Int128 value)
        {
            ReadOnlySpan<byte> bytes = MemoryMarshal.Cast<Int128, byte>(MemoryMarshal.CreateReadOnlySpan(ref value, 1));
            base.Write(bytes);
        }
        internal void WriteInt128SwapEndianness(Int128 value)
        {
            byte[] bytes = MemoryMarshal.Cast<Int128, byte>(MemoryMarshal.CreateReadOnlySpan(ref value, 1)).ToArray();
            Array.Reverse(bytes);
            base.Write(bytes);
        }

        internal void WriteHalfSameEndianness(Half value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            base.Write(bytes);
        }
        internal void WriteHalfSwapEndianness(Half value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }

        internal void WriteFloatSameEndianness(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            base.Write(bytes);
        }
        internal void WriteFloatSwapEndianness(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }

        internal void WriteDoubleSameEndianness(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            base.Write(bytes);
        }
        internal void WriteDoubleSwapEndianness(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }

        internal void WriteDecimalSameEndianness(decimal value)
        {
            var ints = decimal.GetBits(value);
            var decimalBytes = new byte[16];
            for (int i = 0; i < 4; i++)
            {
                var bytes = BitConverter.GetBytes(ints[i]);
                bytes.CopyTo(decimalBytes, i * 4);
            }
            base.Write(decimalBytes);
        }
        internal void WriteDecimalSwapEndianness(decimal value)
        {
            var ints = decimal.GetBits(value);
            var decimalBytes = new byte[16];
            for (int i = 0; i < 4; i++)
            {
                var bytes = BitConverter.GetBytes(ints[i]);
                Array.Reverse(bytes);
                bytes.CopyTo(decimalBytes, i * 4);
            }
            base.Write(decimalBytes);
        }

        internal static void WriteArray<T>(T[] value, Action<T> serializeMethod)
        {
            foreach (var item in value)
                serializeMethod(item);
        }


    }

    /// <summary>
    /// The compiler cannot resolve which overload to use when the class itself has the generic methods.
    /// To circumvent this, we can place the generic methods in a static class and use them like extensions.
    /// When done this way, the compiler appears to fallback onto these methods properly.
    /// </summary>
    public static class EndianBinaryWriterCompilerWorkaround
    {
        public static void Write<TBinarySerializable>(this EndianBinaryWriter writer, TBinarySerializable value) where TBinarySerializable : IBinarySerializable
        {
            value.Serialize(writer);
        }

        public static void Write<TEnum>(this EndianBinaryWriter writer, TEnum value, byte _ = 0) where TEnum : Enum
        {
            switch (value.GetTypeCode())
            {
                // Ordered by my best guess as to which is most common
                // int is the default backing type
                case TypeCode.Int32: writer.Write((int)(object)value); break;
                // I often override the backing type to not have negatives
                case TypeCode.UInt32: writer.Write((uint)(object)value); break;
                // byte and ushort are smaller/compressed enums (also no negatives)
                case TypeCode.Byte: writer.Write((byte)(object)value); break;
                case TypeCode.UInt16: writer.Write((ushort)(object)value); break;
                // Unlikely but perhaps userful to have 64 bits to work with
                case TypeCode.UInt64: writer.Write((ulong)(object)value); break;
                // These are unordered: I know I don't use them as backing types
                case TypeCode.SByte: writer.Write((sbyte)(object)value); break;
                case TypeCode.Int16: writer.Write((short)(object)value); break;
                case TypeCode.Int64: writer.Write((long)(object)value); break;

                default: throw new NotImplementedException("Unsupported Enum backing type used!");
            }
        }

        public static void Write<TBinarySerializable>(this EndianBinaryWriter writer, TBinarySerializable[] value)
            where TBinarySerializable : IBinarySerializable
        {
            foreach (var binarySerializable in value)
                writer.Write(binarySerializable);
        }

        public static void Write<TEnum>(this EndianBinaryWriter writer, TEnum[] values, byte _ = 0)
            where TEnum : Enum
        {
            foreach (var @enum in values)
                writer.Write(@enum);
        }
    }


}
