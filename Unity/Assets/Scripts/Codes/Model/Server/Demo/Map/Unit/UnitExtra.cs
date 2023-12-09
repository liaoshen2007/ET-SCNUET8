namespace ET.Server
{
    /// <summary>
    /// 玩家额外数据
    /// </summary>
    [ComponentOf(typeof (Unit))]
    public class UnitExtra: Entity, IAwake, ITransfer, ICache
    {
        /// <summary>
        /// 玩家数字ID
        /// </summary>
        public long Gid { get; set; }

        public string UserUid { get; set; }

        /// <summary>
        /// 玩家昵称
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// vip等级
        /// </summary>
        public int VipLevel { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadIcon { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 总战力
        /// </summary>
        public long TotalFight { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public long LoginTime { get; set; }

        /// <summary>
        /// 下线时间
        /// </summary>
        public long LogoutTime { get; set; }

        /// <summary>
        /// 总在线时间
        /// </summary>
        public long TotalOnlineTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 上上次零点时间
        /// </summary>
        public long LastestZeroTime { get; set; }

        /// <summary>
        /// 上次零点时间
        /// </summary>
        public long LastZeroTime { get; set; }

        public int Channel { get; set; }

        /// <summary>
        /// 上次周零点时间
        /// </summary>
        public long LastWeekTime { get; set; }

        /// <summary>
        /// 上次月零点时间
        /// </summary>
        public long LastMonthTime { get; set; }

        /// <summary>
        /// 总充值金额
        /// </summary>
        public long TotalCharge { get; set; }
    }
}