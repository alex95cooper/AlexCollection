namespace AlexList
{
    internal class DefaultAlexComparer<T> : IAlexComparer<T>
    {
        public int Compare(T x, T y)
        {
            if (x == null || y == null)
                return CompareNullReference(x, y);

            if (x.GetHashCode() > y.GetHashCode())
            {
                return 1;
            }
            else if (x.GetHashCode() < y.GetHashCode())
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        private static int CompareNullReference(T x, T y)
        {
            if (x == null && y == null)
                return 0;
            else if (x == null)
                return -1;
            else
                return 1;
        }
    }
}
