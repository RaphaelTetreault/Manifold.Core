using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Manifold.Text.Tables
{
    public class TableCollection :
        ICollection<Table>,
        IEnumerable,
        IEnumerable<Table>,
        IList<Table>
    {
        protected delegate bool CompareStrings(string a, string b);

        protected List<Table> tables { get; } = new();
        public Table CurrentTable { get; private set; } = new();

        // ICollection, IList
        public int Count => tables.Count;
        public bool IsReadOnly => true; // get only?
        public Table this[int index] { get => tables[index]; set => tables[index] = value; }


        public Table GetTable(int index)
        {
            bool isInvalidRange = index >= tables.Count;
            if (isInvalidRange)
            {
                string msg =
                    $"Collection contains {tables.Count} tables. " +
                    $"Index {index} is out or range.";
                throw new IndexOutOfRangeException(msg);
            }

            Table table = tables[index];
            return table;
        }
        public Table? GetTable(string name, bool isCaseInsensitive = false)
        {
            CompareStrings compare = GetCompareStringsFunction(isCaseInsensitive);
            foreach (Table table in tables)
                if (compare(table.Name, name))
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
        public void SetCurrentTable(int index)
        {
            Table table = tables[index];
            CurrentTable = table;
        }
        public void SetCurrentTable(string name, bool isCaseInsensitive = false)
        {
            Table table = GetTableOrError(name, isCaseInsensitive);
            CurrentTable = table;
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
                return a.ToLower() == b.ToLower();
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
            TableCollection tables = new TableCollection();
            foreach (var tableArea in tableAreas)
            {
                Table table = Table.FromArea(cells, tableArea);
                tables.Add(table);
            }

            return tables;
        }
        private static TableCollection FromCells(string[][] cells)
        {
            TableCollection tables = new TableCollection();
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
            List<TableArea> tableAreas = new();
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

            return tableAreas.ToArray();
        }

        public void ToFile(string path, TableEncoding tableEncoding)
        {
            // move filepath from gfz-cli to manifold?
            if (Path.GetExtension(path) != tableEncoding.DefaultFileExtension)
            {
                throw new Exception();
            }

            using var writer = new StreamWriter(File.OpenWrite(path));
            
            foreach (var table in tables)
            {
                writer.Write(table.Name);
                writer.Write(tableEncoding.RowSeparator);
                for (int row = 0; row < table.Height; row++)
                {
                    for (int col = 0; col < table.Width; col++)
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

        // Interfaces
        public void Add(Table item) => tables.Add(item);
        public void Clear() => tables.Clear();
        public bool Contains(Table item) => tables.Contains(item);
        public void CopyTo(Table[] array, int arrayIndex) => tables.CopyTo(array, arrayIndex);
        public int IndexOf(Table item) => tables.IndexOf(item);
        public void Insert(int index, Table item) => tables.Insert(index, item);
        public void RemoveAt(int index) => tables.RemoveAt(index);
        public bool Remove(Table item) => tables.Remove(item);
        public IEnumerator<Table> GetEnumerator() => tables.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => tables.GetEnumerator();

    }
}
