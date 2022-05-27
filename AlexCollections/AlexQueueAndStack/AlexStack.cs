using System.Collections;

namespace AlexCollections
{
    public class AlexStack<T> : IEnumerable<T>
    {
        private int _count;
        private T[] _elementsArray;

        public AlexStack()
        {
            _elementsArray = new T[100];
        }

        public int Count => _count;

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexQueueAndStackEnumerator<T>(_elementsArray, _count);
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
            EnsureStackNotEmpty();
            return _elementsArray[_count - 1];
        }

        public bool TryPeek(out T value)
        {
            return ElementsArray<T>.TryDoMethodIfCountNotNull(Peek, _count, out value);
        }

        public T Pop()
        {
            EnsureStackNotEmpty();

            T lastValue = _elementsArray[_count - 1];
            _elementsArray[_count - 1] = default;
            _count--;
            return lastValue;
        }

        public bool TryPop(out T value)
        {
            return ElementsArray<T>.TryDoMethodIfCountNotNull(Pop, _count, out value);
        }

        private void EnsureStackNotEmpty()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
        }
    }
}
