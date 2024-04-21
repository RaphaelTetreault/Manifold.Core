namespace Manifold.Text.Tables
{
    public sealed class TableEncodingTSV : TableEncoding
    {
        public static readonly TableEncodingTSV Encoding = new();
        private static readonly string[] expectedFileExtensions =
            new string[] { ".tsv" };

        public override string ColSeparator => "\t";
        public override string RowSeparator => "\n";
        public override string DefaultFileExtension => ".tsv";
        public override string[] ExpectedFileExtensions => expectedFileExtensions;
    }
}
