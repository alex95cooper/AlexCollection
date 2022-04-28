namespace AlexList
{
    public class AlexEnumerator<T> 
    {       
        readonly int _arraySize;
        readonly T[] _elementsArray;

        private int _indexOfElement = -1;

        public AlexEnumerator(T[] elementsArray, int arraySize)
        {
            _elementsArray = elementsArray;
            _arraySize = arraySize;
        }

        public bool MoveNext()
        {
            if (_indexOfElement < _arraySize - 1)
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
