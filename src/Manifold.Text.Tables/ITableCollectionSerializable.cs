namespace Manifold.Text.Tables
{
    public interface ITableCollectionSerializable
    {
        public void ToTables(TableCollection tableCollection);
        public void FromTables(TableCollection tableCollection);
    }
}
