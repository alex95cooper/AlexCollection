using System.Collections;

namespace AlexCollections
{
    internal class AlexStackEnumerator<T> : IEnumerator<T>
    {
        private readonly int _listSize;
        private readonly T[] _elementsArray;

        private int _counter = 0;

        public AlexStackEnumerator(T[] elementsArray, int listSize)
        {
            _elementsArray = elementsArray;
            _listSize = listSize;
        }

        public T Current => _elementsArray[_listSize - _counter];

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
