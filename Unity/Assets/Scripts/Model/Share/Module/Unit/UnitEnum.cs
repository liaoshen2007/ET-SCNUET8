namespace ET
{
    /// <summary>
    /// 元素类型
    /// </summary>
    public enum ElementType
    {
        None = 0,

        /// <summary>
        /// 火元素攻击
        /// </summary>
        Fire = 1,

        /// <summary>
        /// 雷元素攻击 
        /// </summary>
        Thunder = 2,

        /// <summary>
        /// 冰元素攻击
        /// </summary>
        Ice = 3,
    }

    /// <summary>
    /// 目标类型
    /// </summary>
    public enum FocusType
    {
        /// <summary>
        /// 任意
        /// </summary>
        All = 1,

        /// <summary>
        /// 自身
        /// </summary>
        Self,

        /// <summary>
        /// 敌对 
        /// </summary>
        OOP,

        /// <summary>
        /// 友善
        /// </summary>
        Friend,

        /// <summary>
        /// 友善排己
        /// </summary>
        FriendNSelf,

        /// <summary>
        /// 队友
        /// </summary>
        Team,

        /// <summary>
        /// 队友排己
        /// </summary>
        TeamNSelf,
    }

    /// <summary>
    /// 范围类型
    /// </summary>
    public enum RangeType
    {
        /// <summary>
        /// 自身
        /// </summary>
        Self = 1,

        /// <summary>
        /// 单一目标
        /// </summary>
        Single,
        
        Much,
        
        /// <summary>
        /// 自身扇形
        /// </summary>
        SelfFan,

        /// <summary>
        /// 自身直线
        /// </summary>
        SelfLine,

        /// <summary>
        /// 目标直线
        /// </summary>
        DstLine,

        /// <summary>
        /// 目标扇形
        /// </summary>
        DstFan,

        /// <summary>
        /// 目标扇形+直线
        /// </summary>
        DstFanLine,

        /// <summary>
        /// 使用上次(技能)
        /// </summary>
        UseLast,
    }

    /// <summary>
    /// 角色类型定义
    /// </summary>
    public enum RoleType
    {
        Npc = 0,

        Player = 1,

        /// <summary>
        /// 怪物
        /// </summary>
        Monster = 2,

        /// <summary>
        /// 机关
        /// </summary>
        Machine = 3,

        /// <summary>
        /// 采集物
        /// </summary>
        Collect = 4,

        /// <summary>
        /// 掉落物
        /// </summary>
        Drop = 5,

        /// <summary>
        /// 宝箱
        /// </summary>
        Chest = 6,

        /// <summary>
        /// 召唤物
        /// </summary>
        Summon = 7,

        /// <summary>
        /// 环境生物
        /// </summary>
        Env = 10,
    }

    /// <summary>
    /// 角色阵营定义
    /// </summary>
    public enum CampType
    {
        Npc = 0,

        /// <summary>
        /// 怪物阵营
        /// </summary>
        Monster = 1,

        /// <summary>
        /// 玩家阵营
        /// </summary>
        Player = 2,
    }

    /// <summary>
    /// 角色能力码
    /// </summary>
    public enum RoleAbility
    {
        /// <summary>
        /// 普通攻击
        /// </summary>
        Attack = 1,

        /// <summary>
        /// 免疫
        /// </summary>
        ImmUnity = 2,

        /// <summary>
        /// 使用道具
        /// </summary>
        UseItem = 3,

        /// <summary>
        /// 隐身
        /// </summary>
        Hide = 4,

        /// <summary>
        /// 使用技能
        /// </summary>
        Skill = 5,

        Invincible = 6,

        /// <summary>
        /// 移动
        /// </summary>
        Move = 7,

        /// <summary>
        /// 操作移动
        /// </summary>
        HandlerMove = 8,

        /// <summary>
        /// 无法选中
        /// </summary>
        UnSelectable = 11,

        End = 32,
    }
}