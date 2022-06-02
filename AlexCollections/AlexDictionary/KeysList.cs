using System.Collections;

namespace AlexCollections
{
    public class KeysList<TKey, TValue> : IEnumerable<TKey>
    {
        private readonly KeyValuePair<TKey, TValue>[] _keyValuesPairs;
        private readonly int _count;

        public KeysList(KeyValuePair<TKey, TValue>[] keyValuesPairs, int count) 
        {
            _keyValuesPairs = keyValuesPairs;
            _count = count;
        }

        #region Enumerable

        public IEnumerator<TKey> GetEnumerator()
        {
            return new KeysEnumerator(_keyValuesPairs, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public class KeysEnumerator : IEnumerator<TKey>
        {
            private readonly KeyValuePair<TKey, TValue>[] _keyValuesPairs;
            private readonly int _count;

            private int _counter;

            public KeysEnumerator(KeyValuePair<TKey, TValue>[] keyValuesPairs, int count)
            {
                _keyValuesPairs = keyValuesPairs;
                _count = count;
            }

            public TKey Current => _keyValuesPairs[_counter].Key;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_counter < _count)
                {
                    _counter++;
                    return true;
                }

                Reset();
                return false;
            }

            public void Reset()
            {
                _counter = 0;
            }

            public void Dispose()
            {
            }
        }
    }
}
