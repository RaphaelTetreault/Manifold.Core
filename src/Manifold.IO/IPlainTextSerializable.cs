namespace Manifold.IO;

/// <summary>
///     
/// </summary>
public interface IPlainTextSerializable
{
    void Deserialize(PlainTextReader reader);
    void Serialize(PlainTextWriter writer);
}