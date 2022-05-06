namespace AlexList
{
    public class AlexEnumerator<T>
    {       
        readonly int _listSize;
        readonly T[] _elementsArray;

        private int _indexOfElement = -1;

        public AlexEnumerator(T[] elementsArray, int listSize)
        {
            _elementsArray = elementsArray;
            _listSize = listSize;
        }

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

        public object Current
        {
            get
            {
                return _elementsArray[_indexOfElement];
            }
        }
    }
}
