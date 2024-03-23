namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.NumericNoticeComponent))]
    public static class NumericNoticeComponentSystem
    {
        public static void NoticeImmediately(this NumericNoticeComponent self, NumbericChange args)
        {

            Unit unit = self.GetParent<Unit>();
            // self.NoticeUnitNumericeMessage.UnitId = unit.Id;
            // self.NoticeUnitNumericeMessage.NumericType = args.NumericType;
            // self.NoticeUnitNumericeMessage.NewValue = args.New;
            //todo 这个要记入笔记啊，还好有论坛~~不然血坑啊~~
            M2C_NoticeUnitNumerice m2CNoticeUnitNumerice = new M2C_NoticeUnitNumerice();
            m2CNoticeUnitNumerice.UnitId=unit.Id;
            m2CNoticeUnitNumerice.NumericType = args.NumericType;
            m2CNoticeUnitNumerice.NewValue = args.New;
            
            //Log.Error("NoticeImmediately"+args.NumericType+" "+args.New);
            unit.SendToClient(m2CNoticeUnitNumerice);
        }


    }
}

