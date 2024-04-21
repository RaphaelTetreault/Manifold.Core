using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

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
        public string[][] TableRowCol { get; private set; } = Array.Empty<string[]>();
        public TableAxis ReadNextAxis { get; private set; } = TableAxis.Horizontal;
        public int ActiveDataCol { get; private set; }
        public int ActiveDataRow { get; private set; }
        public int DataWidth { get; private set; }
        public int DataHeight { get; private set; }
        public int FullWidth => RowHeadersCount + DataWidth;
        public int FullHeight => ColHeadersCount + DataHeight;
        public bool HasRowHeaders => RowHeadersCount > 0;
        public bool HasColHeaders => ColHeadersCount > 0;

        private int InternalWidth => TableRowCol.GetLength(0);
        private int InternalHeight => TableRowCol.GetLength(1);
        private GetValueOnAxis GetNextFromAxis { get; set; }
        private bool CanAddVertically => DataHeight < InternalHeight;
        private bool CanAddHorizontally => DataWidth < InternalWidth;


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
        public string[] GetRow(int rowIndex)
        {
            string[] row = TableRowCol[rowIndex];
            string[] rowData = row[RowHeadersCount..DataWidth];
            return rowData;
        }
        public string[] GetRow(string rowHeader)
        {
            foreach (string[] row in TableRowCol)
            {
                string[] rowHeaders = row[..RowHeadersCount];
                foreach (string header in rowHeaders)
                {
                    if (header != rowHeader)
                        continue;

                    string[] rowData = row[RowHeadersCount..DataWidth];
                    return rowData;
                }
            }

            string msg = $"No row with header value {rowHeader}.";
            throw new KeyNotFoundException(msg);
        }
        public string[] GetFullRow(int rowIndex)
        {
            string[] row = TableRowCol[rowIndex];
            string[] rowData = row[0..DataWidth];
            return rowData;
        }
        public string GetNextFromRow()
            => GetNextFromRow(out _);
        public string GetNextFromRow(out bool isAtEndOfRow)
        {
            string value = GetDataCell(ActiveDataRow, ActiveDataCol);

            isAtEndOfRow = false;
            ActiveDataRow++;
            if (ActiveDataRow >= DataHeight)
            {
                ActiveDataRow = ColHeadersCount;
                ActiveDataCol++;
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
        public string[] GetCol(int columnIndex)
        {
            string[] col = new string[DataHeight];

            int destRow = 0;
            int srcRow = RowHeadersCount;
            while (destRow < DataHeight)
            {
                col[destRow] = TableRowCol[srcRow][columnIndex];
                srcRow++;
                destRow++;
            }

            return col;
        }
        public string[] GetCol(string colHeader)
        {
            for (int col = RowHeadersCount; col < DataWidth; col++)
            {
                for (int row = 0; row < ColHeadersCount; row++)
                {
                    string header = TableRowCol[row][col];
                    if (header != colHeader)
                        continue;

                    string[] colValues = GetCol(col);
                    return colValues;
                }
            }

            string msg = $"No column with header value {colHeader}.";
            throw new KeyNotFoundException(msg);
        }
        public string[] GetFullCol(int columnIndex)
        {
            string[] col = new string[FullHeight];

            for (int rowIndex = 0; rowIndex < FullHeight; rowIndex++)
                col[rowIndex] = TableRowCol[rowIndex][columnIndex];

            return col;
        }

        public string GetNextFromCol()
            => GetNextFromCol(out _);
        public string GetNextFromCol(out bool isAtEndOfCol)
        {
            string value = GetDataCell(ActiveDataRow, ActiveDataCol);

            isAtEndOfCol = false;
            ActiveDataCol++;
            if (ActiveDataCol >= DataWidth)
            {
                ActiveDataCol = RowHeadersCount;
                ActiveDataRow++;
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
            string value = TableRowCol[rowIndex][columnIndex];
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
            string value = TableRowCol[rowIndex][columnIndex];
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
            string header = TableRowCol[rowIndex][colIndex];
            return header;
        }
        public string GetRowHeader(int rowIndex, int colIndex = 0)
        {
            AssertHeaderIndex(rowIndex, colIndex);
            string header = TableRowCol[rowIndex][colIndex];
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
            bool invalidColumn = colIndex >= DataWidth;
            bool invalidRow = rowIndex >= DataHeight;
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
                string[] row = TableRowCol[y];
                string[] newRow = ArrayUtility.DefaultArray(string.Empty, newWidth);
                row.CopyTo(newRow, 0);
                TableRowCol[y] = newRow;
            }
        }
        private void ExpandVertically()
        {
            // Get reference to existing array
            var valueRows = TableRowCol;
            // Double vertical size
            int oldHeight = InternalHeight;
            int newHeight = InternalHeight * 2;
            TableRowCol = new string[newHeight][];
            // Copy old values into new array
            valueRows.CopyTo(TableRowCol, 0);
            // Create new entries
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, InternalWidth);
            for (int y = oldHeight; y < newHeight; y++)
                TableRowCol[y] = defaultArray;
        }


        private void AssertRowsCount(int rowValues, int headerValues)
        {
            bool isSameValuesLength = DataWidth == rowValues;
            if (!isSameValuesLength)
            {
                string msg = "";
                throw new ArgumentException(msg);
            }

            bool isSameHeadersLength = RowHeadersCount == headerValues;
            if (!isSameHeadersLength)
            {
                string msg = "";
                throw new ArgumentException(msg);
            }
        }
        private void AssertColsCount(int colValues, int headerValues)
        {
            bool isSameValuesLength = DataWidth == colValues;
            if (!isSameValuesLength)
            {
                string msg = "";
                throw new ArgumentException(msg);
            }

            bool isSameHeadersLength = RowHeadersCount == headerValues;
            if (!isSameHeadersLength)
            {
                string msg = "";
                throw new ArgumentException(msg);
            }
        }


        private void CopyToRow(int rowIndex, string[] rowValues, params string[] headers)
        {
            headers.CopyTo(TableRowCol[rowIndex], 0);
            rowValues.CopyTo(TableRowCol[rowIndex], RowHeadersCount);
        }
        private void CopyToRowRaw(int rowIndex, string[] fullRow)
        {
            //bool isSameLength = fullRow.Length == FullWidth;
            //if (!isSameLength)
            //{
            //    string msg = "";
            //    throw new ArgumentException(msg);
            //}

            fullRow.CopyTo(TableRowCol[rowIndex], 0);
        }
        private void CopyToCol(int colIndex, string[] colValues, params string[] headers)
        {
            // Create temp column
            int length = ColHeadersCount + colValues.Length;
            string[] fullColumn = new string[length];
            // Copy split arrays into column
            headers.CopyTo(fullColumn, 0);
            colValues.CopyTo(fullColumn, ColHeadersCount);
            // Copy full column over
            CopyToColRaw(colIndex, fullColumn);
        }
        private void CopyToColRaw(int colIndex, string[] fullColumn)
        {
            for (int rowIndex = 0; rowIndex < FullHeight; rowIndex++)
                TableRowCol[rowIndex][colIndex] = fullColumn[rowIndex];
        }

        public void AppendRow()
        {
            string[] row = ArrayUtility.DefaultArray(string.Empty, InternalWidth);
            AppendRow(row);
        }
        public void AppendRow(string[] row, params string[] headers)
        {
            AssertRowsCount(row.Length, headers.Length);

            if (!CanAddHorizontally)
                ExpandVertically();

            CopyToRow(FullHeight, row, headers);
            DataHeight++;
        }
        public void AppendCol()
        {
            string[] col = ArrayUtility.DefaultArray(string.Empty, InternalHeight);
            AppendCol(col);
        }
        public void AppendCol(string[] col, params string[] headers)
        {
            AssertColsCount(col.Length, headers.Length);

            if (!CanAddVertically)
                ExpandVertically();

            CopyToCol(FullWidth, col, headers);
            DataWidth++;
        }
        public void ClearAll()
        {
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, InternalWidth);
            for (int y = 0; y < DataHeight; y++)
                TableRowCol[y] = defaultArray;
        }
        public void ClearData()
        {
            int width = DataWidth - RowHeadersCount;
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, width);
            for (int y = RowHeadersCount; y < DataHeight; y++)
                defaultArray.CopyTo(TableRowCol[y], RowHeadersCount);
        }
        public bool RowContains(int rowIndex, string value)
        {
            int index = RowContainsAt(rowIndex, value);
            bool doesContainValue = index > 0;
            return doesContainValue;
        }
        public int RowContainsAt(int rowIndex, string value)
        {
            for (int colIndex = RowHeadersCount; colIndex < DataWidth; colIndex++)
            {
                string colValue = TableRowCol[rowIndex][colIndex];
                bool isSame = colValue == value;
                if (isSame)
                {
                    int index = colIndex - RowHeadersCount;
                    return index;
                }
            }
            return -1;
        }
        public bool ColContains(int colIndex, string value)
        {
            int index = ColContainsAt(colIndex, value);
            bool doesContainValue = index > 0;
            return doesContainValue;
        }
        public int ColContainsAt(int colIndex, string value)
        {
            for (int rowIndex = ColHeadersCount; rowIndex < DataHeight; rowIndex++)
            {
                string rowValue = TableRowCol[rowIndex][colIndex];
                bool isSame = rowValue == value;
                if (isSame)
                {
                    int index = rowIndex - ColHeadersCount;
                    return index;
                }
            }
            return -1;
        }
        public void HardReset()
        {
            TableRowCol = ArrayUtility.DefaultArray2D(string.Empty, DefaultSize, DefaultSize);
        }
        public void InsertRow(int rowIndex)
        {
            string[] row = ArrayUtility.DefaultArray(string.Empty, DataWidth);
            InsertRow(rowIndex, row);
        }
        public void InsertRow(int rowIndex, string[] rowValues, params string[] headers)
        {
            AssertRowsCount(rowValues.Length, headers.Length);

            if (!CanAddVertically)
                ExpandVertically();

            // shift rows at index down 1
            rowIndex += ColHeadersCount;
            for (int r = FullHeight; r > rowIndex; r--)
                TableRowCol[r] = TableRowCol[r - 1];

            CopyToRow(rowIndex, rowValues, headers);
            DataHeight++;
        }
        public void InsertCol(int colIndex)
        {
            string[] col = ArrayUtility.DefaultArray(string.Empty, DataHeight);
            InsertCol(colIndex, col);
        }
        public void InsertCol(int colIndex, string[] colValues, params string[] headers)
        {
            AssertColsCount(colValues.Length, headers.Length);

            if (!CanAddHorizontally)
                ExpandHorizontally();

            for (int r = 0; r > FullHeight; r++) // each row, 0-n
                for (int c = FullWidth; c > colIndex; c--) // each col, n-colIndex
                    TableRowCol[r][c] = TableRowCol[r][c - 1]; // copy left cell right

            CopyToCol(colIndex, colValues, headers);
            DataWidth++;
        }
        
        // TODO: inconsistent. This works on value and header.
        public bool RemoveRow(int rowIndex)
        {
            // Indicate if cannot remove that row
            bool canRemoveItem = rowIndex < FullHeight;
            if (canRemoveItem)
                return false;

            // Shift rows down
            for (int y = rowIndex; y < FullHeight - 1; y++)
                TableRowCol[y] = TableRowCol[y + 1];
            // Replace final item with empty
            TableRowCol[FullHeight] = ArrayUtility.DefaultArray(string.Empty, InternalWidth);
            
            // Update height
            DataHeight--;
            // Update header count
            bool isHeaderRow = rowIndex < ColHeadersCount;
            if (isHeaderRow)
                ColHeadersCount--;

            return true;
        }
        public bool RemoveCol(int colIndex)
        {
            // Indicate if cannot remove col
            bool canRemoveItem = colIndex < FullWidth;
            if (canRemoveItem)
                return false;

            // Shift columns left from removed index
            for (int i = colIndex; i < FullWidth - 1; i++)
            {
                string[] nextCol = TableRowCol[i+1];
                CopyToColRaw(i, nextCol);
            }
            // Clear contents of final column
            string[] emptyColumn = ArrayUtility.DefaultArray(string.Empty, FullHeight);
            CopyToColRaw(FullWidth, emptyColumn);

            // Update width
            DataWidth--;
            // Update header count
            bool isHeader = colIndex < RowHeadersCount;
            if (isHeader)
                RowHeadersCount--;

            return true;
        }


        public void PivotTable()
        {
            // Flip axes
            string[][] pivotedTable = new string[InternalHeight][];

            // Take columns from old table, insert into new table
            for (int colIndex = 0; colIndex < FullWidth; colIndex++)
            {
                string[] column = TableRowCol[colIndex];
                pivotedTable[colIndex] = column;
            }

            // Swap header counts
            int tempColHeadersCount = ColHeadersCount;
            ColHeadersCount = RowHeadersCount;
            RowHeadersCount = tempColHeadersCount;
            // Swap data counts
            int tempDataWidth = DataWidth;
            DataWidth = DataHeight;
            DataHeight = tempDataWidth;
            // Swap active
            int tempActiveDataCol = ActiveDataCol;
            ActiveDataCol = ActiveDataRow;
            ActiveDataRow = tempActiveDataCol;
        }
        public void SwapRows(int rowIndex1, int rowIndex2)
        {
            string[] row1 = TableRowCol[rowIndex1];
            string[] row2 = TableRowCol[rowIndex2];
            TableRowCol[rowIndex1] = row2;
            TableRowCol[rowIndex2] = row1;
        }
        public void SwapColumns(int columnIndex1, int columnIndex2)
        {
            string[] tempCol1 = GetFullCol(columnIndex1);
            string[] tempCol2 = GetFullCol(columnIndex2);
            CopyToColRaw(columnIndex1, tempCol2);
            CopyToColRaw(columnIndex2, tempCol1);
        }
        public void MoveRow(int fromRowIndex, int toRowIndex)
        {
            // Adjust target index if needed
            if (toRowIndex > fromRowIndex)
                toRowIndex -= 1;

            // Copy raw row
            string[] row = GetFullRow(fromRowIndex);
            RemoveRow(fromRowIndex);
            InsertRow(toRowIndex);
            CopyToRowRaw(toRowIndex, row);
        }
        public void MoveCol(int fromColIndex, int toColIndex)
        {
            // Adjust target index if needed
            if (toColIndex > fromColIndex)
                toColIndex -= 1;

            string[] col = GetFullCol(fromColIndex);
            RemoveCol(fromColIndex);
            InsertCol(toColIndex);
            CopyToColRaw(toColIndex, col);
        }


        public static Table FromArea(string[][] cells, TableArea area)
        {
            Table table = new Table();
            
            // Create empty table to input data into
            table.TableRowCol = ArrayUtility.DefaultArray2D(string.Empty, area.NumberOfRows, area.NumberOfCols);
            // Copy subset of rows into table
            for (int rowIndex = area.BeginRow; rowIndex < area.EndRow; rowIndex++)
            {
                string[] row = cells[rowIndex][area.BeginX..area.EndX];
                table.CopyToRowRaw(rowIndex, row);
            }

            // Copy over some metadata
            table.RowHeadersCount = area.rowHeaderCount;
            table.ColHeadersCount = area.colHeaderCount;

            return table;
        }
    }
}
