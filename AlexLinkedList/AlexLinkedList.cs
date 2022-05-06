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
                LinkedNode<T> newNode = new(Count, value);
                First = newNode;
                Last = newNode;
            }
            else if (Count == 1)
            {
                LinkedNode<T> newNode = new(Count, value, First, First);
                First.PreviousNode = newNode;
                First.NextNode = newNode;
                Last = newNode;
            }
            else if (Count > 1)
            {
                LinkedNode<T> newNode = new(Count, value, Last, First);
                First.PreviousNode = newNode;
                Last.NextNode = newNode;
                Last = newNode;
            }

            Count++;
        }
    }
}