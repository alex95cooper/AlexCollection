namespace AlexLinkedList
{
    public class LinkedNode<T>
    {
        public int Index { get; set; }
        public T Value { get; set; }        
        public LinkedNode<T> PreviousNode { get; set; }
        public LinkedNode<T> NextNode { get; set; }
    }
}
