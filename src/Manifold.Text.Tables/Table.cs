using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace Manifold.Text.Tables
{
    public class Table
    {
        private const int DefaultSize = 32;
        public delegate string GetValueOnAxis(out bool isAtEndOfAxis);

        // Public state
        public int ActiveDataCol { get; private set; }
        public int ActiveDataRow { get; private set; }
        public int ColHeadersCount { get; set; }
        public int DataWidth { get; private set; }
        public int DataHeight { get; private set; }
        public int FullWidth => RowHeadersCount + DataWidth;
        public int FullHeight => ColHeadersCount + DataHeight;
        public TableAxis GetNextAxis { get; private set; } = TableAxis.Horizontal;
        public bool HasRowHeaders => RowHeadersCount > 0;
        public bool HasColHeaders => ColHeadersCount > 0;
        public string Name { get; set; } = string.Empty;
        public int RowHeadersCount { get; set; }
        public string[][] TableRowCol { get; private set; } = Array.Empty<string[]>();
        // Private state
        private bool CanAddVertically => DataHeight < InternalHeight;
        private bool CanAddHorizontally => DataWidth < InternalWidth;
        private GetValueOnAxis GetNextFromAxis { get; set; }
        private int InternalWidth => TableRowCol.GetLength(0);
        private int InternalHeight => TableRowCol.GetLength(1);


        // CONSTRUCTORS
        public Table()
        {
            HardReset();
            GetNextFromAxis = GetNextFromRow;
            SetTableGetNextAxis(GetNextAxis);
        }


        // METHODS
        // GET / SET
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
        public string[] GetDataColumn(int columnIndex)
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
        public string[] GetDataColumn(string colHeader)
        {
            for (int col = RowHeadersCount; col < DataWidth; col++)
            {
                for (int row = 0; row < ColHeadersCount; row++)
                {
                    string header = TableRowCol[row][col];
                    if (header != colHeader)
                        continue;

                    string[] colValues = GetDataColumn(col);
                    return colValues;
                }
            }

            string msg = $"No column with header value {colHeader}.";
            throw new KeyNotFoundException(msg);
        }
        public string[] GetDataRow(int rowIndex)
        {
            string[] row = TableRowCol[rowIndex];
            string[] rowData = row[RowHeadersCount..DataWidth];
            return rowData;
        }
        public string[] GetDataRow(string rowHeader)
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
        public string[] GetFullColumn(int columnIndex)
        {
            string[] col = new string[FullHeight];

            for (int rowIndex = 0; rowIndex < FullHeight; rowIndex++)
                col[rowIndex] = TableRowCol[rowIndex][columnIndex];

            return col;
        }
        public string[] GetFullRow(int rowIndex)
        {
            string[] row = TableRowCol[rowIndex];
            string[] rowData = row[0..DataWidth];
            return rowData;
        }
        public string GetHeaderColumn(int colIndex, int rowIndex = 0)
        {
            AssertHeaderIndex(rowIndex, colIndex);
            string header = TableRowCol[rowIndex][colIndex];
            return header;
        }
        public string GetHeaderRow(int rowIndex, int colIndex = 0)
        {
            AssertHeaderIndex(rowIndex, colIndex);
            string header = TableRowCol[rowIndex][colIndex];
            return header;
        }
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
        public string GetNextFromDataCol()
            => GetNextFromDataCol(out _);
        public string GetNextFromDataCol(out bool isAtEndOfCol)
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
        public T GetNextFromDataColAs<T>(Func<string, T> parse)
            => GetNextFromDataColAs(parse, out _);
        public T GetNextFromDataColAs<T>(Func<string, T> parse, out bool isAtEndOfCol)
        {
            string strValue = GetNextFromDataCol(out isAtEndOfCol);
            T value = parse.Invoke(strValue);
            return value;
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
        public void SetDataRow(int rowIndex, string[] rowValues)
        {
            rowValues.CopyTo(TableRowCol[rowIndex], RowHeadersCount);
        }
        public void SetHeaderRow(int rowIndex, string[] headers)
        {
            headers.CopyTo(TableRowCol[rowIndex], 0);
        }
        public void SetFullRow(int rowIndex, string[] fullRow)
        {
            fullRow.CopyTo(TableRowCol[rowIndex], 0);
        }
        public void SetRow(int rowIndex, string[] rowValues, params string[] headers)
        {
            SetHeaderRow(rowIndex, headers);
            SetDataRow(rowIndex, rowValues);
        }
        public void SetColumn(int colIndex, string[] colValues, params string[] headers)
        {
            SetHeaderColumn(colIndex, headers);
            SetDataColumn(colIndex, colValues);
        }
        public void SetDataColumn(int colIndex, string[] columnValues)
        {
            int srcRowIndex = 0;
            int dstRowIndex = ColHeadersCount;
            for (; dstRowIndex < FullHeight; srcRowIndex++, dstRowIndex++)
                TableRowCol[dstRowIndex][colIndex] = columnValues[srcRowIndex];
        }
        public void SetHeaderColumn(int colIndex, string[] headers)
        {
            for (int rowIndex = 0; rowIndex < headers.Length; rowIndex++)
                TableRowCol[rowIndex][colIndex] = headers[rowIndex];
        }
        public void SetFullColumn(int colIndex, string[] fullColumn)
        {
            for (int rowIndex = 0; rowIndex < FullHeight; rowIndex++)
                TableRowCol[rowIndex][colIndex] = fullColumn[rowIndex];
        }
        public void SetTableGetNextAxis(TableAxis tableAxis)
        {
            bool doesNotRequireUpdate = GetNextAxis == tableAxis;
            if (doesNotRequireUpdate)
                return;

            GetNextAxis = tableAxis;
            switch (tableAxis)
            {
                case TableAxis.Horizontal:
                    GetNextFromAxis = GetNextFromDataCol;
                    break;

                case TableAxis.Vertical:
                    GetNextFromAxis = GetNextFromRow;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
        // TABLE OPERATIONS
        public void AppendColumn()
        {
            string[] col = ArrayUtility.DefaultArray(string.Empty, InternalHeight);
            AppendColumn(col);
        }
        public void AppendColumn(string[] col, params string[] headers)
        {
            AssertColsCount(col.Length, headers.Length);

            if (!CanAddVertically)
                ExpandVertically();

            SetColumn(FullWidth, col, headers);
            DataWidth++;
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

            SetRow(FullHeight, row, headers);
            DataHeight++;
        }
        public void ClearAll()
        {
            TableRowCol = ArrayUtility.DefaultArray2D(string.Empty, InternalWidth, InternalHeight);
        }
        public void ClearData()
        {
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, DataWidth);
            for (int rowIndex = RowHeadersCount; rowIndex < FullHeight; rowIndex++)
                defaultArray.CopyTo(TableRowCol[rowIndex], RowHeadersCount);
        }
        public bool DataColumnContains(int colIndex, string value)
        {
            int index = DataColumnContainsAt(colIndex, value);
            bool doesContainValue = index > 0;
            return doesContainValue;
        }
        public int DataColumnContainsAt(int colIndex, string value)
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
        public bool DataRowContains(int rowIndex, string value)
        {
            int index = DataRowContainsAt(rowIndex, value);
            bool doesContainValue = index > 0;
            return doesContainValue;
        }
        public int DataRowContainsAt(int rowIndex, string value)
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
        public void HardReset()
        {
            TableRowCol = ArrayUtility.DefaultArray2D(string.Empty, DefaultSize, DefaultSize);
        }
        public void InsertColumn(int colIndex)
        {
            string[] col = ArrayUtility.DefaultArray(string.Empty, DataHeight);
            InsertColumn(colIndex, col);
        }
        public void InsertColumn(int colIndex, string[] colValues, params string[] headers)
        {
            AssertColsCount(colValues.Length, headers.Length);

            if (!CanAddHorizontally)
                ExpandHorizontally();

            for (int r = 0; r > FullHeight; r++) // each row, 0-n
                for (int c = FullWidth; c > colIndex; c--) // each col, n-colIndex
                    TableRowCol[r][c] = TableRowCol[r][c - 1]; // copy left cell right

            SetColumn(colIndex, colValues, headers);
            DataWidth++;
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

            SetRow(rowIndex, rowValues, headers);
            DataHeight++;
        }
        public void MoveColumn(int fromColIndex, int toColIndex)
        {
            // Adjust target index if needed
            if (toColIndex > fromColIndex)
                toColIndex -= 1;

            string[] col = GetFullColumn(fromColIndex);
            RemoveColumn(fromColIndex);
            InsertColumn(toColIndex);
            SetFullColumn(toColIndex, col);
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
            SetFullRow(toRowIndex, row);
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
        public bool RemoveColumn(int colIndex)
        {
            // Indicate if cannot remove col
            bool canRemoveItem = colIndex < FullWidth;
            if (canRemoveItem)
                return false;

            // Shift columns left from removed index
            for (int i = colIndex; i < FullWidth - 1; i++)
            {
                string[] nextCol = TableRowCol[i + 1];
                SetFullColumn(i, nextCol);
            }
            // Clear contents of final column
            string[] emptyColumn = ArrayUtility.DefaultArray(string.Empty, FullHeight);
            SetFullColumn(FullWidth, emptyColumn);

            // Update width
            DataWidth--;
            // Update header count
            bool isHeader = colIndex < RowHeadersCount;
            if (isHeader)
                RowHeadersCount--;

            return true;
        }
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
        public void SwapColumns(int columnIndex1, int columnIndex2)
        {
            string[] tempCol1 = GetFullColumn(columnIndex1);
            string[] tempCol2 = GetFullColumn(columnIndex2);
            SetFullColumn(columnIndex1, tempCol2);
            SetFullColumn(columnIndex2, tempCol1);
        }
        public void SwapRows(int rowIndex1, int rowIndex2)
        {
            string[] row1 = TableRowCol[rowIndex1];
            string[] row2 = TableRowCol[rowIndex2];
            TableRowCol[rowIndex1] = row2;
            TableRowCol[rowIndex2] = row1;
        }

        // PRIVATE
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


        public static Table FromArea(string[][] cells, TableArea area)
        {
            Table table = new Table();
            
            // Create empty table to input data into
            table.TableRowCol = ArrayUtility.DefaultArray2D(string.Empty, area.NumberOfRows, area.NumberOfCols);
            // Copy subset of rows into table
            for (int rowIndex = area.BeginRow; rowIndex < area.EndRow; rowIndex++)
            {
                string[] row = cells[rowIndex][area.BeginX..area.EndX];
                table.SetFullRow(rowIndex, row);
            }

            // Copy over some metadata
            table.RowHeadersCount = area.rowHeaderCount;
            table.ColHeadersCount = area.colHeaderCount;

            return table;
        }
    }
}
