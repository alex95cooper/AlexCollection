namespace AlexLinkedList
{
    public class LinkedNode<T>
    {
        public LinkedNode(T value, LinkedNode<T> previousNode = null, LinkedNode<T> nextNode = null)
        {
            Value = value;
            PreviousNode = previousNode;
            NextNode = nextNode;
        }

        public T Value { get; set; }        
        public LinkedNode<T> PreviousNode { get; set; }
        public LinkedNode<T> NextNode { get; set; }
    }
}
