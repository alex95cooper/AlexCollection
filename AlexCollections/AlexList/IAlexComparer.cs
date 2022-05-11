namespace AlexCollections
{
    public interface IAlexComparer<T>
    {
        int Compare(T x, T y);
    }
}
