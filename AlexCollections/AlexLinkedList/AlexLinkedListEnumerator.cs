using System.Collections;

namespace AlexCollections
{
    public class AlexLinkedListEnumerator<T> : IEnumerator<T>
    {        
        private readonly int _listSize;    
        
        private LinkedNode<T> _currentNode;
        private int _indexOfElement = 0;

        public AlexLinkedListEnumerator(LinkedNode<T> node, int listSize)
        {
            _currentNode = node;
            _listSize = listSize;
        }

        public T Current => _currentNode.Value;

        object IEnumerator.Current => Current;

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

        void IDisposable.Dispose()
        {            
        }
    }
}
