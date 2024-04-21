namespace Manifold.Text.Tables
{
    public interface ITableCollectionSerializable
    {
        public void AddToTables(TableCollection tableCollection);
        public void GetTables(TableCollection tableCollection);
    }
}
