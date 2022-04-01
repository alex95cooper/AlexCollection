using System.Collections;

namespace AlexList
{
    public class AlexList<T>
    {
        private int indexOfElement = -1;

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

        public void Clear()
        {
            Array.Clear(elementsArray);
        }

        public void Contains()
        {
            
        }

        public int IndexOf(T value)
        {
            return Array.IndexOf(elementsArray, value);
        }

        public void Insert(T value, int index)
        {
            Array.Resize<T>(ref elementsArray, elementsArray.Length + 1);

            for (int counter = elementsArray.Length - 2; counter >= index; counter--)
            {
                elementsArray[counter + 1] = elementsArray[counter];
            }

            elementsArray[index] = value;
        }

        public void Remove(T value)
        {
            int index = Array.IndexOf(elementsArray, value);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            for (int counter = index; counter < elementsArray.Length - 1; counter++)
            {
                elementsArray[counter] = elementsArray[counter + 1];
            }

            Array.Resize<T>(ref elementsArray, elementsArray.Length - 1);
        }

        public void Sort()
        {

        }
    }
}