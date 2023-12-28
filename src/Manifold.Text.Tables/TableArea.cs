namespace Manifold.Text.Tables
{
    public struct TableArea
    {
        public string name;
        public ushort posX;
        public ushort posY;
        public uint width;
        public uint height;
        public byte colHeaderCount;
        public byte rowHeaderCount;

        public int BeginColumn => posX;
        public int BeginRow => posY;
        public int EndColumn => (int)(posX + width);
        public int EndRow => (int)(posY + height);
        public int NumberOfRows => (int)(width - posX);
        public int NumberOfCols => (int)(height - posY);

    }
}
