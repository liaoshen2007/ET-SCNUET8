namespace ET.Server
{
    [Buff("Test")]
    public class TestEffect: ABuffEffect
    {
        protected override void OnCreate(BuffComponent self, Buff buff, EffectArg effectArg)
        {
            Log.Info("------OnCreate---------");
        }

        protected override void OnRemove(BuffComponent self, Buff buff, EffectArg effectArgf)
        {
            Log.Info("------OnRemove---------");
        }
    }
}