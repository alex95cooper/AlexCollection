using System.Collections;

namespace AlexCollections
{
    public class AlexList<T> : IEnumerable<T>
    {
        private const string WrongIndexExceptionMessage = "The collection does not contain the entered index or value.";

        private int _count;
        private T[] _elementsArray;

        public AlexList()
        {
            Clear();
        }

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexEnumerator<T>(_elementsArray, _count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void Add(T value)
        {
            ResizeIfNeeded(_count + 1);
            _elementsArray[_count] = value;
            _count++;
        }

        public void AddRange(AlexList<T> alexList)
        {
            alexList = EnsureAlexListNotNull(alexList);

            int newCount = _count + alexList._count;
            ResizeIfNeeded(_count);
            for (int counter = _count; counter < newCount; counter++)
            {
                _elementsArray[counter] = alexList._elementsArray[counter - _count];
            }

            _count += alexList._count;
        }

        public int BinarySearch(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);
            return RecursivelyBinarySearch(value, (0, _count), comparer);
        }

        public void Clear()
        {
            _elementsArray = new T[100];
            _count = 0;
        }

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);
            return IndexOf(value, comparer) != -1;
        }

        public int FindIndex(Predicate<T> predicate)
        {
            for (int counter = 0; counter < _count; counter++)
            {
                if (predicate.Invoke(_elementsArray[counter]))
                {
                    return counter;
                }
            }

            return -1;
        }

        public int FindLastIndex(Predicate<T> predicate)
        {
            for (int counter = _count - 1; counter > -1; counter--)
            {
                if (predicate.Invoke(_elementsArray[counter]))
                {
                    return counter;
                }
            }

            return -1;
        }

        public int IndexOf(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            for (int counter = 0; counter < _count; counter++)
            {
                if (comparer.Compare(value, _elementsArray[counter]) == 0)
                {
                    return counter;
                }
            }

            return -1;
        }

        public void Insert(T value, int index)
        {
            if (index < 0 || index > _count)
            {
                throw new ArgumentException(WrongIndexExceptionMessage);
            }
            else if (index == _count)
            {
                Add(value);
            }
            else
            {
                ResizeIfNeeded(_count + 1);
                for (int counter = _count - 1; counter >= index; counter--)
                {
                    _elementsArray[counter + 1] = _elementsArray[counter];
                }

                _elementsArray[index] = value;
                _count++;
            }
        }

        public void InsertRange(int index, AlexList<T> alexList)
        {
            alexList = EnsureAlexListNotNull(alexList);

            if (index < 0 || index > _count)
            {
                throw new ArgumentException(WrongIndexExceptionMessage);
            }
            else if (index == _count)
            {
                AddRange(alexList);
            }
            else
            {
                int newCount = _count + alexList._count;
                ResizeIfNeeded(newCount);
                for (int counter = newCount - 1; counter >= index + alexList._count; counter--)
                {
                    _elementsArray[counter] = _elementsArray[counter - alexList._count];
                }

                for (int counter = index; counter < index + alexList._count; counter++)
                {
                    _elementsArray[counter] = alexList._elementsArray[counter - index];
                }

                _count += alexList._count;
            }
        }

        public void Remove(T value)
        {
            int index = IndexOf(value);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentException(WrongIndexExceptionMessage);
            }

            for (int counter = index; counter < _count; counter++)
            {
                _elementsArray[counter] = _elementsArray[counter + 1];
            }

            _count--;
        }

        public void Sort(IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            bool arrayIsNotSorted;
            do
            {
                arrayIsNotSorted = false;

                for (int counter = 0; counter < _count - 1; counter++)
                {
                    if (comparer.Compare(_elementsArray[counter], _elementsArray[counter + 1]) > 0)
                    {
                        T interimValue = _elementsArray[counter];
                        _elementsArray[counter] = _elementsArray[counter + 1];
                        _elementsArray[counter + 1] = interimValue;
                        arrayIsNotSorted = true;
                    }
                }
            }
            while (arrayIsNotSorted == true);
        }

        private void ResizeIfNeeded(int count)
        {
            if (count >= _elementsArray.Length)
            {
                ElementsArray<T>.ResizeArray(_elementsArray.Length + 100, _count, ref _elementsArray);
            }
        }

        private static AlexList<T> EnsureAlexListNotNull(AlexList<T> alexList)
        {
            return alexList ?? throw new ArgumentNullException(nameof(alexList));
        }

        private int RecursivelyBinarySearch(T searchValue, (int Index, int Count) searchRange, IAlexComparer<T> comparer)
        {
            if (searchRange.Count == 0)
            {
                return -1;
            }
            else if (searchRange.Count == 1)
            {
                int rootArrayIndex = searchRange.Index;
                return comparer.Compare(searchValue, _elementsArray[rootArrayIndex]) == 0 ? rootArrayIndex : -1;
            }

            int halfRangeSize = searchRange.Count / 2;
            int middleIndex = searchRange.Index + halfRangeSize;
            int comparerResult = comparer.Compare(searchValue, _elementsArray[middleIndex]);

            if (comparerResult == -1)
            {
                return RecursivelyBinarySearch(searchValue, (searchRange.Index, halfRangeSize), comparer);
            }
            else if (comparerResult == 1)
            {
                int newRangeIndex = middleIndex + 1;
                int newRangeCount = searchRange.Count - (halfRangeSize + 1);
                return RecursivelyBinarySearch(searchValue, (newRangeIndex, newRangeCount), comparer);
            }

            return middleIndex;
        }
    }
}