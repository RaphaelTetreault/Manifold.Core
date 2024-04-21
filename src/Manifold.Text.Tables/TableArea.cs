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

        public int BeginX => x;
        public int BeginRow => y;
        public int EndX => (int)(x + width);
        public int EndRow => (int)(y + height);
        public int NumberOfRows => (int)(width - x);
        public int NumberOfCols => (int)(height - y);

    }
}
