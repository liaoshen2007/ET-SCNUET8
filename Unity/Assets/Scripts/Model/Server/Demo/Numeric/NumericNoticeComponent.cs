namespace ET.Server
{
    [ComponentOf(typeof(Unit))]
    public class NumericNoticeComponent:Entity,IAwake
    {
        public M2C_NoticeUnitNumerice NoticeUnitNumericeMessage = new M2C_NoticeUnitNumerice();

    }
}

