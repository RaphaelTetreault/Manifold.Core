namespace Manifold.IO;

/// <summary>
///     Indicates where in a binary stream this data type resides.
/// </summary>
public interface IBinaryAddressable
{
    /// <summary>
    ///     The binary value's address within a stream.
    /// </summary>
    AddressRange AddressRange { get; set; }
}