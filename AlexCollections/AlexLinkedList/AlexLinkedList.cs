using System.Collections;

namespace AlexCollections
{
    public class AlexLinkedList<T> : IEnumerable<T>
    {
        private int _count;
        private LinkedNode<T> _first;
        private LinkedNode<T> _last;

        public int Count => _count;
        public LinkedNode<T> First => _first;
        public LinkedNode<T> Last => _last;
 
        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexLinkedListEnumerator<T>(Last, Count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void Add(T value)
        {
            if (Count == 0)
            {
                LinkedNode<T> newNode = new(value);
                _first = newNode;
                _last = newNode;
            }
            else if (Count == 1)
            {
                LinkedNode<T> newNode = new(value, First, First);
                First.PreviousNode = newNode;
                First.NextNode = newNode;
                _last = newNode;
            }
            else if (Count > 1)
            {
                LinkedNode<T> newNode = GetNodeBetweenLastAndFirst(value);
                _last = newNode;
            }

            _count++;
        }

        public void AddRange(params T[] values)
        {
            AlexLinkedList<T> newList = ConvertToAlexLinkedList(values);

            _last.NextNode = newList.First;
            _first.PreviousNode = newList.Last;
            newList._first.PreviousNode = _last;
            newList._last.NextNode = _first;

            _last = newList._last;
            _count += newList.Count;
        }

        public void Clear()
        {
            _count = 0;
            _first = null;
            _last = null;
        }

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            LinkedNode<T> verificationNode = Last;
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            for (int counter = 0; counter < _count; counter++)
            {
                verificationNode = verificationNode.NextNode;
                if (comparer.Compare(verificationNode.Value, value) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public T GetByIndex(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentException("The collection does not contain the entered index.");
            }

            LinkedNode<T> verificationNode = Last;

            for (int counter = 0; counter <= index; counter++)
            {
                verificationNode = verificationNode.NextNode;
            }

            return verificationNode.Value;
        }

        public int IndexOf(T value, IAlexComparer<T> comparer = null)
        {
            LinkedNode<T> verificationNode = Last;
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            for (int counter = 0; counter < _count; counter++)
            {
                verificationNode = verificationNode.NextNode;
                if (comparer.Compare(verificationNode.Value, value) == 0)
                {
                    return counter;
                }
            }

            return -1;
        }

        public void InsertAfter(LinkedNode<T> node, T value)
        {
            node = EnsureNodeIsInList(node);

            LinkedNode<T> nextNode = node.NextNode;
            LinkedNode<T> newNode = new(value, node, nextNode);
            node.NextNode = newNode;
            nextNode.PreviousNode = newNode;

            CheckCurrentReferenceToLast(node, newNode);
            _count++;
        }

        public void InsertBefore(LinkedNode<T> node, T value)
        {
            node = EnsureNodeIsInList(node);

            LinkedNode<T> previousNode = node.PreviousNode;
            LinkedNode<T> newNode = new(value, previousNode, node);
            node.PreviousNode = newNode;
            previousNode.NextNode = newNode;

            CheckCurrentReferenceToFirst(node, newNode);
            _count++;
        }

        public void InsertRangeAfter(LinkedNode<T> node, params T[] values)
        {
            AlexLinkedList<T> newList = ConvertToAlexLinkedList(values);

            LinkedNode<T> nextNode = node.NextNode;
            node.NextNode = newList._first;
            nextNode.PreviousNode = newList._last;
            newList._first.PreviousNode = node;
            newList._last.NextNode = nextNode;

            CheckCurrentReferenceToLast(node, newList._last);
            _count += newList.Count;
        }

        public void InsertRangeBefore(LinkedNode<T> node, params T[] values)
        {
            AlexLinkedList<T> newList = ConvertToAlexLinkedList(values);

            LinkedNode<T> previousNode = node.PreviousNode;
            node.PreviousNode = newList._last;
            previousNode.NextNode = newList._first;
            newList._first.PreviousNode = previousNode;
            newList._last.NextNode = node;

            CheckCurrentReferenceToFirst(node, newList._first);
            _count += newList.Count;
        }

        public void InsertFirst(T value)
        {
            LinkedNode<T> newNode = GetNodeBetweenLastAndFirst(value);
            _first = newNode;
        }

        public void Remove(T value, IAlexComparer<T> comparer = null)
        {
            LinkedNode<T> verificationNode = Last;
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            for (int counter = 0; counter < _count; counter++)
            {
                verificationNode = verificationNode.NextNode;
                if (comparer.Compare(verificationNode.Value, value) == 0)
                {
                    Remove(verificationNode);
                    return;
                }
            }
        }

        public void Remove(LinkedNode<T> node)
        {
            node = EnsureNodeIsInList(node);
            RemoveNodeInList(node);
        }

        public void RemoveFirst()
        {
            RemoveNodeInList(First);
        }

        public void RemoveLast()
        {
            RemoveNodeInList(Last);
        }

        public void Sort(IAlexComparer<T> comparer = null)
        {
            LinkedNode<T> verificationNode = Last;
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            bool arrayIsNotSorted;
            do
            {
                arrayIsNotSorted = false;

                for (int counter = 0; counter < _count - 1; counter++)
                {
                    verificationNode = verificationNode.NextNode;
                    LinkedNode<T> verificationNextNode = verificationNode.NextNode;
                    if (comparer.Compare(verificationNode.Value, verificationNextNode.Value) > 0)
                    {
                        T interimValue = verificationNode.Value;
                        verificationNode.Value = verificationNextNode.Value;
                        verificationNextNode.Value = interimValue;
                        arrayIsNotSorted = true;
                    }
                }
            }
            while (arrayIsNotSorted == true);
        }

        private static AlexLinkedList<T> ConvertToAlexLinkedList(T[] array)
        {
            AlexLinkedList<T> newLinkedList = new();

            for (int counter = 0; counter < array.Length - 1; counter++)
            {
                newLinkedList.Add(array[counter]);
            }
            return newLinkedList;
        }

        private LinkedNode<T> EnsureNodeIsInList(LinkedNode<T> node)
        {
            LinkedNode<T> verificationNode = Last;

            for (int counter = 0; counter < Count; counter++)
            {
                verificationNode = verificationNode.NextNode;
                if (node == verificationNode)
                {
                    return node;
                }
            }

            throw new ArgumentException("This node is not in the list", nameof(node));
        }

        private LinkedNode<T> GetNodeBetweenLastAndFirst(T value)
        {
            LinkedNode<T> newNode = new(value, Last, First);
            First.PreviousNode = newNode;
            Last.NextNode = newNode;
            return newNode;
        }

        private void RemoveNodeInList(LinkedNode<T> node)
        {
            LinkedNode<T> previousNode = node.PreviousNode;
            LinkedNode<T> nextNode = node.NextNode;
            nextNode.PreviousNode = previousNode;
            previousNode.NextNode = nextNode;

            CheckCurrentReferenceToFirst(node, nextNode);
            CheckCurrentReferenceToLast(node, previousNode);
            _count--;
        }

        private void CheckCurrentReferenceToFirst(LinkedNode<T> currentNode, LinkedNode<T> newNode)
        {
            if (currentNode == First)
            {
                _first = newNode;
            }
        }

        private void CheckCurrentReferenceToLast(LinkedNode<T> currentNode, LinkedNode<T> newNode)
        {
            if (currentNode == Last)
            {
                _last = newNode;
            }
        }
    }
}