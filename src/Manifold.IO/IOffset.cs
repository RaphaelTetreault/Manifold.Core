namespace Manifold.IO;

/// <summary>
///     
/// </summary>
internal interface IOffset
{
    int AddressOffset { get; }
    bool IsNotNull { get; }
    bool IsNull { get; }
}
