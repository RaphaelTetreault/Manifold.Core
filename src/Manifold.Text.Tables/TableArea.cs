namespace Manifold.Text.Tables
{
    public struct TableArea
    {
        public ushort x;
        public ushort y;
        public uint width;
        public uint height;
        public byte colHeaderCount;
        public byte rowHeaderCount;
        public bool hasName;
    }
}
