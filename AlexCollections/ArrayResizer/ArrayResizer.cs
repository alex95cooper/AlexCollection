namespace AlexCollections
{
    internal static class ArrayResizer<T>
    {
        public static T[] Resize(int newLenght, T[] elementsArray)
        {
            T[] interimElementsArray = new T[newLenght];
            for (int counter = 0; counter < elementsArray.Length; counter++)
            {
                interimElementsArray[counter] = elementsArray[counter];
            }

            return interimElementsArray;
        }
    }
}