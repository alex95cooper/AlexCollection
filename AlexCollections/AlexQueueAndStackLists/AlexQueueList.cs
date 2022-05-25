namespace AlexCollections
{
    internal class AlexQueueList<T>
    {
        private int _count;
        private T[] _elementsArray;

        public AlexQueueList()
        {
            _elementsArray = new T[100];
        }

        public int Count => _count;
    }
}
