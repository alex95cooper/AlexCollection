namespace AlexLinkedList
{
    public class AlexLinkedList<T>
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

    }
}