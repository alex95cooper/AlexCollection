namespace AlexCollections
{
    public class AlexList<T>
    {
        private const string WrongIndexExceptionMessage = "The collection does not contain the entered index or value.";

        private int _listSise;
        private T[] _elementsArray;

        public AlexList()
        {
            Clear();
        }

        public AlexEnumerator<T> GetEnumerator()
        {
            return new AlexEnumerator<T>(_elementsArray, _listSise);
        }

        public void Add(T value)
        {
            ResizeArray(_listSise + 1);
            _elementsArray[_listSise - 1] = value;
        }

        public void AddRange(AlexList<T> alexList)
        {
            alexList = EnsureAlexListNotNull(alexList);

            ResizeArray(_listSise + alexList._listSise);

            int lastListSize = _listSise - alexList._listSise;
            for (int counter = lastListSize; counter < _listSise; counter++)
            {
                _elementsArray[counter] = alexList._elementsArray[counter - lastListSize];
            }
        }

        public int BinarySearch(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);
            return RecursivelyBinarySearch(value, (0, _listSise), comparer);
        }

        public void Clear()
        {
            _elementsArray = new T[100];
            _listSise = 0;
        }

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);
            if (IndexOf(value, comparer) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int FindIndex(Predicate<T> predicate)
        {
            for (int counter = 0; counter < _listSise; counter++)
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
            for (int counter = _listSise - 1; counter > -1; counter--)
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

            for (int counter = 0; counter < _listSise; counter++)
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
            if (index < 0 || index > _listSise)
            {
                throw new ArgumentException(WrongIndexExceptionMessage);
            }
            else if (index == _listSise)
            {
                Add(value);
            }
            else
            {
                ResizeArray(_listSise + 1);

                for (int counter = _listSise - 2; counter >= index; counter--)
                {
                    _elementsArray[counter + 1] = _elementsArray[counter];
                }

                _elementsArray[index] = value;
            }
        }

        public void InsertRange(int index, AlexList<T> alexList)
        {
            alexList = EnsureAlexListNotNull(alexList);

            if (index < 0 || index > _listSise)
            {
                throw new ArgumentException(WrongIndexExceptionMessage);
            }
            else if (index == _listSise)
            {
                AddRange(alexList);
            }
            else
            {
                ResizeArray(_listSise + alexList._listSise);

                for (int counter = _listSise - 1; counter >= index + alexList._listSise; counter--)
                {
                    _elementsArray[counter] = _elementsArray[counter - alexList._listSise];
                }

                for (int counter = index; counter < index + alexList._listSise; counter++)
                {
                    _elementsArray[counter] = alexList._elementsArray[counter - index];
                }
            }
        }

        public void Remove(T value)
        {
            int index = IndexOf(value);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _listSise)
            {
                throw new ArgumentException(WrongIndexExceptionMessage);
            }

            for (int counter = index; counter < _listSise; counter++)
            {
                _elementsArray[counter] = _elementsArray[counter + 1];
            }

            ResizeArray(_listSise - 1);
        }

        public void Sort(IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            bool arrayIsNotSorted;
            do
            {
                arrayIsNotSorted = false;

                for (int counter = 0; counter < _listSise - 1; counter++)
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

        private void ResizeArray(int newSize)
        {
            int excessOfNewSize = newSize - _elementsArray.Length;
            if (excessOfNewSize > 0)
            {
                AssignValuesToNewArray(newSize + 100, newSize);
            }

            _listSise = newSize;
        }

        private void AssignValuesToNewArray(int newArrayLenght, int size)
        {
            T[] interimElementsArray = new T[newArrayLenght];
            for (int counter = 0; counter < size; counter++)
            {
                interimElementsArray[counter] = _elementsArray[counter];
            }

            _elementsArray = interimElementsArray;
        }

        private static AlexList<T> EnsureAlexListNotNull(AlexList<T> alexList)
        {
            return alexList ?? throw new ArgumentNullException(nameof(alexList), "A null is passed as an argument.");
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