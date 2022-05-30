using System.Collections;

namespace AlexCollections
{
    internal class AlexQueueEnumerator<T> : IEnumerator<T>
    {

        private readonly int _head;
        private readonly int _count;
        private readonly T[] _elementsArray;

        private int _elementCounter = -1;
        private int _indexCounter;

        public AlexQueueEnumerator(T[] elementsArray, int count, int head)
        {
            _elementsArray = elementsArray;
            _count = count;
            _head = head;
            _indexCounter = _head - 1;
        }

        public T Current => _elementsArray[_indexCounter];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_elementCounter == _count - 1)
            {
                Reset();
                return false;
            }

            if (_indexCounter == _elementsArray.Length - 1)
            {
                _indexCounter = -1;
            }

            _indexCounter++;
            _elementCounter++;
            return true;
        }

        public void Reset()
        {
            _elementCounter = -1;
            _indexCounter = _head - 1;
        }

        public void Dispose()
        {
        }
    }
}
