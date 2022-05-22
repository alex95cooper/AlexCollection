using System.Collections;

namespace AlexCollections
{
    public class AlexLinkedList<T> : IEnumerable<T>
    {
        private const string ListIsNullExceptionMessage = "AlexLinkedList is passed as null";

        private int _count;
        private LinkedNode<T> _head;

        public int Count => _count;
        public LinkedNode<T> Head => _head;
        public LinkedNode<T> Last => _head.PreviousNode;

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
                _count++;
            }
            else
            {
                newNode = InsertNode(Last, Head, value);
            }

            return newNode;
        }

        public void AddRange(AlexLinkedList<T> values)
        {
            if (Count == 0)
            {
                if (values == null)
                {
                    throw new ArgumentNullException(nameof(values), ListIsNullExceptionMessage);
                }
                SetBelongingToThisList(values);
                _head = values.Head;
            }
            else
            {
                InsertRange(Last, Head, values);
            }
        }

        public void Clear()
        {
            _count = 0;
            _head = null;
        }

        public bool Contains(T value, IAlexComparer<T> comparer = null)
        {
            (_, int index) = GetNodeWithIndex(value, comparer);
            return index != -1;
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
            EnsureNodeValid(node);
            LinkedNode<T> newNode = InsertNode(node, node.NextNode, value);
            return newNode;
        }

        public LinkedNode<T> InsertBefore(LinkedNode<T> node, T value)
        {
            EnsureNodeValid(node);
            LinkedNode<T> newNode = InsertNode(node.PreviousNode, node, value);
            ShiftHeadIfNeeded(node, newNode);
            return newNode;
        }

        public void InsertRangeAfter(LinkedNode<T> node, AlexLinkedList<T> values)
        {
            EnsureNodeValid(node);
            InsertRange(node, node.NextNode, values);
        }

        public void InsertRangeBefore(LinkedNode<T> node, AlexLinkedList<T> values)
        {
            EnsureNodeValid(node);
            InsertRange(node.PreviousNode, node, values);
            ShiftHeadIfNeeded(node, values._head);
        }

        public LinkedNode<T> InsertFirst(T value)
        {
            LinkedNode<T> newNode = InsertNode(Last, Head, value);
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
            EnsureNodeValid(node);
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

        private void EnsureNodeValid(LinkedNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Node is passed as null");
            }

            EnsureNodeIsInList(node);
        }

        private void EnsureNodeIsInList(LinkedNode<T> node)
        {
            if (node.ListContainingNode != this)
            {
                throw new ArgumentException("This node is not in the list", nameof(node));
            }
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

        private LinkedNode<T> InsertNode(LinkedNode<T> previousNode, LinkedNode<T> nextNode, T value)
        {
            LinkedNode<T> newNode = new(value, this, previousNode, nextNode);
            previousNode.NextNode = newNode;
            nextNode.PreviousNode = newNode;

            _count++;
            return newNode;
        }

        private void InsertRange(LinkedNode<T> previousNode, LinkedNode<T> nextNode, AlexLinkedList<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values), ListIsNullExceptionMessage);
            } 

            SetBelongingToThisList(values);

            LinkedNode<T> lastNodeOfValues = values.Head.PreviousNode;
            nextNode.PreviousNode = lastNodeOfValues;
            previousNode.NextNode = values.Head;
            values.Head.PreviousNode = previousNode;
            lastNodeOfValues.NextNode = nextNode;

            _count += values.Count;
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
