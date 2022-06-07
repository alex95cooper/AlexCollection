using System.Collections;

namespace AlexCollections
{
    internal class AlexDictionaryEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private readonly KeyValuePair<TKey, TValue>[] _keyValuesPairs;
        private readonly int _count;

        private int _counter;

        public AlexDictionaryEnumerator(KeyValuePair<TKey, TValue>[] keyValuesPairs, int count)
        {
            _keyValuesPairs = keyValuesPairs;
            _count = count;
        }

        public KeyValuePair<TKey, TValue> Current => _keyValuesPairs[_counter];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_counter < _count - 1)
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
