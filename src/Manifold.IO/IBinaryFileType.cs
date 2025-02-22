namespace Manifold.IO;

/// <summary>
///     Indicates this type is a binary file.
/// </summary>
public interface IBinaryFileType : 
    IFileType
{
    Endianness Endianness { get; }
}
