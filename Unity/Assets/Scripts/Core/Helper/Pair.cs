namespace ET
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Pair<T>
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public T Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Default value
        /// </summary>
        [StaticField]
        public static readonly Pair<T> Default = new Pair<T>(default(T), default(T));
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public Pair(T key, T value)
        {
            Key = key;
            Value = value;
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public struct Pair<TKey, TValue>
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Default value
        /// </summary>
        [StaticField]
        public static readonly Pair<TKey, TValue> Default = new Pair<TKey, TValue>(default(TKey), default(TValue));
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
        #endregion
    }
}
