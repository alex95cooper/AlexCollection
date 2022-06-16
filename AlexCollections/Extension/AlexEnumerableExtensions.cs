namespace AlexCollections.Extension;

public static class AlexEnumerableExtensions
{
    public static AlexList<T> ToAlexList<T>(this IEnumerable<T> enumerable)
    {
        AlexList<T> alexList = new();
        foreach (var item in enumerable)
        {
            alexList.Add(item);
        }

        return alexList;
    }
}

