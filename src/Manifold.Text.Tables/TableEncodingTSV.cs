using System;
namespace Manifold.IO
{
    public sealed class TableEncodingTSV : TableEncoding
    {
        private static readonly string[] expectedFileExtensions =
            new string[] { ".tsv" };

        public override string ColSeparator => "\t";
        public override string RowSeparator => "\n";
        public override string DefaultFileExtension => ".tsv";
        public override string[] ExpectedFileExtensions => expectedFileExtensions;
    }
}
