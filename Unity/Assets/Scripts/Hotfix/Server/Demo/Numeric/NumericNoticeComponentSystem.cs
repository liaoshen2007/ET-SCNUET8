namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.NumericNoticeComponent))]
    public static class NumericNoticeComponentSystem
    {
        public static void NoticeImmediately(this NumericNoticeComponent self, NumbericChange args)
        {
            Unit unit = self.GetParent<Unit>();
            self.NoticeUnitNumericeMessage.UnitId = unit.Id;
            self.NoticeUnitNumericeMessage.NumericType = args.NumericType;
            self.NoticeUnitNumericeMessage.NewValue = args.New;
            unit.SendToClient(self.NoticeUnitNumericeMessage);

        }


    }
}

