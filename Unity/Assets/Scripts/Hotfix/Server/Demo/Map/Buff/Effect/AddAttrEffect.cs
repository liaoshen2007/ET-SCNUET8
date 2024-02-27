namespace ET.Server
{   
    /// <summary>
    /// 添加属性
    /// </summary>
    [Buff("AddAttr")]
    public class AddAttrEffect: ABuffEffect
    {
        protected override void OnCreate(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
            var numericCom = self.GetParent<Unit>().GetComponent<NumericComponent>();
            for (int i = 0; i < effectArg.Args.Count / 2; i++)
            {
                if (effectArg.Args[i * 2] <= 0)
                {
                    continue;
                }

                var attrType = effectArg.Args[i * 2];
                var attrValue = effectArg.Args[i * 2 + 1];
                numericCom.Add(attrType, attrValue);
            }
        }

        protected override void OnRemove(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
            var numericCom = self.GetParent<Unit>().GetComponent<NumericComponent>();
            for (int i = 0; i < effectArg.Args.Count / 2; i++)
            {
                if (effectArg.Args[i * 2] <= 0)
                {
                    continue;
                }

                var attrType = effectArg.Args[i * 2];
                var attrValue = effectArg.Args[i * 2 + 1];
                numericCom.Add(attrType, -attrValue);
            }
        }
    }
}