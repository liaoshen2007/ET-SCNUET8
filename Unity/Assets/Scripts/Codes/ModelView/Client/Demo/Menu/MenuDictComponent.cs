using System.Collections.Generic;

namespace ET.Client;

[ComponentOf]
public class MenuDictComponent: Entity, IAwake
{
    public Dictionary<int, Scroll_Item_Menu> MenuDic { get; } = new Dictionary<int, Scroll_Item_Menu>();

    public int SelectId { get; set; }
}