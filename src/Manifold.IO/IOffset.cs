namespace Manifold.IO
{
    internal interface IOffset
    {
        int AddressOffset { get; }
        bool IsNotNull { get; }
        bool IsNull { get; }
    }
}
