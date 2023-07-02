using System;
using System.Collections;

namespace Manifold.IO
{
    public class BitStreamReader
    {
        private EndianBinaryReader source;
        private BitArray bitstream;
        private int position;

        public int Position { get => position; set => position = value; }

        public BitStreamReader(EndianBinaryReader endianBinaryReader)
        {
            source = endianBinaryReader;

            long position = endianBinaryReader.BaseStream.Position;
            this.position = (int)position * 8;

            endianBinaryReader.SeekBegin();
            var bytes = endianBinaryReader.ReadBytes((int)endianBinaryReader.BaseStream.Length);
            bitstream = new BitArray(bytes);
            endianBinaryReader.JumpToAddress(position);
        }


        private T Read<T>(int nBits, Func<bool[], T> function)
        {
            int position = this.position; // optimize, put in scope
            bool[] bits = new bool[nBits];
            for (int i = 0; i < nBits; i++)
            {
                int index = position + i;
                bool bit = bitstream[index];
                bits[i] = bit;
            }
            T value = function(bits);
            UpdatePosition(nBits);
            return value;
        }

        public bool ReadBool()
        {
            bool value = bitstream[position];
            UpdatePosition(1);
            return value;
        }
        public byte ReadByte(int nBits) => Read(nBits, BoolsToByte);
        public ushort ReadUShort(int nBits) => Read(nBits, BoolsToUShort);
        public uint ReadUInt(int nBits) => Read(nBits, BoolsToUInt);
        public byte[] ReadBytes(int nBits)
        {
            int length = (int)MathF.Ceiling(nBits / 8f);
            byte[] bytes = new byte[length];
            int nBitsLeft = nBits;
            for (int i = 0; i < length; i++)
            {
                int nBitsRead = nBitsLeft > 8 ? 8 : nBitsLeft;
                bytes[i] = ReadByte(nBitsRead);
                nBitsLeft -= nBitsRead;
            }
            return bytes;
        }

        public byte ReadUInt8(int nBits) => ReadByte(nBits);
        public ushort ReadUInt16(int nBits) => ReadUShort(nBits);
        public uint ReadUInt32(int nBits) => ReadUInt(nBits);

        public void Read(ref bool value)
        {
            value = ReadBool();
        }
        public void Read(ref byte value, int nBits)
        {
            value = ReadByte(nBits);
        }
        public void Read(ref ushort value, int nBits)
        {
            value = ReadUShort(nBits);
        }
        public void Read(ref uint value, int nBits)
        {
            value = ReadUInt(nBits);
        }
        public void Read(ref byte[] value, int nBits)
        {
            value = ReadBytes(nBits);
        }
        public void Read<TIBitSerializable>(ref TIBitSerializable bitSerializable)
            where TIBitSerializable : IBitSerializable, new()
        {
            bitSerializable = new();
            bitSerializable.Deserialize(this);
        }
        public void Read<TIBitSerializable>(ref TIBitSerializable[] bitSerializables, int count)
            where TIBitSerializable : IBitSerializable, new()
        {
            bitSerializables = new TIBitSerializable[count];
            for (int i = 0; i < count; i++)
            {
                bitSerializables[i] = new();
                bitSerializables[i].Deserialize(this);
            }
        }

        private byte BoolsToByte(bool[] bools)
        {
            // TODO: MESSAGE
            bool isvalid = bools.Length <= 8;
            if (!isvalid)
                throw new ArgumentException();

            byte value = (byte)BoolsToInteger(bools);
            return value;
        }
        private ushort BoolsToUShort(bool[] bools)
        {
            // TODO: MESSAGE
            bool isvalid = bools.Length <= 16;
            if (!isvalid)
                throw new ArgumentException();

            ushort value = (ushort)BoolsToInteger(bools);
            return value;
        }
        private uint BoolsToUInt(bool[] bools)
        {
            // TODO: MESSAGE
            bool isvalid = bools.Length <= 32;
            if (!isvalid)
                throw new ArgumentException();

            uint value = (uint)BoolsToInteger(bools);
            return value;
        }

        private int BoolsToInteger(bool[] bools)
        {
            int value = 0;
            for (int i = 0; i < bools.Length; i++)
            {
                bool @bool = bools[i];
                int bit = @bool ? 1 : 0;
                value += bit << (bools.Length - 1 - i);
            }

            return value;
        }

        private void UpdatePosition(int bitsForward)
        {
            // Move bit address forward
            position += bitsForward;
            // Map position to source stream - keep in sync
            source.BaseStream.Position = (long)MathF.Ceiling(position / 8.0f);
        }
    }
}
