using System.Collections;

namespace AlexCollections
{
    public class AlexLinkedList<T> : IEnumerable<T>
    {
        private int _count;
        private LinkedNode<T> _first;
        private LinkedNode<T> _last;

        public AlexLinkedList()
        {
            _count = 0;
            _first = null;
            _last = null;
        }

        public int Count 
        { 
            get 
            
            { 
                return _count; 
            } 
        }
        public LinkedNode<T> First
        {
            get
            {
                return _first;
            }            
        }
        public LinkedNode<T> Last
        {
            get
            {
                return _last;
            }
        }

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

        public void InsertFirst(T value)
        {
            LinkedNode<T> newNode = GetNodeBetweenLastAndFirst(value);
            _first = newNode;
        }

        public void Remove(LinkedNode<T> node)
        {
            node = EnsureNodeIsInList(node);

            LinkedNode<T> previousNode = node.PreviousNode;
            LinkedNode<T> nextNode = node.NextNode;
            nextNode.PreviousNode = previousNode;
            previousNode.NextNode = nextNode;

            CheckCurrentReferenceToFirst(node, nextNode);
            CheckCurrentReferenceToLast(node, previousNode);
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