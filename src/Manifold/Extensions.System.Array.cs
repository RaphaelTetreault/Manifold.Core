using System;

namespace Manifold
{
    public static class ArrayExtensions
    {
        // https://stackoverflow.com/questions/8560106/isnullorempty-equivalent-for-array-c-sharp
        /// <summary>
        /// Indicates whether the specified array is null or has a length of zero.
        /// </summary>
        /// <param name="array">The array to test.</param>
        /// <returns>true if the array parameter is null or has a length of zero; otherwise, false.</returns>
        public static bool IsNullOrEmpty(this Array array)
        {
            return (array == null || array.Length == 0);
        }

        public static int LengthToFormat(this Array array)
        {
            var length = array.Length;
            var charWidth = length.ToString().Length;
            return charWidth;
        }

        public static string ArrayFormat(this int i, Array array)
        {
            var charWidth = LengthToFormat(array);
            return i.ToString().PadLeft(charWidth);
        }

        public static string PadLeft(this int i, int width, char paddingChar = ' ')
        {
            string formattedString = i.ToString().PadLeft(width, paddingChar);
            return formattedString;
        }

        public static void Populate<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }

        public static T[] CopyCount<T>(this T[] array, int index, int count)
            => ArrayUtility.CopyCount(array, index, count);
        public static T[] CopyRange<T>(this T[] array, int indexFrom, int indexTo)
            => ArrayUtility.CopyRange(array, indexFrom, indexTo);
    }
}
