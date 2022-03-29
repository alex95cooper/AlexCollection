using System.Collections;

namespace AlexList
{
    public class AlexList<T> 
    {
        int indexOfElement = -1;

        private T[] elementsArray;

        public AlexList()
        {
            elementsArray = Array.Empty<T>();
        }

        #region

        public bool MoveNext()
        {
            if (indexOfElement < elementsArray.Length - 1)
            {
                indexOfElement++;
                return true;
            }
            else
            {
                Reset();
                return false;

            }
        }

        public object Current
        {
            get
            {
                return elementsArray[indexOfElement];
            }
        }

        public void Reset()
        {
            indexOfElement = -1;
        }

        public AlexList<T> GetEnumerator()
        {
            return this;
        }

        #endregion

        public void Add(T value)
        {
            Array.Resize<T>(ref elementsArray, elementsArray.Length + 1);
            elementsArray[^1] = value;
        }
    }
}