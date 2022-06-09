using System.Collections;

namespace AlexCollections
{
    public class ValuesList<TKey, TValue> : IEnumerable<TValue>
    {
        private readonly AlexDictionary<TKey, TValue> _dictionary;

        public ValuesList(AlexDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        #region Enumerable

        public IEnumerator<TValue> GetEnumerator()
        {
            return new ValuesEnumerator(_dictionary);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public class ValuesEnumerator : IEnumerator<TValue>
        {
            private readonly AlexDictionary<TKey, TValue> _dictionary;
            private readonly int _dictionaryVersion;

            private int _counter = -1;

            public ValuesEnumerator(AlexDictionary<TKey, TValue> dictionary)
            {
                _dictionary = dictionary;
                _dictionaryVersion = dictionary.Version;
            }

            public TValue Current => _dictionary.ElementsArray[_counter].Value;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                EnsureDictionaryNotOutdated();
                if (_counter < _dictionary.Count - 1)
                {
                    _counter++;
                    return true;
                }

                Reset();
                return false;
            }

            public void Reset()
            {
                EnsureDictionaryNotOutdated();
                _counter = 0;
            }

            public void Dispose()
            {
            }

            private void EnsureDictionaryNotOutdated()
            {
                if (_dictionaryVersion != _dictionary.Version)
                {
                    throw new InvalidOperationException("Dictionary was changed");
                }
            }
        }
    }
}
