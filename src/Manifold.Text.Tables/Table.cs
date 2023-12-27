using System;
using System.Collections.Generic;

// TODO: CopyTo table?

namespace Manifold.Text.Tables
{
    public class Table
    {
        private const int DefaultSize = 32;
        public delegate string GetValueOnAxis(out bool isAtEndOfAxis);


        public string Name { get; set; } = string.Empty;
        public int ColHeadersCount { get; set; }
        public int RowHeadersCount { get; set; }
        public string[][] ValueRows { get; private set; } = Array.Empty<string[]>();
        public TableAxis ReadNextAxis { get; private set; } = TableAxis.Horizontal;
        public int ActiveCol { get; private set; }
        public int ActiveRow { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool HasRowHeaders => RowHeadersCount > 0;
        public bool HasColHeaders => ColHeadersCount > 0;

        private int InternalWidth => ValueRows.GetLength(0);
        private int InternalHeight => ValueRows.GetLength(1);
        private GetValueOnAxis GetNextFromAxis { get; set; }
        private bool CanAddVertically => Height < InternalHeight;
        private bool CanAddHorizontally => Width < InternalWidth;


        // CONSTRUCTORS
        public Table()
        {
            HardReset();
            GetNextFromAxis = GetNextFromRow;
            SetTableAxis(ReadNextAxis);
        }


        // METHODS
        public string GetNext()
            => GetNext(out _);
        public string GetNext(out bool isAtEndOfAxis)
        {
            string value = GetNextFromAxis.Invoke(out isAtEndOfAxis);
            return value;
        }
        public T GetNextAs<T>(Func<string, T> parse)
            => GetNextAs(parse, out _);
        public T GetNextAs<T>(Func<string, T> parse, out bool isAtEndOfAxis)
        {
            string strValue = GetNext(out isAtEndOfAxis);
            T value = parse.Invoke(strValue);
            return value;
        }
        public ReadOnlySpan<string> GetRow(int index)
        {
            string[] row = ValueRows[index];
            string[] rowData = row[RowHeadersCount..Width];
            return rowData;
        }
        public ReadOnlySpan<string> GetRow(string rowHeader)
        {
            foreach (string[] row in ValueRows)
            {
                string[] rowHeaders = row[..RowHeadersCount];
                foreach (string header in rowHeaders)
                {
                    if (header != rowHeader)
                        continue;

                    string[] rowData = row[RowHeadersCount..Width];
                    return rowData;
                }
            }

            string msg = $"No row with header value {rowHeader}.";
            throw new KeyNotFoundException(msg);
        }
        public string GetNextFromRow()
            => GetNextFromRow(out _);
        public string GetNextFromRow(out bool isAtEndOfRow)
        {
            string value = GetDataCell(ActiveRow, ActiveCol);

            isAtEndOfRow = false;
            ActiveRow++;
            if (ActiveRow >= Height)
            {
                ActiveRow = ColHeadersCount;
                ActiveCol++;
                isAtEndOfRow = true;
            }

            return value;
        }
        public T GetNextFromRowAs<T>(Func<string, T> parse)
            => GetNextFromRowAs(parse, out _);
        public T GetNextFromRowAs<T>(Func<string, T> parse, out bool isAtEndOfRow)
        {
            string strValue = GetNextFromRow(out isAtEndOfRow);
            T value = parse.Invoke(strValue);
            return value;
        }
        public ReadOnlySpan<string> GetCol(int columnIndex)
        {
            int count = Height - ColHeadersCount;
            string[] col = new string[count];

            int destRow = 0;
            int srcRow = RowHeadersCount;
            while (destRow < count)
            {
                col[destRow] = ValueRows[srcRow][columnIndex];
                srcRow++;
                destRow++;
            }

            return col;
        }
        public ReadOnlySpan<string> GetCol(string colHeader)
        {
            for (int x = RowHeadersCount; x < Width; x++)
            {
                for (int y = 0; y < ColHeadersCount; y++)
                {
                    string header = ValueRows[y][x];
                    if (header != colHeader)
                        continue;

                    ReadOnlySpan<string> col = GetCol(x);
                    return col;
                }
            }

            string msg = $"No column with header value {colHeader}.";
            throw new KeyNotFoundException(msg);
        }
        public string GetNextFromCol()
            => GetNextFromCol(out _);
        public string GetNextFromCol(out bool isAtEndOfCol)
        {
            string value = GetDataCell(ActiveRow, ActiveCol);

            isAtEndOfCol = false;
            ActiveCol++;
            if (ActiveCol >= Width)
            {
                ActiveCol = RowHeadersCount;
                ActiveRow++;
                isAtEndOfCol = true;
            }

            return value;
        }
        public T GetNextFromColAs<T>(Func<string, T> parse)
            => GetNextFromColAs(parse, out _);
        public T GetNextFromColAs<T>(Func<string, T> parse, out bool isAtEndOfCol)
        {
            string strValue = GetNextFromCol(out isAtEndOfCol);
            T value = parse.Invoke(strValue);
            return value;
        }

        public string GetCell(int rowIndex, int columnIndex)
        {
            AssertCellIndex(rowIndex, columnIndex);
            string value = ValueRows[rowIndex][columnIndex];
            return value;
        }
        public T GetCellAs<T>(int rowIndex, int columnIndex, Func<string, T> parse)
        {
            string strValue = GetCell(rowIndex, columnIndex);
            T value = parse.Invoke(strValue);
            return value;
        }
        public string GetDataCell(int rowIndex, int columnIndex)
        {
            AssertDataCellIndex(rowIndex, columnIndex);
            AssertCellIndex(rowIndex, columnIndex);
            rowIndex += RowHeadersCount;
            columnIndex += ColHeadersCount;
            string value = ValueRows[rowIndex][columnIndex];
            return value;
        }
        public T GetDataCellAs<T>(int rowIndex, int columnIndex, Func<string, T> parse)
        {
            string strValue = GetDataCell(rowIndex, columnIndex);
            T value = parse.Invoke(strValue);
            return value;
        }
        public string GetColHeader(int colIndex, int rowIndex = 0)
        {
            AssertHeaderIndex(rowIndex, colIndex);
            string header = ValueRows[rowIndex][colIndex];
            return header;
        }
        public string GetRowHeader(int rowIndex, int colIndex = 0)
        {
            AssertHeaderIndex(rowIndex, colIndex);
            string header = ValueRows[rowIndex][colIndex];
            return header;
        }
        private void AssertHeaderIndex(int rowIndex, int colIndex)
        {
            bool invalidColumn = colIndex >= ColHeadersCount;
            bool invalidRow = rowIndex >= RowHeadersCount;
            if (invalidRow || invalidColumn)
            {
                string msg = $"Invalid header index [{rowIndex},{colIndex}]";
                throw new ArgumentOutOfRangeException(msg);
            }
        }
        private void AssertDataCellIndex(int rowIndex, int colIndex)
        {
            bool invalidColumn = colIndex < ColHeadersCount;
            bool invalidRow = rowIndex < RowHeadersCount;
            if (invalidRow || invalidColumn)
            {
                string msg = $"Invalid data cell index [{rowIndex},{colIndex}]";
                throw new ArgumentOutOfRangeException(msg);
            }
        }
        private void AssertCellIndex(int rowIndex, int colIndex)
        {
            bool invalidColumn = colIndex >= Width;
            bool invalidRow = rowIndex >= Height;
            if (invalidRow || invalidColumn)
            {
                string msg = $"Invalid cell index [{rowIndex},{colIndex}]";
                throw new ArgumentOutOfRangeException(msg);
            }
        }

        public void SetTableAxis(TableAxis tableAxis)
        {
            bool doesNotRequireUpdate = ReadNextAxis == tableAxis;
            if (doesNotRequireUpdate)
                return;

            ReadNextAxis = tableAxis;
            switch (tableAxis)
            {
                case TableAxis.Horizontal:
                    GetNextFromAxis = GetNextFromCol;
                    break;

                case TableAxis.Vertical:
                    GetNextFromAxis = GetNextFromRow;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
        // Set row, col, cells?


        private void ExpandHorizontally()
        {
            // Calculate new width
            int newWidth = InternalWidth * 2;
            // Double horizontal size of each row
            for (int y = 0; y < InternalHeight; y++)
            {
                string[] row = ValueRows[y];
                string[] newRow = ArrayUtility.DefaultArray(string.Empty, newWidth);
                row.CopyTo(newRow, 0);
                ValueRows[y] = newRow;
            }
        }
        private void ExpandVertically()
        {
            // Get reference to existing array
            var valueRows = ValueRows;
            // Double vertical size
            int oldHeight = InternalHeight;
            int newHeight = InternalHeight * 2;
            ValueRows = new string[newHeight][];
            // Copy old values into new array
            valueRows.CopyTo(ValueRows, 0);
            // Create new entries
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, Width);
            for (int y = oldHeight; y < newHeight; y++)
                ValueRows[y] = defaultArray;
        }

        public void AppendRow()
        {
            string[] row = ArrayUtility.DefaultArray(string.Empty, InternalWidth);
            AppendRow(row);
        }
        public void AppendRow(string[] row, params string[] headers)
        {
            if (!CanAddHorizontally)
                ExpandVertically();

            ValueRows[Height] = row;
            Height++;

            throw new NotImplementedException();
        }
        public void AppendCol()
        {
            throw new NotImplementedException();
        }
        public void AppendCol(string[] col, params string[] headers)
        {
            throw new NotImplementedException();
        }
        public void ClearAll()
        {
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, InternalWidth);
            for (int y = 0; y < Height; y++)
                ValueRows[y] = defaultArray;
        }
        public void ClearData()
        {
            int width = Width - RowHeadersCount;
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, width);
            for (int y = RowHeadersCount; y < Height; y++)
                defaultArray.CopyTo(ValueRows[y], RowHeadersCount);
        }
        public bool Contains(string[] item)
        {
            // 
            bool isItemLonger = item.Length > InternalWidth;
            if (isItemLonger)
                return false;

            // 
            foreach (string[] row in ValueRows)
            {
                int min = Math.Min(item.Length, row.Length);
                for (int i = 0; i < min; i++)
                {
                    bool isSame = item[i] == row[i];
                    if (isSame)
                        return true;
                }
            }

            return false;
        }
        public int ContainsAt(string[] item)
        {
            foreach (string[] row in ValueRows)
            {
                int min = Math.Min(item.Length, row.Length);
                for (int i = 0; i < min; i++)
                {
                    bool isSame = item[i] == row[i];
                    if (isSame)
                        return i;
                }
            }
            return -1;
        }
        public void HardReset()
        {
            ValueRows = ArrayUtility.DefaultArray2D(string.Empty, DefaultSize, DefaultSize);
        }
        public void InsertRow()
        {
            throw new NotImplementedException();
        }
        public void InsertRow(string[] row, params string[] headers)
        {
            throw new NotImplementedException();
        }
        public void InsertCol()
        {
            throw new NotImplementedException();
        }
        public void InsertCol(string[] col, params string[] headers)
        {
            throw new NotImplementedException();
        }
        public bool RemoveRow(int rowIndex)
        {
            // Indicate if cannot remove that row
            bool canRemoveItem = rowIndex < Height;
            if (canRemoveItem)
                return false;

            // Shift rows down
            for (int y = rowIndex; y < Height - 1; y++)
                ValueRows[y] = ValueRows[y + 1];
            // Replace final item with empty
            ValueRows[Height] = ArrayUtility.DefaultArray(string.Empty, InternalWidth);
            // Update height marker
            Height--;

            // Update header count
            bool isHeaderRow = rowIndex < ColHeadersCount;
            if (isHeaderRow)
                ColHeadersCount--;

            return true;
        }
        public bool RemoveCol(int colIndex)
        {
            throw new NotImplementedException();
        }

        public static Table FromArea(string[][] cells, TableArea area)
        {
            throw new NotImplementedException();
        }
    }
}
