using System;

namespace ET.Server
{
    public abstract class ABuffEffect : IBuffEffect
    {
        public bool IsBsEffect { get; set; }

        public abstract void OnCheckIn();

        public abstract void OnCheckOut();

        public void Create(BuffComponent self)
        {
            try
            {
                OnCreate(self);
            }
            catch (Exception e)
            {
                self.Fiber().Error($"执行Buff创建事件错误: {e}");
            }
        }

        public void Update(BuffComponent self)
        {
            try
            {
                OnUpdate(self);
            }
            catch (Exception e)
            {
                self.Fiber().Error($"执行Buff创建事件错误: {e}");
            }
        }

        public void Event(BuffComponent self, BuffEvent buffEvent)
        {
            try
            {
                OnEvent(self, buffEvent);
            }
            catch (Exception e)
            {
                self.Fiber().Error($"执行Buff事件错误: {buffEvent} {e}");
            }
        }

        public void TimeOut(BuffComponent self)
        {
            try
            {
                OnTimeOut(self);
            }
            catch (Exception e)
            {
                self.Fiber().Error($"执行Buff超时事件错误: {e}");
            }
        }

        public void Remove(BuffComponent self)
        {
            try
            {
                OnRemove(self);
            }
            catch (Exception e)
            {
                self.Fiber().Error($"执行Buff移除事件错误: {e}");
            }
        }

        protected virtual void OnCreate(BuffComponent self)
        {
        }

        protected virtual void OnUpdate(BuffComponent self)
        {
        }

        protected virtual void OnEvent(BuffComponent self, BuffEvent buffEvent)
        {
        }

        protected virtual void OnTimeOut(BuffComponent self)
        {
        }

        protected virtual void OnRemove(BuffComponent self)
        {
        }
    }
}