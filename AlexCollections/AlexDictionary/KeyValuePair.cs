namespace AlexCollections
{
    public readonly struct KeyValuePair<TKey, TValue>
    {        
        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;            
        }

        public TKey Key { get; }
        public TValue Value { get; }
    }
}
