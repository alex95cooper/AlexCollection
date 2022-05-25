using System.Collections;

namespace AlexCollections
{
    public class AlexStackList<T> : IEnumerable<T>
    {
        private int _count;
        private T[] _elementsArray;

        public AlexStackList()
        {
            _elementsArray = new T[100];
        }

        public int Count => _count;

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexStackEnumerator<T>(_elementsArray, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);
            for (int counter = 0; counter < _count; counter++)
            {
                if (comparer.Compare(value, _elementsArray[counter]) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void Push(T value)
        {
            if (_count == _elementsArray.Length)
            {
                ElementsArray<T>.ResizeArray(_elementsArray.Length + 100, Count, ref _elementsArray);
            }

            _elementsArray[_count] = value;
            _count++;
        }

        public T Peek()
        {
            return _elementsArray[_count - 1];
        }

        public T Pop()
        {
            T lastValue = _elementsArray[_count - 1];
            _elementsArray[_count - 1] = default;
            _count--;
            return lastValue;
        }
    }
}
