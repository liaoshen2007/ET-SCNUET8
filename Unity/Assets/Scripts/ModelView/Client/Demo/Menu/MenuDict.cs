using System.Collections.Generic;

namespace ET.Client
{
    [EnableMethod]
    public class MenuDict: Entity, IAwake
    {
        public Dictionary<int, Scroll_Item_Menu> MenuDic { get; } = new();

        public int SelectId { get; set; }

        public Scroll_Item_Menu GetCurrentMenu()
        {
            return this.MenuDic[this.SelectId];
        }

        public int GetGroupId()
        {
            return this.GetCurrentMenu().MeunData.Config.GroupId;
        }
    }
}