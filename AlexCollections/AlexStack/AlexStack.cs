using System.Collections;

namespace AlexCollections
{
    public class AlexStack<T> : IEnumerable<T>
    {
        private const int _initialSize = 100;

        private int _count;
        private T[] _elementsArray;

        public AlexStack()
        {
            _elementsArray = new T[_initialSize];
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
                _elementsArray = ArrayResizer<T>.Resize(_elementsArray.Length + _initialSize, _elementsArray);
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
            if (_count == 0)
            {
                value = default;
                return false;
            }
            else
            {
                value = Peek();
                return true;
            }
        }

        public T Pop()
        {
            EnsureStackNotEmpty();

            _count--;
            T lastValue = _elementsArray[_count];
            _elementsArray[_count] = default;
            return lastValue;
        }

        public bool TryPop(out T value)
        {
            if (_count == 0)
            {
                value = default;
                return false;
            }
            else
            {
                value = Pop();
                return true;
            }
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
