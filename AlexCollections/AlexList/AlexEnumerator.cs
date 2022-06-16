using System.Collections;

namespace AlexCollections
{
    public class AlexEnumerator<T> : IEnumerator<T>
    {
        private readonly int _count;
        private readonly T[] _elementsArray;

        private int _indexOfElement = -1;

        public AlexEnumerator(T[] elementsArray, int count)
        {
            _elementsArray = elementsArray;
            _count = count;
        }

        public T Current => _elementsArray[_indexOfElement];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_indexOfElement < _count - 1)
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

        public void Dispose()
        {
        }
    }
}