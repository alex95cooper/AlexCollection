using System.Collections;

namespace AlexCollections
{
    public class AlexEnumerator<T> : IEnumerator<T>
    {       
        private readonly int _listSize;
        private readonly T[] _elementsArray;

        private int _indexOfElement = -1;

        public AlexEnumerator(T[] elementsArray, int listSize)
        {
            _elementsArray = elementsArray;
            _listSize = listSize;
        }

        public T Current => _elementsArray[_indexOfElement];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_indexOfElement < _listSize - 1)
            {
                _indexOfElement++;
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
            _indexOfElement = -1;
        }

        void IDisposable.Dispose()
        {
        }
    }
}
