namespace Manifold.Text.Tables;

/// <summary>
///     
/// </summary>
public interface ITableCollectionSerializable
{
    public void ToTables(TableCollection tableCollection);
    public void FromTables(TableCollection tableCollection);
}
