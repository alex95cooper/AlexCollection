using System.Collections;

namespace AlexCollections
{
    public class AlexQueueList<T> : IEnumerable<T>
    {
        private int _leftGap;
        private int _count;
        private int _rightGap;
        private T[] _elementsArray;

        public AlexQueueList()
        {
            _elementsArray = new T[100];
            _leftGap = _elementsArray.Length;
        }

        public int Count => _count;

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexQueueAndStackEnumerator<T>(_elementsArray, _leftGap, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            int indexOfFirstElement = _elementsArray.Length - (_rightGap + 1);
            for (int counter = indexOfFirstElement; counter > _leftGap - 1; counter--)
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
            if (_leftGap == 0 && _rightGap < _elementsArray.Length / 10)
            {
                ElementsArray<T>.ResizeArray(_elementsArray.Length + 100, Count, ref _elementsArray);
            }
            else if (_leftGap == 0 && (_rightGap >= _elementsArray.Length / 10))
            {
                ShiftListToRight();
            }

            int indexofElement = _elementsArray.Length - (_rightGap + _count + 1);
            _elementsArray[indexofElement] = value;
            _count++;
            _leftGap--;
        }

        public T Peek()
        {
            EnsureQueueNotEmpty();
            return _elementsArray[^(_rightGap + 1)];
        }

        public bool TryPeek(out T value)
        {
            return ElementsArray<T>.TryDoMethodIfCountNotNull(Peek, _count, out value);
        }

        public T Dequeue()
        {
            EnsureQueueNotEmpty();

            T firstValue = _elementsArray[^(_rightGap + 1)];
            _elementsArray[^(_rightGap + 1)] = default;
            _rightGap++;
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

        private void ShiftListToRight()
        {
            T[] interimArray = new T[_elementsArray.Length];
            for (int counter = _elementsArray.Length - 1; counter > _rightGap; counter--)
            {
                interimArray[counter] = _elementsArray[counter - _rightGap];
            }

            _elementsArray = interimArray;
            _leftGap = _rightGap;
            _rightGap = 0;
        }
    }
}
