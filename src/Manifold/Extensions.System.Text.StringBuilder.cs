using System.Text;

namespace Manifold
{
    public static class StringBuilderExtension
    {
        private static string PrintNullValue<T>(T value)
        {
            return $"{typeof(T).Name} (null)";
        }

        public static void AppendMultiLineIndented<TextPrintable>(this StringBuilder stringBuilder, string indent, int indentLevel, TextPrintable textPrintable)
            where TextPrintable : ITextPrintable
        {
            if (textPrintable is null)
                stringBuilder.AppendLineIndented(indent, indentLevel, PrintNullValue(textPrintable));
            else
                textPrintable.PrintMultiLine(stringBuilder, indentLevel, indent);
        }

        public static void AppendMultiLineIndented<TextPrintable>(this StringBuilder stringBuilder, string indent, int indentLevel, TextPrintable[] textPrintables)
            where TextPrintable : ITextPrintable
        {
            if (textPrintables is null)
            {
                stringBuilder.AppendLineIndented(indent, indentLevel, PrintNullValue(textPrintables));
            }
            else
            {
                string typeName = typeof(TextPrintable).Name;
                string arrayPrefix = $"{typeName}[{textPrintables.Length}]";
                int index = 0;

                stringBuilder.AppendLineIndented(indent, indentLevel, arrayPrefix);
                foreach (var value in textPrintables)
                {
                    string prefix = $"{typeName}[{index++}]";
                    stringBuilder.AppendLineIndented(indent, indentLevel + 1, prefix);
                    value.PrintMultiLine(stringBuilder, indentLevel + 1, indent);
                }
            }
        }

        public static void AppendIndented(this StringBuilder stringBuilder, string indent, int indentLevel, string value)
        {
            stringBuilder.AppendRepeat(indent, indentLevel);
            stringBuilder.Append(value);
        }

        public static void AppendIndented(this StringBuilder stringBuilder, char indent, int indentLevel, string value)
        {
            stringBuilder.AppendRepeat(indent, indentLevel);
            stringBuilder.Append(value);
        }

        /// <summary>
        /// Appends <paramref name="indent"/> as many times as specified by <paramref name="indentLevel"/>
        /// </summary>
        /// <param name="stringBuilder">The string builder to use.</param>
        /// <param name="indent">The value to use for indenting.</param>
        /// <param name="indentLevel">How many times to append the indent character.</param>
        /// <param name="value">The value to append.</param>
        public static void AppendLineIndented(this StringBuilder stringBuilder, string indent, int indentLevel, string value)
        {
            stringBuilder.AppendRepeat(indent, indentLevel);
            stringBuilder.AppendLine(value);
        }


        //public static void AppendMultilineIndented<TextPrintable>(this StringBuilder stringBuilder, string indent, int indentLevel, TextPrintable textPrintable)
        //    where TextPrintable : ITextPrintable
        //{
        //    if (textPrintable is null)
        //        stringBuilder.AppendLineIndented(indent, indentLevel, PrintNullValue(textPrintable));
        //    else
        //        textPrintable.PrintMultiLine(stringBuilder, indentLevel, indent);
        //}





        /// <summary>
        /// Appends <paramref name="indent"/> as many times as specified by <paramref name="indentLevel"/>
        /// </summary>
        /// <param name="stringBuilder">The string builder to use.</param>
        /// <param name="indent">The value to use for indenting.</param>
        /// <param name="indentLevel">How many times to append the indent character.</param>
        /// <param name="value">The value to append.</param>
        public static void AppendLineIndented(this StringBuilder stringBuilder, char indent, int indentLevel, string value)
        {
            stringBuilder.AppendRepeat(indent, indentLevel);
            stringBuilder.AppendLine(value);
        }

        /// <summary>
        /// Appends <paramref name="value"/> as many times as specified by <paramref name="repetitions"/>
        /// </summary>
        /// <param name="stringBuilder">The string builder to use.</param>
        /// <param name="value">The value to append.</param>
        /// <param name="repetitions">How many times to append the value.</param>
        public static void AppendRepeat(this StringBuilder stringBuilder, string value, int repetitions)
        {
            for (int i = 0; i < repetitions; i++)
            {
                stringBuilder.Append(value);
            }
        }

        /// <summary>
        /// Appends <paramref name="value"/> as many times as specified by <paramref name="repetitions"/>
        /// </summary>
        /// <param name="stringBuilder">The string builder to use.</param>
        /// <param name="value">The value to append.</param>
        /// <param name="repetitions">How many times to append the value.</param>
        public static void AppendRepeat(this StringBuilder stringBuilder, char value, int repetitions)
        {
            for (int i = 0; i < repetitions; i++)
            {
                stringBuilder.Append(value);
            }
        }

    }
}
