using System.Collections;

namespace AlexCollections
{
    public class AlexObservableCollection<T> : IEnumerable<T>
    {
        private const string WrongIndexExceptionMessage = "The collection does not contain the entered index or value.";
        private const int InitialSize = 100;
        private const int IndexNotChanged = -1;

        private readonly IAlexComparer<T> _comparer;

        private T[] _elementsArray;

        public delegate void EventHandler(object sender, EventArgs e);

        public event EventHandler<EventArgs> CollectionChanged;

        public AlexObservableCollection(IAlexComparer<T> comparer = null)
        {
            _comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);
            _elementsArray = new T[InitialSize];
        }

        public int Count { get; internal set; }

        public T this[int index]
        {
            get
            {
                EnsureIndexIsValid(index);
                return _elementsArray[index];
            }
            set
            {
                EnsureIndexIsValid(index);
                _elementsArray[index] = value;
            }
        }

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexListEnumerator<T>(_elementsArray, Count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void Add(T value)
        {
            ResizeIfNeeded(Count + 1);
            _elementsArray[Count] = value;
            Count++;

            SendEvent(Action.Add, value, default, Count - 1, IndexNotChanged);
        }

        public void Clear()
        {
            _elementsArray = new T[InitialSize];
            Count = 0;
        }

        public bool Contains(T value) => IndexOf(value) >= 0;

        public void Insert(int index, T value)
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
                    _elementsArray[counter + 1] = _elementsArray[counter];
                }

                _elementsArray[index] = value;
                Count++;
            }
        }

        public int IndexOf(T value)
        {
            for (int counter = 0; counter < Count; counter++)
            {
                if (_comparer.Compare(_elementsArray[counter], value) == 0)
                {
                    return counter;
                }
            }

            return -1;
        }

        public void Move(int firstIndex, int secondIndex)
        {
            T firstValue = _elementsArray[firstIndex];
            T secondValue = _elementsArray[secondIndex];
            _elementsArray[secondIndex] = firstValue;
            _elementsArray[firstIndex] = secondValue;
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
                _elementsArray[counter] = _elementsArray[counter + 1];
            }

            Count--;
        }

        private void EnsureIndexIsValid(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentException(WrongIndexExceptionMessage);
            }
        }

        private void ResizeIfNeeded(int count)
        {
            if (count >= _elementsArray.Length)
            {
                _elementsArray = ArrayResizer<T>.Resize(_elementsArray.Length + InitialSize, _elementsArray);
            }
        }

        private void SendEvent(Action action, T newValue, T oldValue, int newIndex, int oldIndex)
        {
            CollectionChanged?.Invoke(this, new CollectionChangedEventArgs<T>(action, newValue, oldValue, newIndex, oldIndex));
        }
    }
}
