using System.IO;

namespace Manifold.IO;

/// <summary>
///     
/// </summary>
public interface ITsvSerializable
{
    void Deserialize(StreamReader reader);
    void Serialize(StreamWriter writer);
}