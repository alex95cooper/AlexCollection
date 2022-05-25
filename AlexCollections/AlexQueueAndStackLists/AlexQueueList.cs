namespace AlexCollections
{
    public class AlexQueueList<T>
    {
        private int _count;
        private T[] _elementsArray;

        public AlexQueueList()
        {
            _elementsArray = new T[100];
        }

        public int Count => _count;

        public void Enqueue(T value)
        {
            if (_count == _elementsArray.Length)
            {
                ElementsArray<T>.ResizeArray(_elementsArray.Length + 100, Count, ref _elementsArray);
            }

            _elementsArray[_count] = value;
            _count++;
        }
    }
}
