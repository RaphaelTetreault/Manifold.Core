using System;
using System.Collections;
using System.Collections.Generic;

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
