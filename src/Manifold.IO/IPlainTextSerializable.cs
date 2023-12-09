using System.IO;

namespace Manifold.IO
{
    public interface IPlainTextSerializable
    {
        void Deserialize(PlainTextReader reader);
        void Serialize(PlainTextWriter writer);
    }
}