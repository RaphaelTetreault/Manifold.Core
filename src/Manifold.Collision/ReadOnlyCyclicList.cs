using System.Collections.ObjectModel;

namespace Manifold.Geometry
{
    internal class ReadOnlyCyclicList<T> :
        //IEnumerable<T>
        ReadOnlyCollection<T>
    {
        private readonly int lastIndex;
        private readonly ReadOnlyCollection<int> nextIndexes;
        private readonly ReadOnlyCollection<int> prevIndexes;


        public ReadOnlyCyclicList(IList<T> list) : base (list)
        {
            int count = list.Count;
            int lastIndex = count - 1;

            var _prevIndexes = new int[count];
            var _nextIndexes = new int[count];
            for (int i = 0; i < count; i++)
            {
                _prevIndexes[i] = i - 1;
                _nextIndexes[i] = i + 1;
            }
            _prevIndexes[0] = lastIndex;
            _nextIndexes[lastIndex] = 0;
            prevIndexes = new ReadOnlyCollection<int>(_prevIndexes);
            nextIndexes = new ReadOnlyCollection<int>(_nextIndexes);
            this.lastIndex = lastIndex;
        }


        public int CurrentIndex { get; set; }
        public int PrevIndex => prevIndexes[CurrentIndex];
        public int NextIndex => nextIndexes[CurrentIndex];
        public int LastIndex => lastIndex;


        public T GetAndMoveNext()
        {
            T value = this[CurrentIndex];
            MoveNext();
            return value;
        }

        public T GetAndMovePrev()
        {
            T value = this[CurrentIndex];
            MovePrev();
            return value;
        }

        public void MoveNext()
        {
            CurrentIndex = NextIndex;
        }

        public void MovePrev()
        {
            CurrentIndex = PrevIndex;
        }

    }
}
