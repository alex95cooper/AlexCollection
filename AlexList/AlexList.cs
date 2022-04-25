namespace AlexList
{
    public class AlexList<T>
    {
        private const string ArgumentExceptMessage = "The collection does not contain the entered index or value.";

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

        public object Current
        {
            get
            {
                return _elementsArray[_indexOfElement];
            }
        }

        public void Reset()
        {
            _indexOfElement = -1;
        }

        public AlexList<T> GetEnumerator()
        {
            return this;
        }

        #endregion

        public void Add(T value)
        {
            ArrayResize(_arraySize + 1);
            _elementsArray[_arraySize - 1] = value;
        }

        public int BinarySearch(T value, IAlexComparer<T> comparer = null)
        {
            comparer = CheckComparerReference(comparer);
            int lowerRangeLimit = 0;
            int upperRangeLimit = _arraySize - 1;
            return RecursionBinarySearch(lowerRangeLimit, upperRangeLimit, value, comparer);
        }

        public void Clear()
        {
            _elementsArray = new T[100];
            _arraySize = 0;
        }

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            CheckComparerReference(comparer);
            if (IndexOf(value, comparer) == -1)
                return false;
            else
                return true;
        }

        public int FindIndex(Predicate<T> predicate)
        {
            for (int counter = 0; counter < _arraySize - 1; counter++)
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
            CheckComparerReference(comparer);
            for (int counter = 0; counter < _arraySize - 1; counter++)
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
            if (index >= 0 && index < _arraySize)
            {
                ArrayResize(_arraySize + 1);

                for (int counter = _arraySize - 2; counter >= index; counter--)
                {
                    _elementsArray[counter + 1] = _elementsArray[counter];
                }

                _elementsArray[index] = value;
            }
            else if (index == _arraySize)
            {
                Add(value);
            }
            else
            {
                throw new ArgumentException(ArgumentExceptMessage);
            }
        }

        public void Remove(T value)
        {
            int index = IndexOf(value);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < _arraySize)
            {
                for (int counter = index; counter < _arraySize; counter++)
                {
                    _elementsArray[counter] = _elementsArray[counter + 1];
                }

                ArrayResize(_arraySize - 1);
            }
            else
            {
                throw new ArgumentException(ArgumentExceptMessage);
            }
        }

        public void Sort(IAlexComparer<T> comparer = null)
        {
            comparer = CheckComparerReference(comparer);

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

        private void ArrayResize(int newSize)
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
        
        private static IAlexComparer<T> CheckComparerReference(IAlexComparer<T> comparer)
        {
            if (comparer == null)
            {
                comparer = new DefaultAlexComparer<T>();
            }

            return comparer;
        }

        private int RecursionBinarySearch(int lowerRangeLimit, int upperRangeLimit, T value, IAlexComparer<T> comparer)
        {
            int rangeOfSearching = upperRangeLimit - lowerRangeLimit;

            if (rangeOfSearching == 0 || rangeOfSearching == 1)
                return -(upperRangeLimit + 1);

            int middleOfRange = (lowerRangeLimit + (rangeOfSearching / 2));

            if (comparer.Compare(value, _elementsArray[lowerRangeLimit]) == 0)
            {
                return lowerRangeLimit;
            }
            else if (comparer.Compare(value, _elementsArray[middleOfRange]) == 0)
            {
                return middleOfRange;
            }
            else if (comparer.Compare(value, _elementsArray[upperRangeLimit]) == 0)
            {
                return upperRangeLimit;
            }
            else if (comparer.Compare(value, _elementsArray[lowerRangeLimit]) < 0)
            {
                return -1;
            }
            else if (comparer.Compare(value, _elementsArray[upperRangeLimit]) > 0)
            {
                return -(upperRangeLimit + 1);
            }
            else if (comparer.Compare(value, _elementsArray[lowerRangeLimit]) > 0 && comparer.Compare(value, _elementsArray[middleOfRange]) < 0)
            {
                upperRangeLimit = middleOfRange;
                return RecursionBinarySearch(lowerRangeLimit, upperRangeLimit, value, comparer);
            }
            else if (comparer.Compare(value, _elementsArray[middleOfRange]) > 0 && comparer.Compare(value, _elementsArray[upperRangeLimit]) < 0)
            {
                lowerRangeLimit = middleOfRange;
                return RecursionBinarySearch(lowerRangeLimit, upperRangeLimit, value, comparer);
            }
            else
            {
                return -1;
            }
        }
    }
}