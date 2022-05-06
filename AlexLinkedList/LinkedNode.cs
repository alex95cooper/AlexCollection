namespace AlexLinkedList
{
    public class LinkedNode<T>
    {
        public LinkedNode(int index, T value, LinkedNode<T> previousNode = null, LinkedNode<T> nextNode = null)
        {
            Index = index;
            Value = value;
            PreviousNode = previousNode;
            NextNode = nextNode;
        }

        public int Index { get; set; }
        public T Value { get; set; }        
        public LinkedNode<T> PreviousNode { get; set; }
        public LinkedNode<T> NextNode { get; set; }
    }
}
