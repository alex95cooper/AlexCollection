using System.Collections;

namespace AlexLinkedList
{
    public class AlexLinkedListEnumerator<T> : IEnumerator<T>
    {        
        readonly int _listSize;    
        
        private LinkedNode<T> _currentNode;
        private int _indexOfElement = 0;

        public AlexLinkedListEnumerator(LinkedNode<T> node, int listSize)
        {
            _currentNode = node;
            _listSize = listSize;
        }

        public bool MoveNext()
        {
            
            if (_indexOfElement < _listSize)
            {
                _currentNode = _currentNode.NextNode;
                _indexOfElement++;
                return true;
            }
            else 
            {
                Reset();
                return false;                
            }
        }

        public void Reset()
        {
            _indexOfElement = 0;
        }

        public T Current
        {
            get
            {
                return _currentNode.Value;
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
