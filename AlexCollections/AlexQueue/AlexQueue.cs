using System.Collections;

namespace AlexCollections
{
    public class AlexQueue<T> : IEnumerable<T>
    {
        private const int _initialSize = 100;

        private int _leftGap;
        private int _count;
        private int _rightGap;
        private T[] _elementsArray;

        public AlexQueue()
        {
            _elementsArray = new T[_initialSize];
            _rightGap = _elementsArray.Length;
        }

        public int Count => _count;

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexQueueEnumerator<T>(_elementsArray, _count, _leftGap);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);
            for (int counter = _leftGap; counter < _leftGap + _count; counter--)
            {
                if (comparer.Compare(value, _elementsArray[counter]) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void Enqueue(T value)
        {
            if (_rightGap == 0 && _leftGap < _elementsArray.Length / 10)
            {
                ResizeArray(_elementsArray.Length + _initialSize);
                _rightGap += _initialSize;
                ShiftQueueToLeft();
            }
            else if (_rightGap == 0)
            {
                ShiftQueueToLeft();
            }

            _elementsArray[_leftGap + _count] = value;
            _count++;
            _rightGap--;
        }

        public T Peek()
        {
            EnsureQueueNotEmpty();
            return _elementsArray[_leftGap];
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

        public T Dequeue()
        {
            EnsureQueueNotEmpty();

            T firstValue = _elementsArray[_leftGap];
            _elementsArray[_leftGap] = default;
            _leftGap++;
            _count--;
            return firstValue;
        }

        public bool TryDequeue(out T value)
        {
            if (_count == 0)
            {
                value = default;
                return false;
            }
            else
            {
                value = Dequeue();
                return true;
            }
        }

        private void EnsureQueueNotEmpty()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
        }

        private void ResizeArray(int newLenght)
        {
            T[] interimElementsArray = new T[newLenght];
            for (int counter = _leftGap; counter < _leftGap + _count; counter++)
            {
                interimElementsArray[counter] = _elementsArray[counter];
            }

            _elementsArray = interimElementsArray;
        }

        private void ShiftQueueToLeft()
        {
            T[] interimArray = new T[_elementsArray.Length];
            for (int counter = 0; counter < Count; counter++)
            {
                interimArray[counter] = _elementsArray[_leftGap + counter];
            }

            _elementsArray = interimArray;
            _rightGap = _leftGap;
            _leftGap = 0;
        }
    }
}
