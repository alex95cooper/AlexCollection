using System.Collections;

namespace AlexCollections
{
    public class AlexDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private const int InitialSize = 100;

        private readonly IAlexComparer<TKey> _comparer;

        private int _count;
        private KeyValuePair<TKey, TValue>[] _elementsArray;

        public AlexDictionary(IAlexComparer<TKey> comparer = null)
        {
            _comparer = DefaultAlexComparer<TKey>.GetComparerOrDefault(comparer);
            _elementsArray = new KeyValuePair<TKey, TValue>[InitialSize];
        }

        public int Count => _count;
        public KeysList<TKey, TValue> Keys => new(_elementsArray, _count);
        public ValuesList<TKey, TValue> Values => new(_elementsArray, _count);
        public TValue this[TKey key]
        {
            get
            {
                (TValue value, int index) = GetValueWithIndex(key);
                EnsureKeyExistAtIndex(index);
                return value;
            }
        }

        #region Enumerable

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new AlexDictionaryEnumerator<TKey, TValue>(_elementsArray, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void Add(TKey key, TValue value)
        {
            EnsureKeyIsNotNull(key);
            EnsureKeyDoesNotRepeat(key);

            if (_count == _elementsArray.Length)
            {
                ArrayResizer<KeyValuePair<TKey, TValue>>.Resize(_elementsArray.Length + InitialSize, _elementsArray);
            } 

            _elementsArray[_count++] = new KeyValuePair<TKey, TValue>(key, value);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            try
            {
                Add(key, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Clear()
        {
            _elementsArray = new KeyValuePair<TKey, TValue>[InitialSize];
            _count = 0;
        }

        public bool ContainsKey(TKey key)
        {
            EnsureKeyIsNotNull(key);
            (_, int index) = GetValueWithIndex(key);
            return index != -1;
        }

        public bool ContainsValue(TValue value, IAlexComparer<TValue> comparer = null)
        {
            comparer = DefaultAlexComparer<TValue>.GetComparerOrDefault(comparer);
            foreach (var pair in this)
            {
                if (comparer.Compare(pair.Value, value) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void Remove(TKey key)
        {
            (_, int index) = GetValueWithIndex(key);
            RemoveAt(index);
        }

        public void Remove(TKey key, out TValue value)
        {
            (value, int index) = GetValueWithIndex(key);
            RemoveAt(index);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            (value, int index) = GetValueWithIndex(key);
            return index != -1;
        }

        private static void EnsureKeyIsNotNull(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentException("The key is null.");
            }
        }

        private void EnsureKeyDoesNotRepeat(TKey key)
        {
            foreach (var pair in this)
            {
                if (_comparer.Compare(pair.Key, key) == 0)
                {
                    throw new ArgumentException("This key already exists.");
                }
            }
        }

        private static void EnsureKeyExistAtIndex(int index)
        {
            if (index == -1)
            {
                throw new ArgumentException("The collection does not contain this key.");
            }
        }

        private (TValue, int) GetValueWithIndex(TKey key)
        {
            for (int counter = 0; counter < _count; counter++)
            {
                if (_comparer.Compare(key, _elementsArray[counter].Key) == 0)
                {
                    return (_elementsArray[counter].Value, counter);
                }
            }

            return (default, -1);
        }

        private void RemoveAt(int index)
        {
            EnsureKeyExistAtIndex(index);
            for (int counter = index; counter < _count; counter++)
            {
                _elementsArray[counter] = _elementsArray[counter + 1];
            }

            _count--;
        }
    }
}
