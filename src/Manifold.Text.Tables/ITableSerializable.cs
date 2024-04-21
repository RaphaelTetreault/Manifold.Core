namespace Manifold.Text.Tables
{
    public interface ITableSerializable
    {
        public void ReadCells(Table table);
        public void WriteCells(Table table);
        public string[] GetHeaders();
    }
}
