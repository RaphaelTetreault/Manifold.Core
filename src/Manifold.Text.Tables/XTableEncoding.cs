namespace Manifold.IO
{
    public abstract class XTableEncoding
    {
        public abstract string ColSeparator { get; }
        public abstract string RowSeparator { get; }
        public abstract string DefaultFileExtension { get; }
        public abstract string[] ExpectedFileExtensions { get; }
    }
}
