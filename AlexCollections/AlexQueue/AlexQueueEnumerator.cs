using System.Collections;

namespace AlexCollections
{
    internal class AlexQueueEnumerator<T> : IEnumerator<T>
    {
        private readonly int _head;
        private readonly int _tail;
        private readonly T[] _elementsArray;

        private int _counter;

        public AlexQueueEnumerator(T[] elementsArray, int head, int tail)
        {
            _elementsArray = elementsArray;
            _head = head;
            _tail = tail;
            _counter = _head - 1;
        }

        public T Current => _elementsArray[_counter];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_counter == _elementsArray.Length - 1)
            {
                _counter = -1;
            }

            if (_counter != _tail)
            {
                _counter++;
                return true;
            }
            else
            {
                Reset();
                return false;
            }
        }

        public void Reset()
        {
            _counter = _head - 1;
        }

        public void Dispose()
        {
        }
    }
}
