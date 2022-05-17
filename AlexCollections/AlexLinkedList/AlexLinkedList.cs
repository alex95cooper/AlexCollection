using System.Collections;

namespace AlexCollections
{
    public class AlexLinkedList<T> : IEnumerable<T>
    {
        private int _count;
        private LinkedNode<T> _head;

        public int Count => _count;
        public LinkedNode<T> Head => _head;

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexLinkedListEnumerator<T>(Head.PreviousNode, Count);
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
                LinkedNode<T> newNode = new(value, this);
                _head = newNode;
            }
            else if (Count == 1)
            {
                LinkedNode<T> newNode = new(value, this, Head, Head);
                Head.PreviousNode = newNode;
                Head.NextNode = newNode;
            }
            else if (Count > 1)
            {
                CreateNodeBetweenLastAndFirst(value);
            }

            _count++;
        }

        public void AddRange(AlexLinkedList<T> values)
        {
            SetBelongingToThisList(values);

            LinkedNode<T> newLastNode = values.Head.PreviousNode;
            LinkedNode<T> oldLastNode = Head.PreviousNode;

            oldLastNode.NextNode = values.Head;
            values._head.PreviousNode = oldLastNode;
            newLastNode.NextNode = Head;
            Head.PreviousNode = newLastNode;

            _count += values.Count;
        }

        public void Clear()
        {
            _count = 0;
            _head = null;
        }

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            LinkedNode<T> verificationNode = Head.PreviousNode;
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
                throw new IndexOutOfRangeException();
            }

            LinkedNode<T> verificationNode = Head.PreviousNode;

            for (int counter = 0; counter <= index; counter++)
            {
                verificationNode = verificationNode.NextNode;
            }

            return verificationNode.Value;
        }

        public int IndexOf(T value, IAlexComparer<T> comparer = null)
        {
            LinkedNode<T> verificationNode = Head.PreviousNode;
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
            LinkedNode<T> newNode = new(value, this, node, nextNode);
            node.NextNode = newNode;
            nextNode.PreviousNode = newNode;

            _count++;
        }

        public void InsertBefore(LinkedNode<T> node, T value)
        {
            node = EnsureNodeIsInList(node);

            LinkedNode<T> previousNode = node.PreviousNode;
            LinkedNode<T> newNode = new(value, this, previousNode, node);
            node.PreviousNode = newNode;
            previousNode.NextNode = newNode;

            CheckCurrentReferenceToFirst(node, newNode);
            _count++;
        }

        public void InsertRangeAfter(LinkedNode<T> node, AlexLinkedList<T> values)
        {
            node = EnsureNodeIsInList(node);
            SetBelongingToThisList(values);

            LinkedNode<T> nextNode = node.NextNode;
            LinkedNode<T> lastNodeOfValues = values.Head.PreviousNode;
            node.NextNode = values.Head;
            nextNode.PreviousNode = lastNodeOfValues;
            values._head.PreviousNode = node;
            lastNodeOfValues.NextNode = nextNode;

            _count += values.Count;
        }

        public void InsertRangeBefore(LinkedNode<T> node, AlexLinkedList<T> values)
        {
            node = EnsureNodeIsInList(node);
            SetBelongingToThisList(values);

            LinkedNode<T> previousNode = node.PreviousNode;
            LinkedNode<T> lastNodeOfValues = values.Head.PreviousNode;
            node.PreviousNode = lastNodeOfValues;
            previousNode.NextNode = values.Head;
            values.Head.PreviousNode = previousNode;
            lastNodeOfValues.NextNode = node;

            CheckCurrentReferenceToFirst(node, values._head);
            _count += values.Count;
        }

        public void InsertFirst(T value)
        {
            LinkedNode<T> newNode = CreateNodeBetweenLastAndFirst(value);
            _head = newNode;
        }

        public void Remove(T value, IAlexComparer<T> comparer = null)
        {
            LinkedNode<T> verificationNode = Head.PreviousNode;
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
            RemoveNodeInList(Head);
        }

        public void RemoveLast()
        {
            RemoveNodeInList(Head.PreviousNode);
        }

        public void Sort(IAlexComparer<T> comparer = null)
        {
            LinkedNode<T> verificationNode = Head.PreviousNode;
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

        private LinkedNode<T> EnsureNodeIsInList(LinkedNode<T> node)
        {
            if (node.ListContainingNode == this)
            {
                return node;
            }

            throw new ArgumentException("This node is not in the list", nameof(node));
        }

        private LinkedNode<T> CreateNodeBetweenLastAndFirst(T value)
        {
            LinkedNode<T> lastNode = Head.PreviousNode;
            LinkedNode<T> newNode = new(value, this, lastNode, Head);
            Head.PreviousNode = newNode;
            lastNode.NextNode = newNode;

            return newNode;
        }

        private void SetBelongingToThisList(AlexLinkedList<T> values)
        {
            LinkedNode<T> interimNode = values.Head.PreviousNode;

            for (int counter = 0; counter < values.Count; counter++)
            {
                interimNode = interimNode.NextNode;
                interimNode.ListContainingNode = this;
            }
        }

        private void RemoveNodeInList(LinkedNode<T> node)
        {
            LinkedNode<T> previousNode = node.PreviousNode;
            LinkedNode<T> nextNode = node.NextNode;
            nextNode.PreviousNode = previousNode;
            previousNode.NextNode = nextNode;

            CheckCurrentReferenceToFirst(node, nextNode);
            _count--;
        }

        private void CheckCurrentReferenceToFirst(LinkedNode<T> currentNode, LinkedNode<T> newNode)
        {
            if (currentNode == Head)
            {
                _head = newNode;
            }
        }
    }
}
