using System;
using System.Collections.Generic;

// Considerations
// 1. Make the underlying collection a buffer with a way to infer row/col?
//    Perhaps that means List<List<string>> ?
//    IDK what the memory overhead looks like.
//    Maybe implement a "growing" [,] like list?

namespace Manifold.IO
{
    public class Table
    {
        public string Name { get; set; } = string.Empty;
        public int ColHeadersCount { get; set; }
        public int RowHeadersCount { get; set; }
        public string[,] Values { get; private set; } = new string[0, 0];
        public int Width => Values.GetLength(0);
        public int Height => Values.GetLength(1);
        public bool HasRowHeaders => RowHeadersCount > 0;
        public bool HasColHeaders => ColHeadersCount > 0;
        // TODO: read/write axis (horizontal/vertical), config generic readnext

        public string[] GetRow(int index)
        {
            throw new NotImplementedException();
        }
        public string[] GetRow(string rowHeader)
        {
            throw new NotImplementedException();
        }
        public string GetNextFromRow()
        {
            throw new NotImplementedException();
        }
        public T GetNextFromRowAs<T>(Func<T, string> parse)
        {
            throw new NotImplementedException();
        }

        public string[] GetCol(int index)
        {
            throw new NotImplementedException();
        }
        public string[] GetCol(string colHeader)
        {
            throw new NotImplementedException();
        }
        public string GetNextFromCol()
        {
            throw new NotImplementedException();
        }
        public T GetNextFromColAs<T>(Func<T, string> parse)
        {
            throw new NotImplementedException();
        }

        public string GetCell(int x, int y)
        {
            throw new NotImplementedException();
        }
        public T GetCellAs<T>(Func<T, string> parse)
        {
            throw new NotImplementedException();
        }

        public string GetColHeader(int x, int y = 0)
        {
            throw new NotImplementedException();
        }
        public string GetRowHeader(int y, int x = 0)
        {
            throw new NotImplementedException();
        }
    }
}
