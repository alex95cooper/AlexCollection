using System.Collections;

namespace AlexCollections
{
    internal class AlexQueueAndStackEnumerator<T> : IEnumerator<T>
    {
        private readonly int _leftGap;
        private readonly int _listSize;
        private readonly T[] _elementsArray;

        private int _counter;

        public AlexQueueAndStackEnumerator(T[] elementsArray, int leftGap, int listSize)
        {
            _elementsArray = elementsArray;
            _listSize = listSize;
            _leftGap = leftGap;
        }

        public T Current => _elementsArray[_leftGap + _listSize - _counter];

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
