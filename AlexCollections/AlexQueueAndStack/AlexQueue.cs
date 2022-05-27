using System.Collections;

namespace AlexCollections
{
    public class AlexQueue<T> : IEnumerable<T>
    {
        private int _leftGap;
        private int _count;
        private int _rightGap;
        private T[] _elementsArray;

        public AlexQueue()
        {
            _elementsArray = new T[100];
            _rightGap = _elementsArray.Length;
        }

        public int Count => _count;

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexQueueAndStackEnumerator<T>(_elementsArray, _count, _leftGap);
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
                ElementsArray<T>.ResizeArray(_elementsArray.Length + 100, Count, ref _elementsArray);
                _rightGap += 100;
                ShiftQueueToLeft();
            }
            else if (_rightGap == 0 && (_leftGap >= _elementsArray.Length / 10))
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
            return ElementsArray<T>.TryDoMethodIfCountNotNull(Peek, _count, out value);
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
            return ElementsArray<T>.TryDoMethodIfCountNotNull(Dequeue, _count, out value);
        }

        private void EnsureQueueNotEmpty()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
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
