namespace ET
{
    public interface IBuffEffect
    {
        /// <summary>
        /// 天赋是否生效过
        /// </summary>
        bool IsBsEffect {get; set;}

        void OnCheckIn();
        
        void OnCheckOut();
        
        /// <summary>
        /// Buff 创建时
        /// </summary>
        void Create();

        /// <summary>
        /// Buff 时间间隔到
        /// </summary>
        void Update();
        
        /// <summary>
        /// 触发Buff事件时
        /// </summary>
        /// <param name="buffEvent">Buff事件类型</param>
        /// <param name="args">事件参数</param>
        void Event(BuffEvent buffEvent, object args);
        
        /// <summary>
        /// 计时时间到
        /// </summary>
        void TimeOut();

        /// <summary>
        /// buff 移除时
        /// </summary>
        void Remove();
    }
}