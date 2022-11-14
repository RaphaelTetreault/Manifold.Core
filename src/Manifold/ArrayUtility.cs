using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manifold
{
    public static class ArrayUtility
    {
        public static T[] DefaultArray<T>(T defaultValue, int length)
        {
            var array = new T[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = defaultValue;
            }
            return array;
        }
        public static T[][] DefaultArray2D<T>(T defaultValue, int lengthDimension1, int lengthDimention2)
        {
            var array = new T[lengthDimension1][];
            for (int i = 0; i < lengthDimension1; i++)
            {
                array[i] = DefaultArray(defaultValue, lengthDimention2);
            }

            return array;
        }

        public static TStruct[] CopyArray<TStruct>(TStruct[] array)
            where TStruct : struct
        {
            var arrayCopy = new TStruct[array.Length];
            array.CopyTo(arrayCopy, 0);
            return arrayCopy;
        }

        public static T[] CopyCount<T>(T[] array, int index, int count)
        {
            T[] subselection = new T[count];
            for (int i = 0; i < count; i++)
                subselection[i] = array[index + i];
            return subselection;
        }
        public static T[] CopyRange<T>(T[] array, int indexFrom, int indexTo)
        {
            int count = indexTo - indexFrom + 1;
            T[] subselection = new T[count];
            for (int i = 0; i < count; i++)
            {
                subselection[i] = array[indexFrom + i];
            }
            return subselection;
        }
    }
}
