using System;

namespace ET
{
    public abstract class ABuffEffect : IBuffEffect
    {
        public bool IsBsEffect { get; set; }

        public abstract void OnCheckIn();

        public abstract void OnCheckOut();

        public void Create()
        {
            try
            {
                OnCreate();
            }
            catch (Exception e)
            {
                Log.Error($"执行Buff创建事件错误: {e}");
            }
        }

        public void Update()
        {
            try
            {
                OnUpdate();
            }
            catch (Exception e)
            {
                Log.Error($"执行Buff创建事件错误: {e}");
            }
        }

        public void Event(BuffEvent buffEvent, object args)
        {
            try
            {
                OnEvent(buffEvent, args);
            }
            catch (Exception e)
            {
                Log.Error($"执行Buff事件错误: {buffEvent} {e}");
            }
        }

        public void TimeOut()
        {
            try
            {
                OnTimeOut();
            }
            catch (Exception e)
            {
                Log.Error($"执行Buff超时事件错误: {e}");
            }
        }

        public void Remove()
        {
            try
            {
                OnRemove();
            }
            catch (Exception e)
            {
                Log.Error($"执行Buff移除事件错误: {e}");
            }
        }

        protected virtual void OnCreate()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnEvent(BuffEvent buffEvent, object args)
        {
        }

        protected virtual void OnTimeOut()
        {
        }

        protected virtual void OnRemove()
        {
        }
    }
}