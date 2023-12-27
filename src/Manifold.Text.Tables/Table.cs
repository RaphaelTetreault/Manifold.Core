using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace Manifold.Text.Tables
{
    public sealed partial class Table
    {
        private const int DefaultSize = 32;
        public delegate string GetValueOnAxis(out bool isAtEndOfAxis);
        public delegate T ParseType<T>(string value);

        // Public state
        public int ActiveDataCol { get; private set; }
        public int ActiveDataRow { get; private set; }
        public int ColumnHeadersCount { get; set; }
        public int DataWidth { get; private set; }
        public int DataHeight { get; private set; }
        public int FullWidth => RowHeadersCount + DataWidth;
        public int FullHeight => ColumnHeadersCount + DataHeight;
        public TableAxis GetNextAxis { get; private set; } = TableAxis.Horizontal;
        public bool HasRowHeaders => RowHeadersCount > 0;
        public bool HasColumnHeaders => ColumnHeadersCount > 0;
        public string Name { get; set; } = string.Empty;
        public int RowHeadersCount { get; set; }
        public string[][] TableRowsAndColumns { get; private set; } = Array.Empty<string[]>();
        // Private state
        private bool CanAddVertically => DataHeight < InternalHeight;
        private bool CanAddHorizontally => DataWidth < InternalWidth;
        private GetValueOnAxis GetNextFromAxis { get; set; }
        private int InternalWidth => TableRowsAndColumns.GetLength(0);
        private int InternalHeight => TableRowsAndColumns.GetLength(1);


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
            string value = TableRowsAndColumns[rowIndex][columnIndex];
            return value;
        }
        public T GetCellAs<T>(int rowIndex, int columnIndex, ParseType<T> parse)
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
            columnIndex += ColumnHeadersCount;
            string value = TableRowsAndColumns[rowIndex][columnIndex];
            return value;
        }
        public T GetDataCellAs<T>(int rowIndex, int columnIndex, ParseType<T> parse)
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
                col[destRow] = TableRowsAndColumns[srcRow][columnIndex];
                srcRow++;
                destRow++;
            }

            return col;
        }
        public string[] GetDataColumn(string columnHeader)
        {
            for (int col = RowHeadersCount; col < DataWidth; col++)
            {
                for (int row = 0; row < ColumnHeadersCount; row++)
                {
                    string header = TableRowsAndColumns[row][col];
                    if (header != columnHeader)
                        continue;

                    string[] colValues = GetDataColumn(col);
                    return colValues;
                }
            }

            string msg = $"No column with header value {columnHeader}.";
            throw new KeyNotFoundException(msg);
        }
        public string[] GetDataRow(int rowIndex)
        {
            string[] row = TableRowsAndColumns[rowIndex];
            string[] rowData = row[RowHeadersCount..DataWidth];
            return rowData;
        }
        public string[] GetDataRow(string rowHeader)
        {
            foreach (string[] row in TableRowsAndColumns)
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
                col[rowIndex] = TableRowsAndColumns[rowIndex][columnIndex];

            return col;
        }
        public string[] GetFullRow(int rowIndex)
        {
            string[] row = TableRowsAndColumns[rowIndex];
            string[] rowData = row[0..DataWidth];
            return rowData;
        }
        public string GetHeaderColumn(int columnIndex, int rowIndex = 0)
        {
            AssertHeaderIndex(rowIndex, columnIndex);
            string header = TableRowsAndColumns[rowIndex][columnIndex];
            return header;
        }
        public string GetHeaderRow(int rowIndex, int columnIndex = 0)
        {
            AssertHeaderIndex(rowIndex, columnIndex);
            string header = TableRowsAndColumns[rowIndex][columnIndex];
            return header;
        }
        public string GetNext()
        => GetNext(out _);
        public string GetNext(out bool isAtEndOfAxis)
        {
            string value = GetNextFromAxis.Invoke(out isAtEndOfAxis);
            return value;
        }
        public T GetNextAs<T>(ParseType<T> parse)
            => GetNextAs(parse, out _);
        public T GetNextAs<T>(ParseType<T> parse, out bool isAtEndOfAxis)
        {
            string strValue = GetNext(out isAtEndOfAxis);
            T value = parse.Invoke(strValue);
            return value;
        }
        public string GetNextFromDataColumn()
            => GetNextFromDataColumn(out _);
        public string GetNextFromDataColumn(out bool isAtEndOfColumn)
        {
            string value = GetDataCell(ActiveDataRow, ActiveDataCol);

            isAtEndOfColumn = false;
            ActiveDataCol++;
            if (ActiveDataCol >= DataWidth)
            {
                ActiveDataCol = RowHeadersCount;
                ActiveDataRow++;
                isAtEndOfColumn = true;
            }

            return value;
        }
        public T GetNextFromDataColumnAs<T>(ParseType<T> parse)
            => GetNextFromDataColumnAs(parse, out _);
        public T GetNextFromDataColumnAs<T>(ParseType<T> parse, out bool isAtEndOfColumn)
        {
            string strValue = GetNextFromDataColumn(out isAtEndOfColumn);
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
                ActiveDataRow = ColumnHeadersCount;
                ActiveDataCol++;
                isAtEndOfRow = true;
            }

            return value;
        }
        public T GetNextFromDataRowAs<T>(ParseType<T> parse)
            => GetNextFromDataRowAs(parse, out _);
        public T GetNextFromDataRowAs<T>(ParseType<T> parse, out bool isAtEndOfRow)
        {
            string strValue = GetNextFromRow(out isAtEndOfRow);
            T value = parse.Invoke(strValue);
            return value;
        }
        public void SetDataRow(int rowIndex, string[] data)
        {
            data.CopyTo(TableRowsAndColumns[rowIndex], RowHeadersCount);
        }
        public void SetHeaderRow(int rowIndex, string[] headers)
        {
            headers.CopyTo(TableRowsAndColumns[rowIndex], 0);
        }
        public void SetFullRow(int rowIndex, string[] fullRow)
        {
            fullRow.CopyTo(TableRowsAndColumns[rowIndex], 0);
        }
        public void SetRow(int rowIndex, string[] data, params string[] headers)
        {
            SetHeaderRow(rowIndex, headers);
            SetDataRow(rowIndex, data);
        }
        public void SetColumn(int columnIndex, string[] data, params string[] headers)
        {
            SetHeaderColumn(columnIndex, headers);
            SetDataColumn(columnIndex, data);
        }
        public void SetDataColumn(int columnIndex, string[] data)
        {
            int srcRowIndex = 0;
            int dstRowIndex = ColumnHeadersCount;
            for (; dstRowIndex < FullHeight; srcRowIndex++, dstRowIndex++)
                TableRowsAndColumns[dstRowIndex][columnIndex] = data[srcRowIndex];
        }
        public void SetHeaderColumn(int colIndex, string[] headers)
        {
            for (int rowIndex = 0; rowIndex < headers.Length; rowIndex++)
                TableRowsAndColumns[rowIndex][colIndex] = headers[rowIndex];
        }
        public void SetFullColumn(int colIndex, string[] fullColumn)
        {
            for (int rowIndex = 0; rowIndex < FullHeight; rowIndex++)
                TableRowsAndColumns[rowIndex][colIndex] = fullColumn[rowIndex];
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
                    GetNextFromAxis = GetNextFromDataColumn;
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
            TableRowsAndColumns = ArrayUtility.DefaultArray2D(string.Empty, InternalWidth, InternalHeight);
        }
        public void ClearData()
        {
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, DataWidth);
            for (int rowIndex = RowHeadersCount; rowIndex < FullHeight; rowIndex++)
                defaultArray.CopyTo(TableRowsAndColumns[rowIndex], RowHeadersCount);
        }
        public bool DataColumnContains(int colIndex, string value)
        {
            int index = DataColumnContainsAt(colIndex, value);
            bool doesContainValue = index > 0;
            return doesContainValue;
        }
        public int DataColumnContainsAt(int colIndex, string value)
        {
            for (int rowIndex = ColumnHeadersCount; rowIndex < DataHeight; rowIndex++)
            {
                string rowValue = TableRowsAndColumns[rowIndex][colIndex];
                bool isSame = rowValue == value;
                if (isSame)
                {
                    int index = rowIndex - ColumnHeadersCount;
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
                string colValue = TableRowsAndColumns[rowIndex][colIndex];
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
            TableRowsAndColumns = ArrayUtility.DefaultArray2D(string.Empty, DefaultSize, DefaultSize);
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
                    TableRowsAndColumns[r][c] = TableRowsAndColumns[r][c - 1]; // copy left cell right

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
            rowIndex += ColumnHeadersCount;
            for (int r = FullHeight; r > rowIndex; r--)
                TableRowsAndColumns[r] = TableRowsAndColumns[r - 1];

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
                string[] column = TableRowsAndColumns[colIndex];
                pivotedTable[colIndex] = column;
            }

            // Swap header counts
            int tempColHeadersCount = ColumnHeadersCount;
            ColumnHeadersCount = RowHeadersCount;
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
                string[] nextCol = TableRowsAndColumns[i + 1];
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
                TableRowsAndColumns[y] = TableRowsAndColumns[y + 1];
            // Replace final item with empty
            TableRowsAndColumns[FullHeight] = ArrayUtility.DefaultArray(string.Empty, InternalWidth);

            // Update height
            DataHeight--;
            // Update header count
            bool isHeaderRow = rowIndex < ColumnHeadersCount;
            if (isHeaderRow)
                ColumnHeadersCount--;

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
            string[] row1 = TableRowsAndColumns[rowIndex1];
            string[] row2 = TableRowsAndColumns[rowIndex2];
            TableRowsAndColumns[rowIndex1] = row2;
            TableRowsAndColumns[rowIndex2] = row1;
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
            bool invalidColumn = colIndex < ColumnHeadersCount;
            bool invalidRow = rowIndex < RowHeadersCount;
            if (invalidRow || invalidColumn)
            {
                string msg = $"Invalid data cell index [{rowIndex},{colIndex}]";
                throw new ArgumentOutOfRangeException(msg);
            }
        }
        private void AssertHeaderIndex(int rowIndex, int colIndex)
        {
            bool invalidColumn = colIndex >= ColumnHeadersCount;
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
                string[] row = TableRowsAndColumns[y];
                string[] newRow = ArrayUtility.DefaultArray(string.Empty, newWidth);
                row.CopyTo(newRow, 0);
                TableRowsAndColumns[y] = newRow;
            }
        }
        private void ExpandVertically()
        {
            // Get reference to existing array
            var valueRows = TableRowsAndColumns;
            // Double vertical size
            int oldHeight = InternalHeight;
            int newHeight = InternalHeight * 2;
            TableRowsAndColumns = new string[newHeight][];
            // Copy old values into new array
            valueRows.CopyTo(TableRowsAndColumns, 0);
            // Create new entries
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, InternalWidth);
            for (int y = oldHeight; y < newHeight; y++)
                TableRowsAndColumns[y] = defaultArray;
        }


        public static Table FromArea(string[][] cells, TableArea area, string name = "")
        {
            Table table = new Table();
            
            // Create empty table to input data into
            table.TableRowsAndColumns = ArrayUtility.DefaultArray2D(string.Empty, area.NumberOfRows, area.NumberOfCols);
            // Copy subset of rows into table
            for (int rowIndex = area.BeginRow; rowIndex < area.EndRow; rowIndex++)
            {
                string[] row = cells[rowIndex][area.BeginX..area.EndX];
                table.SetFullRow(rowIndex, row);
            }

            // Copy over some metadata
            table.RowHeadersCount = area.rowHeaderCount;
            table.ColumnHeadersCount = area.colHeaderCount;
            table.Name = name;

            return table;
        }
    }
}
