using System.Collections;

namespace AlexCollections
{
    public class AlexQueue<T> : IEnumerable<T>
    {
        private const int InitialSize = 100;

        private int _head;
        private int _count;
        private int _tail;
        private T[] _elementsArray;

        public AlexQueue()
        {
            _elementsArray = new T[InitialSize];
            _tail = -1;
        }

        public int Count => _count;

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexQueueEnumerator<T>(_elementsArray, _count, _head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            int indexCounter = _head;
            for (int elementCounter = 0; elementCounter < _count; elementCounter++)
            {
                if (indexCounter == _elementsArray.Length)
                {
                    indexCounter = 0;
                }

                if (comparer.Compare(value, _elementsArray[indexCounter]) == 0)
                {
                    return true;
                }

                indexCounter++;
            }

            return false;
        }

        public void Enqueue(T value)
        {
            if (_count < _elementsArray.Length)
            {
                _tail = (_tail == _elementsArray.Length) ? 0 : _tail + 1;
            }
            else if (_count == _elementsArray.Length)
            {
                ResizeArray(_elementsArray.Length + InitialSize);
                Enqueue(value);
            }

            _elementsArray[_tail] = value;
            _count++;
        }

        public T Peek()
        {
            EnsureQueueNotEmpty();
            return _elementsArray[_head];
        }

        public bool TryPeek(out T value)
        {
            if (_count == 0)
            {
                value = default;
                return false;
            }

            value = Peek();
            return true;
        }

        public T Dequeue()
        {
            EnsureQueueNotEmpty();

            T firstValue = _elementsArray[_head];
            _elementsArray[_head] = default;
            _head = (_head == _elementsArray.Length - 1) ? 0 : _head + 1;
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

            value = Dequeue();
            return true;
        }

        private void EnsureQueueNotEmpty()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
        }

        private void ResizeArray(int newLength)
        {
            T[] interimElementsArray = new T[newLength];

            int indexCounter = _head;
            for (int elementCounter = 0; elementCounter < _count; elementCounter++)
            {
                if (indexCounter == _elementsArray.Length)
                {
                    indexCounter = 0;
                }

                interimElementsArray[elementCounter] = _elementsArray[indexCounter];
                indexCounter++;
            }

            _elementsArray = interimElementsArray;
            _head = 0;
            _tail = _count - 1;
        }
    }
}