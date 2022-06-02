using System.Collections;

namespace AlexCollections.AlexDictionary
{
    internal class ValueList<TKey, TValue> : IEnumerable<TValue>
    {
        private readonly KeyValuePair<TKey, TValue>[] _keyValuesPairs;
        private readonly int _count;

        public ValueList(KeyValuePair<TKey, TValue>[] keyValuesPairs, int count)
        {
            _keyValuesPairs = keyValuesPairs;
            _count = count;
        }

        #region Enumerable

        public IEnumerator<TValue> GetEnumerator()
        {
            return new ValuesEnumerator(_keyValuesPairs, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public class ValuesEnumerator : IEnumerator<TValue>
        {
            private readonly KeyValuePair<TKey, TValue>[] _keyValuesPairs;
            private readonly int _count;

            private int _counter;

            public ValuesEnumerator(KeyValuePair<TKey, TValue>[] keyValuesPairs, int count)
            {
                _keyValuesPairs = keyValuesPairs;
                _count = count;
            }

            public TValue Current => _keyValuesPairs[_counter].Value;

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
