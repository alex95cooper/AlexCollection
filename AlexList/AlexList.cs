using System.Collections;

namespace AlexList
{
    public class AlexList<T>
    {
        private int indexOfElement = -1;

        private T[] elementsArray;

        public AlexList()
        {
            elementsArray = Array.Empty<T>();
        }

        #region

        public bool MoveNext()
        {
            if (indexOfElement < elementsArray.Length - 1)
            {
                indexOfElement++;
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
                return elementsArray[indexOfElement];
            }
        }

        public void Reset()
        {
            indexOfElement = -1;
        }

        public AlexList<T> GetEnumerator()
        {
            return this;
        }

        #endregion

        public void Add(T value)
        {
            Array.Resize<T>(ref elementsArray, elementsArray.Length + 1);
            elementsArray[^1] = value;
        }

        public int BinarySearch(T value)
        {
            int lowerRangeLimit = 0;
            int upperRangeLimit = elementsArray.Length - 1;

            return RecursionSearch(lowerRangeLimit, upperRangeLimit, value);
        }

        private int Compare(Object? x, Object? y)
        {
            return -new CaseInsensitiveComparer().Compare(y, x);
        }

        private int RecursionSearch(int lowerRangeLimit, int upperRangeLimit, T value)
        {
            int rangeOfSearching = upperRangeLimit - lowerRangeLimit;

            if (rangeOfSearching == 0 || rangeOfSearching == 1)
                return -(upperRangeLimit + 1);

            int middleOfRange = (lowerRangeLimit + (rangeOfSearching / 2));

            if (Compare(value, elementsArray[lowerRangeLimit]) == 0)
            {
                return lowerRangeLimit;
            }
            else if (Compare(value, elementsArray[middleOfRange]) == 0)
            {
                return middleOfRange;
            }
            else if (Compare(value, elementsArray[upperRangeLimit]) == 0)
            {
                return upperRangeLimit;
            }
            else if (Compare(value, elementsArray[lowerRangeLimit]) < 0)
            {
                return -1;
            }
            else if (Compare(value, elementsArray[upperRangeLimit]) > 0)
            {
                return -(upperRangeLimit + 1);
            }
            else if (Compare(value, elementsArray[lowerRangeLimit]) > 0 && Compare(value, elementsArray[middleOfRange]) < 0)
            {
                upperRangeLimit = middleOfRange;
                return RecursionSearch(lowerRangeLimit, upperRangeLimit, value);
            }
            else if (Compare(value, elementsArray[middleOfRange]) > 0 && Compare(value, elementsArray[upperRangeLimit]) < 0)
            {
                lowerRangeLimit = middleOfRange;
                return RecursionSearch(lowerRangeLimit, upperRangeLimit, value);
            }
            else
            {
                return -1;
            }
        }

        public void Clear()
        {
            elementsArray = Array.Empty<T>();
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
            for (int counter = 0; counter < elementsArray.Length - 1; counter++)
            {
                if (predicate.Invoke(elementsArray[counter]))
                {
                    return counter;
                }
            }

            return -1;
        }

        public int FindLastIndex(Predicate<T> predicate)
        {
            for (int counter = elementsArray.Length - 1; counter > -1; counter--)
            {
                if (predicate.Invoke(elementsArray[counter]))
                {
                    return counter;
                }
            }

            return -1;
        }

        public int IndexOf(T value)
        {
            for (int counter = 0; counter < elementsArray.Length - 1; counter++)
            {
                if (Compare(value, elementsArray[counter]) == 0)
                {
                    return counter;
                }
            }

            return -1;
        }

        public void Insert(T value, int index)
        {
            Array.Resize<T>(ref elementsArray, elementsArray.Length + 1);

            for (int counter = elementsArray.Length - 2; counter >= index; counter--)
            {
                elementsArray[counter + 1] = elementsArray[counter];
            }

            elementsArray[index] = value;
        }

        public void Remove(T value)
        {
            int index = IndexOf(value);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            for (int counter = index; counter < elementsArray.Length - 1; counter++)
            {
                elementsArray[counter] = elementsArray[counter + 1];
            }

            Array.Resize<T>(ref elementsArray, elementsArray.Length - 1);
        }

        public void Sort()
        {
            bool arrayIsNotSorted;

            do
            {
                arrayIsNotSorted = false;

                for (int counter = 0; counter < elementsArray.Length - 1; counter++)
                {
                    if (Compare(elementsArray[counter], elementsArray[counter + 1]) > 0)
                    {
                        Insert(elementsArray[counter + 1], counter);
                        RemoveAt(counter + 2);
                        arrayIsNotSorted = true;
                    }
                }
            }
            while (arrayIsNotSorted == true);
        }
    }
}