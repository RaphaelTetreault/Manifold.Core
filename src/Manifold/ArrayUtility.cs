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
    }
}
