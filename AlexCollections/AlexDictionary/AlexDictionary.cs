namespace AlexCollections
{
    public class AlexDictionary<TKey, TValue>
    {
        private const int InitialSize = 100;

        private int _count;
        private KeyValuePair<TKey, TValue>[] _keyValuePairs;

        public AlexDictionary()
        {
            _keyValuePairs = new KeyValuePair<TKey, TValue>[InitialSize];
        }

        public int Count => _count;

        public KeysList<TKey, TValue> Keys
        {
            get
            {
                return new KeysList<TKey, TValue>(_keyValuePairs, _count);
            }
        }
        public TValue[] Values { get; set; }

        public void Add(TKey key, TValue value)
        {

        }
    }
}
