using System.Collections;

namespace AlexList
{
    public class AlexList<T>
    {        
        private const string ArgumentExceptMessage = "The collection does not contain the entered index or value.";

        private int _indexOfElement = -1;

        private T[] _elementsArray;
        
        public AlexList()
        {
            _elementsArray = new T[0];
        }

        #region Enumerable

        public bool MoveNext()
        {
            if (_indexOfElement < _elementsArray.Length - 1)
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

        public object? Current
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
            ArrayResize(_elementsArray.Length + 1);
            _elementsArray[^1] = value;
        }

        public int BinarySearch(T value)
        {
            int lowerRangeLimit = 0;
            int upperRangeLimit = _elementsArray.Length - 1;

            return RecursionBinarySearch(lowerRangeLimit, upperRangeLimit, value);
        }

        public void Clear()
        {
            _elementsArray = new T[0];
        }

        public bool Contains(T value)
        {
            if (IndexOf(value) == -1)
                return false;
            else
                return true;
        }

        public int FindIndex(Predicate<T> predicate)
        {
            for (int counter = 0; counter < _elementsArray.Length - 1; counter++)
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
            for (int counter = _elementsArray.Length - 1; counter > -1; counter--)
            {
                if (predicate.Invoke(_elementsArray[counter]))
                {
                    return counter;
                }
            }

            return -1;
        }

        public int IndexOf(T value)
        {
            for (int counter = 0; counter < _elementsArray.Length - 1; counter++)
            {
                if (Compare(value, _elementsArray[counter]) == 0)
                {
                    return counter;
                }
            }

            return -1;
        }

        public void Insert(T value, int index)
        {
            if (index >= 0 && index < _elementsArray.Length)
            {
                ArrayResize(_elementsArray.Length + 1);

                for (int counter = _elementsArray.Length - 2; counter >= index; counter--)
                {
                    _elementsArray[counter + 1] = _elementsArray[counter];
                }

                _elementsArray[index] = value;
            }
            else if (index == _elementsArray.Length)
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
            if (index >= 0 && index < _elementsArray.Length)
            {
                for (int counter = index; counter < _elementsArray.Length - 1; counter++)
                {
                    _elementsArray[counter] = _elementsArray[counter + 1];
                }

                ArrayResize(_elementsArray.Length - 1);
            }
            else
            {
                throw new ArgumentException(ArgumentExceptMessage);
            }
        }

        public void Sort()
        {
            bool arrayIsNotSorted;

            do
            {
                arrayIsNotSorted = false;

                for (int counter = 0; counter < _elementsArray.Length - 1; counter++)
                {
                    if (Compare(_elementsArray[counter], _elementsArray[counter + 1]) > 0)
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
            T[] interimElementsArray = new T[newSize];

            if (_elementsArray.Length > 0)
            {
                if (_elementsArray.Length < newSize)                
                    AssignValuesToNewArray(interimElementsArray, _elementsArray.Length);                
                else if (_elementsArray.Length > newSize)                
                    AssignValuesToNewArray(interimElementsArray, newSize);                
                else                
                    return;                                    
            }

            _elementsArray = interimElementsArray;
        }

        private void AssignValuesToNewArray(T[] interimElementsArray, int size)
        {
            for (int counter = 0; counter < size; counter++)
                interimElementsArray[counter] = _elementsArray[counter];
        }

        private int Compare(Object? x, Object? y)
        {
            return -new CaseInsensitiveComparer().Compare(y, x);
        }

        private int RecursionBinarySearch(int lowerRangeLimit, int upperRangeLimit, T value)
        {
            int rangeOfSearching = upperRangeLimit - lowerRangeLimit;

            if (rangeOfSearching == 0 || rangeOfSearching == 1)
                return -(upperRangeLimit + 1);

            int middleOfRange = (lowerRangeLimit + (rangeOfSearching / 2));

            if (Compare(value, _elementsArray[lowerRangeLimit]) == 0)
            {
                return lowerRangeLimit;
            }
            else if (Compare(value, _elementsArray[middleOfRange]) == 0)
            {
                return middleOfRange;
            }
            else if (Compare(value, _elementsArray[upperRangeLimit]) == 0)
            {
                return upperRangeLimit;
            }
            else if (Compare(value, _elementsArray[lowerRangeLimit]) < 0)
            {
                return -1;
            }
            else if (Compare(value, _elementsArray[upperRangeLimit]) > 0)
            {
                return -(upperRangeLimit + 1);
            }
            else if (Compare(value, _elementsArray[lowerRangeLimit]) > 0 && Compare(value, _elementsArray[middleOfRange]) < 0)
            {
                upperRangeLimit = middleOfRange;
                return RecursionBinarySearch(lowerRangeLimit, upperRangeLimit, value);
            }
            else if (Compare(value, _elementsArray[middleOfRange]) > 0 && Compare(value, _elementsArray[upperRangeLimit]) < 0)
            {
                lowerRangeLimit = middleOfRange;
                return RecursionBinarySearch(lowerRangeLimit, upperRangeLimit, value);
            }
            else
            {
                return -1;
            }
        }
    }
}