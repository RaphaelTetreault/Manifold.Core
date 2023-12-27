using System;
using System.Collections.Generic;

namespace Manifold.IO
{
    public class TableCollection
    // IEnumerable?
    // ICollection?
    {
        protected delegate bool CompareStrings(string a, string b);

        protected List<Table> tables = new();
        public Table CurrentTable { get; private set; } = new();
         

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
        // Get tables...?

        public void SetTable(int index)
        {
            Table table = tables[index];
            CurrentTable = table;
        }
        public void SetTable(string name, bool isCaseInsensitive)
        {
            Table table = GetTableOrError(name, isCaseInsensitive);
            CurrentTable = table;
        }

        // ICollections, IEnumerable, etc...?
        // Add table
        // Insert table
        // Remove table

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

        // TODO: figure out a good heuristic to deserializing tables/subtables
        // Enum?
        //      Single table
        //      Heuristic: trim left, right based on max bounds
        //      Above, but any gaps are considered split between tables
        // Position data array?
        //      offsets xy, wh, header wh, name y/n
        public static TableCollection FromFile()
        {
            throw new NotImplementedException();
        }

    }
}
