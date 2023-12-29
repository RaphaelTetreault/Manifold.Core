namespace Manifold.Text.Tables
{
    public interface ITableSerializable
    {
        public void Deserialize(TableCollection tableCollection);
        public void Serialize(TableCollection tableCollection);
        public string[] GetHeaders();
    }
}
