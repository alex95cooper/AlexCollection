using System.Collections;
using AlexCollections.Extension;

namespace AlexCollections
{
    public class AlexObservableCollection<T> : IEnumerable<T>
    {
        private const int InitialSize = 100;

        private readonly IAlexComparer<T> _comparer;

        private T[] _elementsArray;

        public AlexObservableCollection(IAlexComparer<T> comparer = null)
        {
            _comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);
            _elementsArray = new T[InitialSize];
        }

        public int Count { get; private set; }

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
                T oldValue = _elementsArray[index];
                _elementsArray[index] = value;
                OnCollectionChanged(Action.Replace, value, oldValue, index, index);
            }
        }

        public delegate void CollectionChangedHandler(object sender, CollectionChangedEventArgs<T> e);

        public event CollectionChangedHandler CollectionChanged;

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexEnumerator<T>(_elementsArray, Count);
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
            OnCollectionChanged(Action.Add, newValue: value, newIndex: Count - 1);
        }

        public void Clear()
        {
            AlexList<T> oldValues = this.ToAlexList();
            _elementsArray = new T[InitialSize];
            Count = 0;
            OnCollectionChanged(Action.Сlear, oldValues: oldValues);
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
                OnCollectionChanged(Action.Add, newValue: value, newIndex: index);
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
            OnCollectionChanged(Action.Move, firstValue, secondValue, secondIndex, firstIndex);
        }

        public void Remove(T value)
        {
            int index = IndexOf(value);
            if (index == -1)
            {
                throw new ArgumentException("The collection does not contain the entered value");
            }

            RemoveAt(index);
            OnCollectionChanged(Action.Remove, oldValue: value, oldIndex: index);
        }

        public void RemoveAt(int index)
        {
            EnsureIndexIsValid(index);
            for (int counter = index; counter < Count; counter++)
            {
                _elementsArray[counter] = _elementsArray[counter + 1];
            }

            Count--;
            OnCollectionChanged(Action.Remove, oldValue: _elementsArray[index], oldIndex: index);
        }

        private void EnsureIndexIsValid(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void ResizeIfNeeded(int count)
        {
            if (count >= _elementsArray.Length)
            {
                _elementsArray = ArrayResizer<T>.Resize(_elementsArray.Length + InitialSize, _elementsArray);
            }
        }

        private void OnCollectionChanged(Action action, T newValue = default, T oldValue = default, int newIndex = -1, int oldIndex = -1)
        {
            OnCollectionChanged(action, new AlexList<T>() { newValue }, new AlexList<T>() { oldValue }, newIndex, oldIndex);
        }

        private void OnCollectionChanged(Action action, AlexList<T> newValues = null, AlexList<T> oldValues = null, int newIndex = -1, int oldIndex = -1)
        {
            CollectionChanged?.Invoke(this, new CollectionChangedEventArgs<T>(action, newValues, oldValues, newIndex, oldIndex));
        }
    }
}
