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
        }

        public int Count => _count;

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexQueueEnumerator<T>(_elementsArray, _head, _tail);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            int counter = _head;
            while (counter != _tail + 1)
            {
                if (counter == _elementsArray.Length)
                {
                    counter = 0;
                }

                if (comparer.Compare(value, _elementsArray[counter]) == 0)
                {
                    return true;
                }

                counter++;
            }

            return false;
        }

        public void Enqueue(T value)
        {
            if (_count == 0)
            {
                _tail = _head = 0;
            }
            else if (_count < _elementsArray.Length - 1)
            {
                if (_tail == _elementsArray.Length)
                {
                    _tail = 0;
                }
                else
                {
                    _tail++;
                }
            }
            else if (_count == _elementsArray.Length - 1)
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
            if (_head == _elementsArray.Length - 1)
            {
                _head = 0;
            }
            else
            {
                _head++;
            }
           
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

            int interimCounter = 0;
            int counter = _head;
            while (counter != _tail + 1)
            {
                if (counter == _elementsArray.Length)
                {
                    counter = 0;
                }

                interimElementsArray[interimCounter] = _elementsArray[counter];
                interimCounter++;
                counter++;
            }

            _elementsArray = interimElementsArray;
            _head = 0;
            _tail = _count - 1;
        }
    }
}