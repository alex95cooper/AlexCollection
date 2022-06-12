using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexCollections
{
    internal class CollectionChangedEventArgs<T> : EventArgs
    {
        public CollectionChangedEventArgs(Action action, T newValue, T oldValue, int newIndex, int oldIndex)
        {
            Action = action;
            NewValue = newValue;
            OldValue = oldValue;
            NewIndex = newIndex;
            OldIndex = oldIndex;

        }

        public Action Action { get; set; }
        public T NewValue { get; set; }
        public T OldValue { get; set; }
        public int NewIndex { get; set; }
        public int OldIndex { get; set; }
    }
}
