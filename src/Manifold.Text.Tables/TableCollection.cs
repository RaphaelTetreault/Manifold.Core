using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Manifold.Text.Tables;

/// <summary>
///     
/// </summary>
public class TableCollection :
    ICollection<Table>,
    IEnumerable,
    IEnumerable<Table>,
    IList<Table>
{
    protected delegate bool CompareStrings(string a, string b);

    protected List<Table> Tables { get; } = [];

    // ICollection, IList
    public int Count => Tables.Count;
    public bool IsReadOnly => true; // get only?
    public Table this[int index] { get => Tables[index]; set => Tables[index] = value; }


    public Table GetTable(int index)
    {
        bool isInvalidRange = index >= Tables.Count;
        if (isInvalidRange)
        {
            string msg =
                $"Collection contains {Tables.Count} tables. " +
                $"Index {index} is out or range.";
            throw new IndexOutOfRangeException(msg);
        }

        Table table = Tables[index];
        return table;
    }
    public Table? GetTable(string name, bool isCaseInsensitive = false)
    {
        CompareStrings compareEquals = GetCompareStringsFunction(isCaseInsensitive);
        foreach (Table table in Tables)
            if (compareEquals(table.Name, name))
                return table;

        return null;
    }
    public Table GetTableOrError(string name, bool isCaseInsensitive = false)
    {
        Table? table = GetTable(name, isCaseInsensitive);

        if (table == null)
        {
            string msg = $"No table with name {name} in collection.";
            throw new KeyNotFoundException(msg);
        }

        return table;
    }

    protected CompareStrings GetCompareStringsFunction(bool isCaseInsensitive)
    {
        // Compare as-is
        bool CompareFuncSensitive(string a, string b)
        {
            return a == b;
        };
        // Compare without case sensitivity
        bool CompareFuncInsensitive(string a, string b)
        {
            return a.Equals(b, StringComparison.OrdinalIgnoreCase);
        };

        // Select
        return isCaseInsensitive
            ? CompareFuncInsensitive
            : CompareFuncSensitive;
    }


    // Build tables
    public static TableCollection FromFile(string filePath, TableEncoding tableEncoding, TableInferenceMode inferenceMode = TableInferenceMode.None)
    {
        string text = File.ReadAllText(filePath);
        TableCollection tables = FromText(text, tableEncoding, inferenceMode);
        return tables;
    }
    public static TableCollection FromFile(string filePath, TableEncoding tableEncoding, TableArea[] tableAreas)
    {
        string text = File.ReadAllText(filePath);
        TableCollection tables = FromText(text, tableEncoding, tableAreas);
        return tables;
    }
    public static TableCollection FromText(string text, TableEncoding tableEncoding, TableInferenceMode inferenceMode = TableInferenceMode.None)
    {
        string[] lines = tableEncoding.GetLinesFromText(text);
        string[][] cells = tableEncoding.GetCellsFromLines(lines);

        return inferenceMode switch
        {
            TableInferenceMode.None => FromCells(cells),
            _ => FromInferedTableAreas(lines, cells, inferenceMode),
        };
    }
    public static TableCollection FromText(string text, TableEncoding tableEncoding, TableArea[] tableAreas)
    {
        string[][] cells = tableEncoding.GetCellsFromText(text);
        TableCollection tables = FromCells(cells, tableAreas);
        return tables;
    }
    public static TableCollection FromCells(string[][] cells, TableArea[] tableAreas)
    {
        TableCollection tables = [];
        foreach (var tableArea in tableAreas)
        {
            Table table = Table.FromArea(cells, tableArea);
            tables.Add(table);
        }

        return tables;
    }
    private static TableCollection FromCells(string[][] cells)
    {
        TableCollection tables = [];
        Table table = Table.FromCells(cells);
        tables.Add(table);
        return tables;
    }
    private static TableCollection FromInferedTableAreas(string[] lines, string[][] cells, TableInferenceMode inferenceMode)
    {
        TableArea[] tableAreas = InferTableAreas(lines, cells, inferenceMode);
        TableCollection tables = FromCells(cells, tableAreas);
        return tables;
    }
    private static TableArea[] InferTableAreas(string[] lines, string[][] cells, TableInferenceMode tableInference)
    {
        List<TableArea> tableAreas = [];
        TableArea tableArea = new();
        bool isReadingTable = false;

        // Capture vertical areas of tables
        for (int rowIndex = 0; rowIndex < lines.Length; rowIndex++)
        {
            string line = lines[rowIndex];
            bool hasValue = !string.IsNullOrWhiteSpace(line);
            bool captureStart = hasValue && !isReadingTable;
            bool captureEnd = !hasValue && isReadingTable;
            if (captureStart)
            {
                tableArea.posY = checked((ushort)rowIndex);
                isReadingTable = true;
            }
            else if (captureEnd)
            {
                tableArea.height = checked((uint)rowIndex - tableArea.posY);
                isReadingTable = false;
                tableAreas.Add(tableArea);
                tableArea = new TableArea();
            }
        }

        // Capture horizontal areas of tables
        foreach (TableArea area in tableAreas)
        {
            // Capture name of table...
            // inc. y if necessary

            // tODO: solve for width of table
            for (int rowIndex = area.BeginRow; rowIndex < area.EndRow; rowIndex++)
            {
                string[] linesCells = cells[rowIndex];
                // do stuff
            }
        }

        // Capture headers?

        return [.. tableAreas];
    }

    // TODO: to stream, to file, to etc...
    public void ToFile(string path, TableEncoding tableEncoding)
    {
        // move filepath from gfz-cli to manifold?
        if (Path.GetExtension(path) != tableEncoding.DefaultFileExtension)
        {
            throw new Exception();
        }

        using var writer = new StreamWriter(File.Create(path));
        
        foreach (var table in Tables)
        {
            writer.Write(table.Name);
            writer.Write(tableEncoding.RowSeparator);
            // TODO: don't use internal width, use recorded max bounds
            for (int row = 0; row < table.InternalHeight; row++)
            {
                for (int col = 0; col < table.InternalWidth; col++)
                {
                    string cell = table.GetCell(row, col);
                    writer.Write(cell);
                    writer.Write(tableEncoding.ColSeparator);
                }
                writer.Write(tableEncoding.RowSeparator);
            }
            writer.Write(tableEncoding.RowSeparator);
        }
    }
    public void Add(params Table[] tables)
    {
        foreach (var table in tables)
            Add(table);
    }

    // Interfaces
    public void Add(Table item) => Tables.Add(item);
    public void Clear() => Tables.Clear();
    public bool Contains(Table item) => Tables.Contains(item);
    public void CopyTo(Table[] array, int arrayIndex) => Tables.CopyTo(array, arrayIndex);
    public int IndexOf(Table item) => Tables.IndexOf(item);
    public void Insert(int index, Table item) => Tables.Insert(index, item);
    public void RemoveAt(int index) => Tables.RemoveAt(index);
    public bool Remove(Table item) => Tables.Remove(item);
    public IEnumerator<Table> GetEnumerator() => Tables.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Tables.GetEnumerator();

}
