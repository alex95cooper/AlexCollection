namespace AlexCollections
{
    internal class ElementsArray<T>
    {
        public static void ResizeArray(int newArrayLenght, int count, ref T[] elementsArray)
        {
            T[] interimElementsArray = new T[newArrayLenght];
            for (int counter = 0; counter < count; counter++)
            {
                interimElementsArray[counter] = elementsArray[counter];
            }

            elementsArray = interimElementsArray;
        }
    }
}
