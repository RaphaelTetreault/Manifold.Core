using System.IO;

namespace Manifold.IO
{
    public interface ITsvSerializable
    {
        void Deserialize(StreamReader reader);
        void Serialize(StreamWriter writer);
    }
}