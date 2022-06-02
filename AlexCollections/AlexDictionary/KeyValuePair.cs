namespace AlexCollections
{
    public readonly struct KeyValuePair<TKey, TValue>
    {        
        public KeyValuePair(TValue value, TKey key)
        {
            Value = value;
            Key = key;
        }

        public TKey Key { get; }
        public TValue Value { get; }
    }
}
