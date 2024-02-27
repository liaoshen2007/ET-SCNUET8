namespace ET
{
    /// <summary>
    /// 添加道具事件
    /// </summary>
    public struct AddItem
    {
        public int ItemId { get; set; }

        public long Count { get; set; }

        public long ChangeCount { get; set; }

        public int LogEvent { get; set; }
    }

    /// <summary>
    /// 移除道具事件
    /// </summary>
    public struct RemoveItem
    {
        public int ItemId { get; set; }

        public long Count { get; set; }

        public int LogEvent { get; set; }
    }
}