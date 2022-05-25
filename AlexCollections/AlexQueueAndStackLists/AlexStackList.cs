
namespace AlexCollections
{
    internal class AlexStackList<T>
    {
        private int _count;
        private T[] _elementsArray;

        public AlexStackList()
        {
            _elementsArray = new T[100];
        }

        public int Count => _count;

        public void Push(T value)
        {
            if (_count == _elementsArray.Length)
            {
                ResizeArray(_elementsArray.Length);
            }

            _elementsArray[_count] = value;
            _count++;
        }

        private void ResizeArray(int newArrayLenght)
        {
            T[] interimElementsArray = new T[newArrayLenght];
            for (int counter = 0; counter < _count; counter++)
            {
                interimElementsArray[counter] = _elementsArray[counter];
            }

            _elementsArray = interimElementsArray;
        }
    }
}
