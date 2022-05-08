using System.Collections;

namespace AlexLinkedList
{
    public class AlexLinkedList<T> : IEnumerable<T>
    {
        public AlexLinkedList()
        {
            Count = 0;
            First = null;
            Last = null;
        }

        public int Count { get; set; }
        public LinkedNode<T> First { get; set; }
        public LinkedNode<T> Last { get; set; }

        #region Enumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new AlexLinkedListEnumerator<T>(Last, Count);
        }

        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        #endregion

        public void Add(T value)
        {
            if (Count == 0)
            {
                LinkedNode<T> newNode = new(value);
                First = newNode;
                Last = newNode;
            }
            else if (Count == 1)
            {
                LinkedNode<T> newNode = new(value, First, First);
                First.PreviousNode = newNode;
                First.NextNode = newNode;
                Last = newNode;
            }
            else if (Count > 1)
            {
                LinkedNode<T> newNode = GetNodeBetweenLastAndFirst(value);
                Last = newNode;
            }

            Count++;
        }

        public void InsertAfter(LinkedNode<T> node, T value)
        {
            node = EnsureNodeIsInList(node);

            LinkedNode<T> nextNode = node.NextNode;
            LinkedNode<T> newNode = new(value, node, nextNode);
            node.NextNode = newNode;
            nextNode.PreviousNode = newNode;
            if (node == Last)
            {
                Last = newNode;
            }

            Count++;
        }

        public void InsertBefore(LinkedNode<T> node, T value)
        {
            node = EnsureNodeIsInList(node);

            LinkedNode<T> previousNode = node.PreviousNode;
            LinkedNode<T> newNode = new(value, previousNode, node);
            node.PreviousNode = newNode;
            previousNode.NextNode = newNode;
            if (node == First)
            {
                First = newNode;
            }

            Count++;
        }

        public void InsertFirst(T value)
        {
            LinkedNode < T > newNode = GetNodeBetweenLastAndFirst(value);
            First = newNode;
        }

        private LinkedNode<T> EnsureNodeIsInList(LinkedNode<T> node)
        {
            LinkedNode<T> verificationNode = Last;

            for (int i = 0; i < Count; i++)
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
    }
}