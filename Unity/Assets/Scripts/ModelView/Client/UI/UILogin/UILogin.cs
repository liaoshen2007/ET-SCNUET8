using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    public class UILogin: Entity, IAwake, IUILogic
    {
        public UILoginViewComponent View
        {
            get => GetParent<UIBaseWindow>().GetComponent<UILoginViewComponent>();
        }
        
        public Dictionary<int, Scroll_Item_Server> ScrollItemServerTests;
    }
}