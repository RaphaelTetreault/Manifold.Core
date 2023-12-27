namespace Manifold.IO
{
    public interface ITableSerializable
    {
        public void Deserialize(Table table); // TODO: contrain further, interface?
        public void Serialize(Table table);
    }
}
