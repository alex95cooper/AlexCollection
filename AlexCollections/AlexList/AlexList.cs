using System.Collections;

namespace AlexCollections
{
    public class AlexList<T> : IEnumerable<T>
    {
        private const string WrongIndexExceptionMessage = "The collection does not contain the entered index or value.";
        private const int InitialSize = 100;

        public AlexList()
        {
            Clear();
        }

        internal T[] ElementsArray { get; set; }

        public int Count { get; internal set; }

        public T this[int index]
        {
            get
            {
                EnsureIndexIsValid(index);
                return ElementsArray[index];
            }
            set
            {
                EnsureIndexIsValid(index);
                ElementsArray[index] = value;
            }
        }

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexListEnumerator<T>(ElementsArray, Count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void Add(T value)
        {
            ResizeIfNeeded(Count + 1);
            ElementsArray[Count] = value;
            Count++;
        }

        public void AddRange(AlexList<T> alexList)
        {
            alexList = EnsureAlexListNotNull(alexList);

            int newCount = Count + alexList.Count;
            ResizeIfNeeded(Count);
            for (int counter = Count; counter < newCount; counter++)
            {
                ElementsArray[counter] = alexList.ElementsArray[counter - Count];
            }

            Count += alexList.Count;
        }

        public int BinarySearch(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);
            return RecursivelyBinarySearch(value, (0, Count), comparer);
        }

        public void Clear()
        {
            ElementsArray = new T[InitialSize];
            Count = 0;
        }

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);
            return IndexOf(value, comparer) != -1;
        }

        public int FindIndex(Predicate<T> predicate)
        {
            for (int counter = 0; counter < Count; counter++)
            {
                if (predicate.Invoke(ElementsArray[counter]))
                {
                    return counter;
                }
            }

            return -1;
        }

        public int FindLastIndex(Predicate<T> predicate)
        {
            for (int counter = Count - 1; counter > -1; counter--)
            {
                if (predicate.Invoke(ElementsArray[counter]))
                {
                    return counter;
                }
            }

            return -1;
        }

        public int IndexOf(T value, IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            for (int counter = 0; counter < Count; counter++)
            {
                if (comparer.Compare(value, ElementsArray[counter]) == 0)
                {
                    return counter;
                }
            }

            return -1;
        }

        public void Insert(T value, int index)
        {
            EnsureIndexIsValid(index);
            if (index == Count)
            {
                Add(value);
            }
            else
            {
                ResizeIfNeeded(Count + 1);
                for (int counter = Count - 1; counter >= index; counter--)
                {
                    ElementsArray[counter + 1] = ElementsArray[counter];
                }

                ElementsArray[index] = value;
                Count++;
            }
        }

        public void InsertRange(int index, AlexList<T> alexList)
        {
            alexList = EnsureAlexListNotNull(alexList);

            EnsureIndexIsValid(index);
            if (index == Count)
            {
                AddRange(alexList);
            }
            else
            {
                int newCount = Count + alexList.Count;
                ResizeIfNeeded(newCount);
                for (int counter = newCount - 1; counter >= index + alexList.Count; counter--)
                {
                    ElementsArray[counter] = ElementsArray[counter - alexList.Count];
                }

                for (int counter = index; counter < index + alexList.Count; counter++)
                {
                    ElementsArray[counter] = alexList.ElementsArray[counter - index];
                }

                Count += alexList.Count;
            }
        }

        public void Remove(T value)
        {
            int index = IndexOf(value);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            EnsureIndexIsValid(index);
            for (int counter = index; counter < Count; counter++)
            {
                ElementsArray[counter] = ElementsArray[counter + 1];
            }

            Count--;
        }

        public void Sort(IAlexComparer<T> comparer = null)
        {
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            bool arrayIsNotSorted;
            do
            {
                arrayIsNotSorted = false;

                for (int counter = 0; counter < Count - 1; counter++)
                {
                    if (comparer.Compare(ElementsArray[counter], ElementsArray[counter + 1]) > 0)
                    {
                        T interimValue = ElementsArray[counter];
                        ElementsArray[counter] = ElementsArray[counter + 1];
                        ElementsArray[counter + 1] = interimValue;
                        arrayIsNotSorted = true;
                    }
                }
            }
            while (arrayIsNotSorted == true);
        }

        private void ResizeIfNeeded(int count)
        {
            if (count >= ElementsArray.Length)
            {
                ElementsArray = ArrayResizer<T>.Resize(ElementsArray.Length + InitialSize, ElementsArray);
            }
        }

        private void EnsureIndexIsValid(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentException(WrongIndexExceptionMessage);
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
                return comparer.Compare(searchValue, ElementsArray[rootArrayIndex]) == 0 ? rootArrayIndex : -1;
            }

            int halfRangeSize = searchRange.Count / 2;
            int middleIndex = searchRange.Index + halfRangeSize;
            int comparerResult = comparer.Compare(searchValue, ElementsArray[middleIndex]);

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