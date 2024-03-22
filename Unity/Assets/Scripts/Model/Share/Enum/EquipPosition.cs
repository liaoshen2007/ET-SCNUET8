namespace ET
{


    /// <summary>
    /// 装备装配位置
    /// </summary>
    public enum EquipPosition
    {
        None=0,//不可装备
        Head=1,//头盔
        Clothes=2,//鞋子
        Shoes=3,//鞋子
        Ring=4,//戒指
        Weapon=5,//武器
        Shield=6,//盾牌
    }

    /// <summary>
    /// 装备操作指令
    /// </summary>
    public enum EquipOp
    {
        Load,//穿戴
        Unload,//卸下
        
    }


}