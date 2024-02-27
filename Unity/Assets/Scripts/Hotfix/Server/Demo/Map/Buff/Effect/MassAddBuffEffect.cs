namespace ET.Server
{
    /// <summary>
    /// 给周围目标添加Buff
    /// BuffId;Buff时长;最大数量;万分比
    /// </summary>
    [Buff("MassAddBuff")]
    public class MassAddBuffEffect: ABuffEffect
    {
        protected override void OnTimeOut(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
            int buffId = effectArg.Args[0];
            int time = effectArg.Args[1];
            int maxCount = effectArg.Args[2];
            int pct = effectArg.Args[3];
        }
    }
}