using System.Collections;

namespace AlexCollections
{
    internal class AlexQueueEnumerator<T> : IEnumerator<T>
    {
        private readonly int _leftGap;
        private readonly int _listSize;
        private readonly T[] _elementsArray;

        private int _counter;

        public AlexQueueEnumerator(T[] elementsArray, int listSize, int leftGap)
        {
            _elementsArray = elementsArray;
            _listSize = listSize;
            _leftGap = leftGap;
        }

        public T Current => _elementsArray[_leftGap + _counter];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_counter < _listSize)
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
            _counter = 0;
        }

        public void Dispose()
        {
        }
    }
}
