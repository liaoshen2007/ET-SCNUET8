using System.Collections.Generic;

namespace ET.Server;

public enum ItemMode
{
    /// <summary>
    /// 开宝箱
    /// </summary>
    OpenBox = 1,
}

[ComponentOf(typeof (Unit))]
public class ItemComponent: Entity, IAwake, IDestroy, ILoad, ICache, ITransfer
{
    /// <summary>
    /// 所有道具字典
    /// </summary>
    public Dictionary<int, ItemData> ItemDict = new Dictionary<int, ItemData>();

    /// <summary>
    /// 期限道具
    /// </summary>
    public Dictionary<int, ItemData> ValidItemDict = new Dictionary<int, ItemData>();
}