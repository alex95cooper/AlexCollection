namespace AlexCollections
{
    public class OnCollectionChangedEventArgs<T> : EventArgs
    {
        public OnCollectionChangedEventArgs(Action action, AlexList<T> newValues, AlexList<T> oldValues, int newIndex, int oldIndex)
        {
            Action = action;
            NewValues = newValues;
            OldValues = oldValues;
            NewIndex = newIndex;
            OldIndex = oldIndex;

        }

        public Action Action { get; set; }
        public AlexList<T> NewValues { get; set; }
        public AlexList<T> OldValues { get; set; }
        public int NewIndex { get; set; }
        public int OldIndex { get; set; }
    }
}
