using System;

namespace ET.Server
{
    public abstract partial class ABuffEffect : IBuffEffect
    {
        public bool IsBsEffect { get; set; }

        public virtual void OnCheckIn()
        {
            
        }

        public virtual void OnCheckOut()
        {
            
        }

        public void Create(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
            try
            {
                OnCreate(self, buff, effectArg);
            }
            catch (Exception e)
            {
                Log.Error($"执行Buff创建事件错误: {e}");
            }
        }

        public void Update(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
            try
            {
                OnUpdate(self, buff, effectArg);
            }
            catch (Exception e)
            {
                Log.Error($"执行Buff创建事件错误: {e}");
            }
        }

        public void Event(BuffComponent self, BuffEvent buffEvent, BuffUnit buff, EffectArg effectArg)
        {
            try
            {
                OnEvent(self, buffEvent, buff, effectArg);
            }
            catch (Exception e)
            {
                Log.Error($"执行Buff事件错误: {buffEvent} {e}");
            }
        }

        public void TimeOut(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
            try
            {
                OnTimeOut(self, buff, effectArg);
            }
            catch (Exception e)
            {
                Log.Error($"执行Buff超时事件错误: {e}");
            }
        }

        public void Remove(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
            try
            {
                OnRemove(self, buff, effectArg);
            }
            catch (Exception e)
            {
                Log.Error($"执行Buff移除事件错误: {e}");
            }
        }

        protected virtual void OnCreate(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
        }

        protected virtual void OnUpdate(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
        }

        protected virtual void OnEvent(BuffComponent self, BuffEvent buffEvent, BuffUnit buff, EffectArg effectArg)
        {
        }

        protected virtual void OnTimeOut(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
        }

        protected virtual void OnRemove(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
        }
    }
}