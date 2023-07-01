using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manifold.IO
{
    public class BitStreamReader
    {
        private BitArray stream;
        private int position;

        public int Position { get => position; set => position = value; }

        public BitStreamReader(byte[] bytes)
        {
            stream = new BitArray(bytes);
        }


        private T Read<T>(int nBits, Func<bool[], T> function)
        {
            int start = position;
            int end = position + nBits;
            bool[] bits = new bool[nBits];
            for (int i = start; i < end; i++)
                bits[i] = stream[i];
            T value = function(bits);
            return value;
        }

        public bool ReadBool() => GetBool();
        public byte ReadByte(int nBits) => Read(nBits, BoolsToByte);
        public ushort ReadUShort(int nBits) => Read(nBits, BoolsToUShort);
        public uint ReadUInt(int nBits) => Read(nBits, BoolsToUInt);

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

        private bool GetBool()
        {
            bool value = stream[position];
            position++;
            return value;
        }
        private byte BoolsToByte(bool[] bools)
        {
            // TODO: MESSAGE
            bool isvalid = bools.Length <= 8;
            if (!isvalid)
                throw new ArgumentException();

            byte value = 0;
            for (int i = 0; i < bools.Length; i++)
            {
                bool @bool = bools[i];
                int bit = @bool ? 1 : 0;
                value += (byte)(bit << i);
            }

            return value;
        }
        private ushort BoolsToUShort(bool[] bools)
        {
            // TODO: MESSAGE
            bool isvalid = bools.Length <= 16;
            if (!isvalid)
                throw new ArgumentException();

            ushort value = 0;
            for (int i = 0; i < bools.Length; i++)
            {
                bool @bool = bools[i];
                int bit = @bool ? 1 : 0;
                value += (ushort)(bit << i);
            }

            return value;
        }
        private uint BoolsToUInt(bool[] bools)
        {
            // TODO: MESSAGE
            bool isvalid = bools.Length <= 32;
            if (!isvalid)
                throw new ArgumentException();

            uint value = 0;
            for (int i = 0; i < bools.Length; i++)
            {
                bool @bool = bools[i];
                int bit = @bool ? 1 : 0;
                value += (uint)(bit << i);
            }

            return value;
        }

        public void MirrorBytes()
        {
            for (int i = 0; i < stream.Length; i += 8)
            {
                // Swap bits around
                (stream[i + 0], stream[i + 7]) = (stream[i + 7], stream[i + 0]);
                (stream[i + 1], stream[i + 6]) = (stream[i + 6], stream[i + 1]);
                (stream[i + 2], stream[i + 5]) = (stream[i + 5], stream[i + 2]);
                (stream[i + 3], stream[i + 4]) = (stream[i + 4], stream[i + 3]);
            }
        }
    }
}
