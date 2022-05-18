namespace AlexCollections
{
    public class LinkedNode<T>
    {
        internal LinkedNode(T value, AlexLinkedList<T> listContainingNode = null, LinkedNode<T> previousNode = null, LinkedNode<T> nextNode = null)
        {
            Value = value;
            PreviousNode = previousNode;
            NextNode = nextNode;
            ListContainingNode = listContainingNode;
        }

        public LinkedNode(T value, LinkedNode<T> previousNode = null, LinkedNode<T> nextNode = null)
        {
            Value = value;
            PreviousNode = previousNode;
            NextNode = nextNode;
        }

        internal AlexLinkedList<T> ListContainingNode { get; set; }

        public LinkedNode<T> PreviousNode { get; internal set; }
        public LinkedNode<T> NextNode { get; internal set; }
        
        public T Value { get; set; }
    }
}
