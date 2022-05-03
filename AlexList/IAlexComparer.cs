namespace AlexList
{
    public interface IAlexComparer<T>
    {
        int Compare(T x, T y);
    }
}
