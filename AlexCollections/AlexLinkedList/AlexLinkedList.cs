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

        public LinkedNode<T> Add(T value)
        {
            LinkedNode<T> newNode;
            if (Count == 0)
            {
                newNode = new(value, this);
                newNode.PreviousNode = newNode;
                newNode.NextNode = newNode;
                _head = newNode;
            }
            else if (Count == 1)
            {
                newNode = new(value, this, Head, Head);
                Head.PreviousNode = newNode;
                Head.NextNode = newNode;
            }
            else
            {
                newNode = CreateNodeBetweenLastAndFirst(value);
            }

            _count++;
            return newNode;
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
            (_, int index) = GetNodeWithIndex(value, comparer);
            if (index == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public T GetByIndex(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new IndexOutOfRangeException();
            }

            LinkedNode<T> verificationNode = Head;

            for (int counter = 0; counter < index; counter++)
            {
                verificationNode = verificationNode.NextNode;
            }

            return verificationNode.Value;
        }

        public int IndexOf(T value, IAlexComparer<T> comparer = null)
        {
            (_, int index) = GetNodeWithIndex(value, comparer);
            return index;
        }

        public LinkedNode<T> InsertAfter(LinkedNode<T> node, T value)
        {
            node = EnsureNodeIsInList(node);

            LinkedNode<T> nextNode = node.NextNode;
            LinkedNode<T> newNode = new(value, this, node, nextNode);
            node.NextNode = newNode;
            nextNode.PreviousNode = newNode;

            _count++;
            return newNode;
        }

        public LinkedNode<T> InsertBefore(LinkedNode<T> node, T value)
        {
            node = EnsureNodeIsInList(node);

            LinkedNode<T> previousNode = node.PreviousNode;
            LinkedNode<T> newNode = new(value, this, previousNode, node);
            node.PreviousNode = newNode;
            previousNode.NextNode = newNode;

            ShiftHeadIfNeeded(node, newNode);
            _count++;
            return newNode;
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

            ShiftHeadIfNeeded(node, values._head);
            _count += values.Count;
        }

        public LinkedNode<T> InsertFirst(T value)
        {
            LinkedNode<T> newNode = CreateNodeBetweenLastAndFirst(value);
            _count++;
            _head = newNode;
            return newNode;
        }

        public void Remove(T value, IAlexComparer<T> comparer = null)
        {
            (LinkedNode<T> node, _) = GetNodeWithIndex(value, comparer);

            if (node != null)
            {
                Remove(node);
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
            LinkedNode<T> verificationNode = Head;
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            bool arrayIsNotSorted = false;
            do
            {
                for (int counter = 0; counter < _count - 1; counter++)
                {
                    LinkedNode<T> verificationNextNode = verificationNode.NextNode;
                    if (comparer.Compare(verificationNode.Value, verificationNextNode.Value) > 0)
                    {
                        T interimValue = verificationNode.Value;
                        verificationNode.Value = verificationNextNode.Value;
                        verificationNextNode.Value = interimValue;
                        arrayIsNotSorted = true;
                    }

                    verificationNode = verificationNode.NextNode;
                }
            }
            while (arrayIsNotSorted == true);
        }

        private LinkedNode<T> EnsureNodeIsInList(LinkedNode<T> node)
        {
            if (node.ListContainingNode != this)
            {
                throw new ArgumentException("This node is not in the list", nameof(node));
            }

            return node;
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
            LinkedNode<T> interimNode = values.Head;

            for (int counter = 0; counter < values.Count; counter++)
            {
                interimNode.ListContainingNode = this;
                interimNode = interimNode.NextNode;
            }
        }

        private (LinkedNode<T> node, int index) GetNodeWithIndex(T value, IAlexComparer<T> comparer = null)
        {
            LinkedNode<T> verificationNode = Head;
            comparer = DefaultAlexComparer<T>.GetComparerOrDefault(comparer);

            for (int counter = 0; counter < _count; counter++)
            {
                if (comparer.Compare(verificationNode.Value, value) == 0)
                {
                    return (verificationNode, Count);
                }

                verificationNode = verificationNode.NextNode;
            }

            return (null, -1);
        }

        private void RemoveNodeInList(LinkedNode<T> node)
        {
            LinkedNode<T> previousNode = node.PreviousNode;
            LinkedNode<T> nextNode = node.NextNode;
            nextNode.PreviousNode = previousNode;
            previousNode.NextNode = nextNode;

            ShiftHeadIfNeeded(node, nextNode);
            _count--;
        }

        private void ShiftHeadIfNeeded(LinkedNode<T> shiftableNode, LinkedNode<T> replacingNode)
        {
            if (shiftableNode == Head)
            {
                _head = replacingNode;
            }
        }
    }
}
