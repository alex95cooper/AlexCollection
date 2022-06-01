namespace AlexCollections
{
    internal static class ArrayResizer<T>
    {
        public static T[] Resize(int newLength, T[] elementsArray)
        {
            T[] interimElementsArray = new T[newLength];
            for (int counter = 0; counter < elementsArray.Length; counter++)
            {
                interimElementsArray[counter] = elementsArray[counter];
            }

            return interimElementsArray;
        }
    }
}