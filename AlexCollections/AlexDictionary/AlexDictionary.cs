using System.Collections;

namespace AlexCollections
{
    public class AlexDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private const int InitialSize = 100;

        private int _count;
        private KeyValuePair<TKey, TValue>[] _keyValuePairs;

        public AlexDictionary()
        {
            _keyValuePairs = new KeyValuePair<TKey, TValue>[InitialSize];
        }

        public int Count => _count;
        public KeysList<TKey, TValue> Keys => new(_keyValuePairs, _count);
        public ValuesList<TKey, TValue> Values => new(_keyValuePairs, _count);

        #region Enumerable

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new AlexDictionaryEnumerator<TKey, TValue>(_keyValuePairs, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void Add(TKey key, TValue value, IAlexComparer<TKey> comparer)
        {
            comparer = DefaultAlexComparer<TKey>.GetComparerOrDefault(comparer);
            EnsureKeyDoesNotRepeat(key, comparer);

            _keyValuePairs[_count++] = new KeyValuePair<TKey, TValue>(key, value);           
        }
        
        public void EnsureKeyDoesNotRepeat(TKey key, IAlexComparer<TKey> comparer)
        {
            foreach (var pair in this)
            {
                if (comparer.Compare(pair.Key, key) == 0)
                {
                    throw new ArgumentException("This key already exists.");
                }
            }
        }
    }
}
