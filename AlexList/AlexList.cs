namespace AlexList
{
    public class AlexList<T>
    {
        private const string WrongIndexExceptionMessage = "The collection does not contain the entered index or value.";

        private int _arraySize;
        private int _indexOfElement = -1;
        private T[] _elementsArray;

        public AlexList()
        {
            Clear();
        }

        #region Enumerable

        public bool MoveNext()
        {
            if (_indexOfElement < _arraySize - 1)
            {
                _indexOfElement++;
                return true;
            }
            else
            {
                Reset();
                return false;
            }
        }

        public void Reset()
        {
            _indexOfElement = -1;
        }

        public object Current
        {
            get
            {
                return _elementsArray[_indexOfElement];
            }
        }

        public AlexList<T> GetEnumerator()
        {
            return this;
        }

        #endregion

        public void Add(T value)
        {
            ResizeArray(_arraySize + 1);
            _elementsArray[_arraySize - 1] = value;
        }

        public int BinarySearch(T value, IAlexComparer<T> comparer = null)
        {
            comparer = GetComparerOrDefault(comparer);
            return RecursivelyBinarySearch(value, (0, _arraySize), comparer);
        }

        public void Clear()
        {
            _elementsArray = new T[100];
            _arraySize = 0;
        }

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            comparer = GetComparerOrDefault(comparer);
            if (IndexOf(value, comparer) == -1)
                return false;
            else
                return true;
        }

        public int FindIndex(Predicate<T> predicate)
        {
            for (int counter = 0; counter < _arraySize; counter++)
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
            for (int counter = _arraySize - 1; counter > -1; counter--)
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
            comparer = GetComparerOrDefault(comparer);

            for (int counter = 0; counter < _arraySize; counter++)
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
            if (index < 0 || index > _arraySize)
            {
                throw new ArgumentException(WrongIndexExceptionMessage);
            }
            else if (index == _arraySize)
            {
                Add(value);
            }
            else
            {
                ResizeArray(_arraySize + 1);

                for (int counter = _arraySize - 2; counter >= index; counter--)
                {
                    _elementsArray[counter + 1] = _elementsArray[counter];
                }

                _elementsArray[index] = value;
            }
        }

        public void Remove(T value)
        {
            int index = IndexOf(value);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _arraySize)
            {
                throw new ArgumentException(WrongIndexExceptionMessage);
            }

            for (int counter = index; counter < _arraySize; counter++)
            {
                _elementsArray[counter] = _elementsArray[counter + 1];
            }

            ResizeArray(_arraySize - 1);
        }

        public void Sort(IAlexComparer<T> comparer = null)
        {
            comparer = GetComparerOrDefault(comparer);

            bool arrayIsNotSorted;
            do
            {
                arrayIsNotSorted = false;

                for (int counter = 0; counter < _arraySize - 1; counter++)
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
            _arraySize = newSize;
            if (newSize > _elementsArray.Length)
            {
                T[] interimElementsArray = new T[_elementsArray.Length + 100];
                AssignValuesToNewArray(interimElementsArray, newSize);
                _elementsArray = interimElementsArray;
            }
        }

        private void AssignValuesToNewArray(T[] interimElementsArray, int size)
        {
            for (int counter = 0; counter < size; counter++)
                interimElementsArray[counter] = _elementsArray[counter];
        }

        private static IAlexComparer<T> GetComparerOrDefault(IAlexComparer<T> comparer)
        {
            return comparer ?? new DefaultAlexComparer<T>();
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
            else
            {
                return middleIndex;
            }
        }
    }
}