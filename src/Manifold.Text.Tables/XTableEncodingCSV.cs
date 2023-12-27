namespace Manifold.IO
{
    public sealed class XTableEncodingCSV : XTableEncoding
    {
        private static readonly string[] expectedFileExtensions =
            new string[] { ".csv" };

        public override string ColSeparator => ",";
        public override string RowSeparator => "\n";
        public override string DefaultFileExtension => ".csv";
        public override string[] ExpectedFileExtensions => expectedFileExtensions;
    }
}
