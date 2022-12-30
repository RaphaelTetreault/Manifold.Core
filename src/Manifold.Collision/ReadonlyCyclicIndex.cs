using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manifold.Geometry
{
    public class ReadonlyCyclicIndex
    {
        private readonly int lastIndex;
        private readonly ReadOnlyCollection<int> prevIndexes;
        private readonly ReadOnlyCollection<int> nextIndexes;

        public ReadonlyCyclicIndex(int length)
        {
            lastIndex = length - 1;
            var _prevIndexes = new int[length];
            var _nextIndexes = new int[length];
            for (int i = 0; i < length; i++)
            {
                _prevIndexes[i] = i - 1;
                _nextIndexes[i] = i + 1;
            }
            _prevIndexes[0] = lastIndex;
            _nextIndexes[lastIndex] = 0;
            prevIndexes = new ReadOnlyCollection<int>(_prevIndexes);
            nextIndexes = new ReadOnlyCollection<int>(_nextIndexes);
        }

        public int CurrentIndex { get; set; }
        public int PreviousIndex => prevIndexes[CurrentIndex];
        public int NextIndex => nextIndexes[CurrentIndex];
        public int LastIndex => lastIndex;

        public int PostIncrement()
        {
            var index = CurrentIndex;
            CurrentIndex = NextIndex;
            return index;
        }
        public int PostDecrement()
        {
            var index = CurrentIndex;
            CurrentIndex = PreviousIndex;
            return index;
        }

        public static ReadonlyCyclicIndex operator ++(ReadonlyCyclicIndex value)
        {
            value.CurrentIndex = value.NextIndex;
            return value;
        }

        public static ReadonlyCyclicIndex operator --(ReadonlyCyclicIndex value)
        {
            value.CurrentIndex = value.PreviousIndex;
            return value;
        }

        //public static ReadonlyCyclicIndex operator +(ReadonlyCyclicIndex value, int number)
        //{
        //    if (number < 0)
        //        number = -number;

        //    if (number >= )
        //}

        //public static ReadonlyCyclicIndex operator -(ReadonlyCyclicIndex value, int number)
        //{
        //    if (number < 0)
        //        number = -number;

        //    return value + number;
        //}

    }
}
