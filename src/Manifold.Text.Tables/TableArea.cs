namespace Manifold.Text.Tables;

/// <summary>
///     
/// </summary>
public struct TableArea
{
    public string name;
    public ushort posX;
    public ushort posY;
    public uint width;
    public uint height;
    public byte colHeaderCount;
    public byte rowHeaderCount;

    public readonly int BeginColumn => posX;
    public readonly int BeginRow => posY;
    public readonly int EndColumn => (int)(posX + width);
    public readonly int EndRow => (int)(posY + height);
    public readonly int NumberOfRows => (int)(width - posX);
    public readonly int NumberOfCols => (int)(height - posY);

}
