namespace Manifold.Text.Tables;

/// <summary>
///     
/// </summary>
public sealed class TableEncodingCSV : TableEncoding
{
    private static readonly string[] expectedFileExtensions = [ ".csv" ];

    public override string ColSeparator => ",";
    public override string RowSeparator => "\n";
    public override string DefaultFileExtension => ".csv";
    public override string[] ExpectedFileExtensions => expectedFileExtensions;
}
