using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.PortableExecutable;

// TODO Refactoring
//
//      row/col headers are separate "tables"
//      Each keeps track of own size, expands, etc
//      Row gets all columns, columns only get rows
//      
//      Remove all these get row/col functions. Bloat!
//      Especially in the functions files.
//
//      Table functions cross between these other tables
//      eg: insert/swap/etc rows does col header + main
//      eg: insert/swap/etc cols does row headers + main
//      Consider: should there even be row headers per-se?
//      Why not just treat as data? Convinience?
//
//      KISS: keep it simple: do we need header stuff like
//      this even? No, not really at all.


namespace Manifold.Text.Tables
{
    public sealed partial class Table
    {
        private const int DefaultSize = 32;
        public delegate string GetValueOnAxis(out bool isAtEndOfAxis);
        public delegate void SetValueOnAxis(string value);
        public delegate T ParseType<T>(string value);

        // Public state
        public int ActiveDataCol { get; private set; }
        public int ActiveDataRow { get; private set; }
        public int ColumnHeadersCount { get; set; }
        public int DataWidth { get; private set; }
        public int DataHeight { get; private set; }
        public int Width => RowHeadersCount + DataWidth;
        public int Height => ColumnHeadersCount + DataHeight;
        public TableAxis GetSetNextAxis { get; private set; } = TableAxis.Horizontal;
        public bool HasRowHeaders => RowHeadersCount > 0;
        public bool HasColumnHeaders => ColumnHeadersCount > 0;
        public string Name { get; set; } = string.Empty;
        public int RowHeadersCount { get; set; }
        public string[][] TableRowsAndColumns { get; private set; } = Array.Empty<string[]>();
        // Private state
        private GetValueOnAxis GetNextOnAxis { get; set; }
        private SetValueOnAxis SetNextOnAxis { get; set; }
        private int InternalWidth { get; set; } = DefaultSize;
        private int InternalHeight { get; set; } = DefaultSize;


        // CONSTRUCTORS
        public Table()
        {
            HardReset();
            GetNextOnAxis = GetNextInDataRow;
            SetNextOnAxis = SetNextInDataRow;
            SetTableNextAxis(GetSetNextAxis);
        }

        // GET
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

        public string[] GetColumn(int columnIndex)
        {
            int count = Height;
            string[] columnCells = GetColumn(columnIndex, count);
            return columnCells;
        }
        public string[] GetColumn(int columnIndex, int count)
        {
            string[] columnCells = GetColumn(columnIndex, count);
            return columnCells;
        }
        public string[] GetColumn(int columnIndex, int fromRowIndex, int toRowIndex)
        {
            // TODO: assert negatives

            int count = toRowIndex - fromRowIndex;
            string[] columnCells = new string[count];

            int destRow = 0;
            int srcRow = RowHeadersCount;
            while (destRow < count)
            {
                columnCells[destRow] = TableRowsAndColumns[srcRow][columnIndex];
                srcRow++;
                destRow++;
            }

            return columnCells;
        }
        public string[] GetColumn(string columnHeader)
        {
            for (int columnIndex = RowHeadersCount; columnIndex < Width; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < ColumnHeadersCount; rowIndex++)
                {
                    string header = TableRowsAndColumns[rowIndex][columnIndex];
                    if (header != columnHeader)
                        continue;

                    string[] colValues = GetColumnData(columnIndex);
                    return colValues;
                }
            }

            string msg = $"No column with header value {columnHeader}.";
            throw new KeyNotFoundException(msg);
        }
        public string[] GetColumnData(int columnIndex)
        {
            int count = Height - ColumnHeadersCount;
            string[] columnData = GetColumnData(columnIndex, count);
            return columnData;
        }
        public string[] GetColumnData(int columnIndex, int count)
        {
            int from = RowHeadersCount;
            int to = RowHeadersCount + count;
            string[] column = GetColumn(columnIndex, from, to);
            return column;
        }

        public string GetNextCell() => GetNextCell(out _);
        public string GetNextCell(out bool isAtEndOfAxis)
        {
            string value = GetNextOnAxis.Invoke(out isAtEndOfAxis);
            return value;
        }
        public T GetNextCellAs<T>(ParseType<T> parse) => GetNextCellAs(parse, out _);
        public T GetNextCellAs<T>(ParseType<T> parse, out bool isAtEndOfAxis)
        {
            string strValue = GetNextCell(out isAtEndOfAxis);
            T value = parse.Invoke(strValue);
            return value;
        }
        public void ResetActiveCell()
        {
            ActiveDataRow = ColumnHeadersCount;
            ActiveDataCol = RowHeadersCount;
        }

        public string[] GetRow(int rowIndex)
        {
            string[] row = TableRowsAndColumns[rowIndex];
            string[] rowData = row[0..Width];
            return rowData;
        }
        public string[] GetRow(int rowIndex, int cellCount)
        {
            int fromIndex = RowHeadersCount;
            int toIndex = RowHeadersCount + cellCount;
            string[] row = TableRowsAndColumns[rowIndex];
            string[] rowData = row[fromIndex..toIndex];
            return rowData;
        }
        public string[] GetRow(int rowIndex, int fromCelumnIndex, int toColumnIndex)
        {
            string[] row = TableRowsAndColumns[rowIndex];
            string[] rowData = row[fromCelumnIndex..toColumnIndex];
            return rowData;
        }
        public string[] GetRow(string rowHeader)
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


        // Internal GET stuff
        private string GetNextInDataColumn(out bool isAtEndOfColumn)
        {
            string value = GetCell(ActiveDataRow, ActiveDataCol);

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
        private string GetNextInDataRow(out bool isAtEndOfRow)
        {
            string value = GetCell(ActiveDataRow, ActiveDataCol);

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

        // Set
        public void SetColumn(int columnIndex, string[] columnData, int rowIndex = 0)
        {
            // Ensure cell is addressable
            int finalRowIndex = rowIndex + columnData.Length;
            CheckExpandVertically(finalRowIndex);
            AssertCellIndex(finalRowIndex, columnIndex);

            // Set column cells
            int rowIndexSrc = 0;
            int rowIndexDst = rowIndex;
            for (/* above */; rowIndexSrc < columnData.Length; rowIndexSrc++, rowIndexDst++)
                TableRowsAndColumns[rowIndexDst][columnIndex] = columnData[rowIndexSrc];
        }
        public void SetColumn(int columnIndex, string[] data, params string[] headers)
        {
            // Validation done in below functions
            SetColumnHeaders(columnIndex, headers);
            SetColumnData(columnIndex, data);
        }
        public void SetColumnData(int colIndex, string[] columnData)
        {
            // Validation done in below functions
            SetColumn(colIndex, columnData, RowHeadersCount);
        }
        private void SetColumnHeaders(int colIndex, string[] headers)
        {
            // Ensure cell is addressable
            CheckExpandVertically(headers.Length);
            AssertHeaderIndex(headers.Length, colIndex);
            // Set column cells
            for (int rowIndex = 0; rowIndex < headers.Length; rowIndex++)
                TableRowsAndColumns[rowIndex][colIndex] = headers[rowIndex];
        }

        public void SetRow(int rowIndex, string[] rowData, int columnIndex = 0)
        {
            // Ensure cell is addressable
            int finalColumnIndex = columnIndex + rowData.Length;
            CheckExpandHorizontally(finalColumnIndex);
            AssertCellIndex(rowIndex, finalColumnIndex);
            // Set row cells
            rowData.CopyTo(TableRowsAndColumns[rowIndex], columnIndex);
        }
        public void SetRow(int rowIndex, string[] rowData, params string[] headers)
        {
            // Validation done in below functions
            SetRowHeaders(rowIndex, headers);
            SetRowData(rowIndex, rowData);
        }
        public void SetRowData(int rowIndex, string[] rowData)
        {
            // Validation done in below functions
            SetRow(rowIndex, rowData, ColumnHeadersCount);
        }
        public void SetRowHeaders(int rowIndex, string[] headers)
        {
            // Ensure cell is addressable
            CheckExpandHorizontally(headers.Length);
            AssertHeaderIndex(rowIndex, headers.Length);
            // Set row cells
            headers.CopyTo(TableRowsAndColumns[rowIndex], 0);
        }

        public void LineFeed()
        {
            switch (GetSetNextAxis)
            {
                case TableAxis.Horizontal:
                    ActiveDataRow++;
                    ActiveDataCol = RowHeadersCount;
                    break;

                case TableAxis.Vertical:
                    ActiveDataRow = ColumnHeadersCount;
                    ActiveDataCol++;
                    break;

                default:
                    throw new NotImplementedException($"{GetSetNextAxis}");
            }
        }
        public void SetTableNextAxis(TableAxis tableAxis)
        {
            bool doesNotRequireUpdate = GetSetNextAxis == tableAxis;
            if (doesNotRequireUpdate)
                return;

            GetSetNextAxis = tableAxis;
            switch (tableAxis)
            {
                case TableAxis.Horizontal:
                    GetNextOnAxis = GetNextInDataColumn;
                    SetNextOnAxis = SetNextInDataColumn;
                    break;

                case TableAxis.Vertical:
                    GetNextOnAxis = GetNextInDataRow;
                    SetNextOnAxis = SetNextInDataRow;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
        public void SetCell(int rowIndex, int columnIndex, string value)
        {
            AssertCellIndex(rowIndex, columnIndex);
            TableRowsAndColumns[rowIndex][columnIndex] = value;
        }
        public void SetNextCell(object value) => SetNextCell(value.ToString()!);
        public void SetNextCell(string value)
        {
            SetNextOnAxis.Invoke(value);
        }
        private void SetNextInDataColumn(string value)
        {
            CheckExpandHorizontally();
            SetCell(ActiveDataRow, ActiveDataCol, value);
            ActiveDataRow++;
        }
        private void SetNextInDataRow(string value)
        {
            CheckExpandVertically();
            SetCell(ActiveDataRow, ActiveDataCol, value);
            ActiveDataCol++;
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

            CheckExpandHorizontally();
            SetColumn(Width, col, headers);
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

            CheckExpandVertically();
            SetRow(Height, row, headers);
            DataHeight++;
        }
        public void ClearAllCells()
        {
            TableRowsAndColumns = ArrayUtility.DefaultArray2D(string.Empty, InternalWidth, InternalHeight);
        }
        public void ClearDataCells()
        {
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, DataWidth);
            for (int rowIndex = RowHeadersCount; rowIndex < Height; rowIndex++)
                defaultArray.CopyTo(TableRowsAndColumns[rowIndex], RowHeadersCount);
        }
        public bool ColumnDataContains(int colIndex, string value)
        {
            int index = ColumnDataContainsAt(colIndex, value);
            bool doesContainValue = index > 0;
            return doesContainValue;
        }
        public int ColumnDataContainsAt(int colIndex, string value)
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
        public bool RowDataContains(int rowIndex, string value)
        {
            int index = RowDataContainsAt(rowIndex, value);
            bool doesContainValue = index > 0;
            return doesContainValue;
        }
        public int RowDataContainsAt(int rowIndex, string value)
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

            CheckExpandHorizontally();

            for (int r = 0; r > Height; r++) // each row, 0-n
                for (int c = Width; c > colIndex; c--) // each col, n-colIndex
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

            CheckExpandVertically();

            // shift rows at index down 1
            rowIndex += ColumnHeadersCount;
            for (int r = Height; r > rowIndex; r--)
                TableRowsAndColumns[r] = TableRowsAndColumns[r - 1];

            SetRow(rowIndex, rowValues, headers);
            DataHeight++;
        }
        public void MoveColumn(int fromColIndex, int toColIndex)
        {
            // Adjust target index if needed
            if (toColIndex > fromColIndex)
                toColIndex -= 1;

            string[] col = GetColumn(fromColIndex);
            RemoveColumn(fromColIndex);
            InsertColumn(toColIndex);
            SetColumn(toColIndex, col);
        }
        public void MoveRow(int fromRowIndex, int toRowIndex)
        {
            // Adjust target index if needed
            if (toRowIndex > fromRowIndex)
                toRowIndex -= 1;

            // Copy raw row
            string[] row = GetRow(fromRowIndex);
            RemoveRow(fromRowIndex);
            InsertRow(toRowIndex);
            SetRow(toRowIndex, row);
        }
        public void PivotTable()
        {
            // Flip axes
            string[][] pivotedTable = new string[InternalHeight][];

            // Take columns from old table, insert into new table
            for (int colIndex = 0; colIndex < Width; colIndex++)
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
            bool canRemoveItem = colIndex < Width;
            if (canRemoveItem)
                return false;

            // Shift columns left from removed index
            for (int i = colIndex; i < Width - 1; i++)
            {
                string[] nextCol = TableRowsAndColumns[i + 1];
                SetColumn(i, nextCol);
            }
            // Clear contents of final column
            string[] emptyColumn = ArrayUtility.DefaultArray(string.Empty, Height);
            SetColumn(Width, emptyColumn);

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
            bool canRemoveItem = rowIndex < Height;
            if (canRemoveItem)
                return false;

            // Shift rows down
            for (int y = rowIndex; y < Height - 1; y++)
                TableRowsAndColumns[y] = TableRowsAndColumns[y + 1];
            // Replace final item with empty
            TableRowsAndColumns[Height] = ArrayUtility.DefaultArray(string.Empty, InternalWidth);

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
            string[] tempCol1 = GetColumn(columnIndex1);
            string[] tempCol2 = GetColumn(columnIndex2);
            SetColumn(columnIndex1, tempCol2);
            SetColumn(columnIndex2, tempCol1);
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
            bool invalidColumn = colIndex >= InternalWidth;
            bool invalidRow = rowIndex >= InternalHeight;
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
        private void CheckExpandVertically(int count = 1)
        {
            bool cannotFit = (Height + count) > InternalHeight;
            if (cannotFit)
                ExpandVertically();
        }
        private void CheckExpandHorizontally(int count = 1)
        {
            bool cannotFit = (Width + count) > InternalWidth;
            if (cannotFit)
                ExpandHorizontally();
        }
        private void ExpandHorizontally()
        {
            // Calculate new width
            InternalWidth *= 2;
            // Double horizontal size of each row
            for (int y = 0; y < InternalHeight; y++)
            {
                string[] row = TableRowsAndColumns[y];
                string[] newRow = ArrayUtility.DefaultArray(string.Empty, InternalWidth);
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
            InternalHeight = newHeight;
            TableRowsAndColumns = new string[newHeight][];
            // Copy old values into new array
            valueRows.CopyTo(TableRowsAndColumns, 0);
            // Create new entries
            string[] defaultArray = ArrayUtility.DefaultArray(string.Empty, InternalWidth);
            for (int y = oldHeight; y < newHeight; y++)
                TableRowsAndColumns[y] = defaultArray;
        }


        public static Table FromArea(string[][] cells, TableArea area)
        {
            Table table = new Table();

            // Create empty table to input data into
            table.TableRowsAndColumns = ArrayUtility.DefaultArray2D(string.Empty, area.NumberOfRows, area.NumberOfCols);
            // Copy subset of rows into table
            for (int rowIndex = area.BeginRow; rowIndex < area.EndRow; rowIndex++)
            {
                string[] row = cells[rowIndex][area.BeginColumn..area.EndColumn];
                table.SetRow(rowIndex, row);
            }

            // Copy over some metadata
            table.RowHeadersCount = area.rowHeaderCount;
            table.ColumnHeadersCount = area.colHeaderCount;
            table.Name = area.name;

            return table;
        }
        public static Table FromCells(string[][] cells)
        {
            // Find dimensions of table
            int rowCount = cells.Length;
            int colCount = 0;
            for (int row = 0; row < cells.GetLength(0); row++)
                colCount = cells[row].Length > colCount ? cells[row].Length : colCount;

            // Set dimensions
            Table table = new Table();
            table.DataWidth = rowCount;
            table.DataHeight = colCount;

            // Create underlying cells in table, set values
            table.TableRowsAndColumns = ArrayUtility.DefaultArray2D(string.Empty, rowCount, colCount);
            for (int i = 0; i < rowCount; i++)
                cells.CopyTo(table.TableRowsAndColumns, 0);

            return table;
        }
    }
}
