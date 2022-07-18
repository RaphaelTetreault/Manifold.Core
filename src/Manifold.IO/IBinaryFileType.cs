namespace Manifold.IO
{
    public interface IBinaryFileType : 
        IFileType
    {
        Endianness Endianness { get; }
    }
}
