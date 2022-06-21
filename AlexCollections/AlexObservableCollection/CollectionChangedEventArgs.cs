namespace AlexCollections
{
    public class CollectionChangedEventArgs<T> : EventArgs
    {
        public CollectionChangedEventArgs(Action action, AlexList<T> newValues, AlexList<T> oldValues, int newIndex, int oldIndex)
        {
            Action = action;
            NewValues = newValues;
            OldValues = oldValues;
            NewIndex = newIndex;
            OldIndex = oldIndex;

        }

        public Action Action { get; }
        public AlexList<T> NewValues { get; }
        public AlexList<T> OldValues { get; }
        public int NewIndex { get; }
        public int OldIndex { get; }
    }
}
