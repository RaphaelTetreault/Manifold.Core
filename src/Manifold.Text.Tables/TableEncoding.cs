using System;

namespace Manifold.Text.Tables
{
    public abstract class TableEncoding
    {
        public abstract string ColSeparator { get; }
        public abstract string RowSeparator { get; }
        public abstract string DefaultFileExtension { get; }
        public abstract string[] ExpectedFileExtensions { get; }

        public string[][] GetCellsFromLines(string[] lines, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
        {
            string[][] cells = new string[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
                cells[i] = lines[i].Split(ColSeparator, stringSplitOptions);

            return cells;
        }
        public string[] GetLinesFromText(string text, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
        {
            string[] lines = text.Split(RowSeparator, stringSplitOptions);
            return lines;
        }
        public string[][] GetCellsFromText(string text, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
        {
            string[] lines = GetLinesFromText(text, stringSplitOptions);
            string[][] cells = GetCellsFromLines(lines, stringSplitOptions);
            return cells;
        }
    }
}
