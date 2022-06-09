using System.Collections;

namespace AlexCollections
{
    public class AlexDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private const int InitialSize = 100;

        private readonly IAlexComparer<TKey> _comparer;

        private KeysList<TKey, TValue> _keys = null;
        private ValuesList<TKey, TValue> _values = null;

        public AlexDictionary(IAlexComparer<TKey> comparer = null)
        {
            _comparer = DefaultAlexComparer<TKey>.GetComparerOrDefault(comparer);
            ElementsArray = new KeyValuePair<TKey, TValue>[InitialSize];
        }

        internal KeyValuePair<TKey, TValue>[] ElementsArray { get; private set; }
        internal int Version { get; private set; }

        public KeysList<TKey, TValue> Keys => _keys ??= new(this);
        public ValuesList<TKey, TValue> Values => _values ??= new(this);

        public int Count { get; private set; }

        public TValue this[TKey key]
        {
            get
            {
                var valueWithIndex = GetValueWithIndex(key);
                EnsureKeyExistAtIndex(valueWithIndex.Index);
                return valueWithIndex.Value;
            }
            set
            {
                var valueWithIndex = GetValueWithIndex(key);
                EnsureKeyExistAtIndex(valueWithIndex.Index);
                ElementsArray[valueWithIndex.Index].Value = value;
                Version++;
            }
        }

        #region Enumerable

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new AlexDictionaryEnumerator<TKey, TValue>(ElementsArray, Count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void Add(TKey key, TValue value)
        {
            EnsureKeyIsNotNull(key);
            if (CheckIfKeyExists(key))
            {
                throw new ArgumentException("This key already exists.");
            }

            AddValidKeyValue(key, value);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (key == null || CheckIfKeyExists(key))
            {
                return false;
            }

            AddValidKeyValue(key, value);
            return true;
        }

        public void Clear()
        {
            ElementsArray = new KeyValuePair<TKey, TValue>[InitialSize];
            Count = 0;
            Version = 0;
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

        private bool CheckIfKeyExists(TKey key)
        {
            foreach (var pair in this)
            {
                if (pair.Key.GetHashCode() == key.GetHashCode()
                    && (_comparer.Compare(pair.Key, key) == 0)
                    && pair.Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        private void AddValidKeyValue(TKey key, TValue value)
        {
            if (Count == ElementsArray.Length)
            {
                ArrayResizer<KeyValuePair<TKey, TValue>>.Resize(ElementsArray.Length + InitialSize, ElementsArray);
            }

            ElementsArray[Count++] = new KeyValuePair<TKey, TValue>(key, value);
            Version++;
        }

        private static void EnsureKeyExistAtIndex(int index)
        {
            if (index == -1)
            {
                throw new ArgumentException("The collection does not contain this key.");
            }
        }

        private (TValue Value, int Index) GetValueWithIndex(TKey key)
        {
            for (int counter = 0; counter < Count; counter++)
            {
                if (ElementsArray[counter].Key.GetHashCode() == key.GetHashCode()
                    && (_comparer.Compare(key, ElementsArray[counter].Key) == 0)
                    && ElementsArray[counter].Key.Equals(key))
                {
                    return (ElementsArray[counter].Value, counter);
                }
            }

            return (default, -1);
        }

        private void RemoveAt(int index)
        {
            EnsureKeyExistAtIndex(index);
            for (int counter = index; counter < Count; counter++)
            {
                ElementsArray[counter] = ElementsArray[counter + 1];
            }

            Count--;
            Version++;
        }
    }
}
