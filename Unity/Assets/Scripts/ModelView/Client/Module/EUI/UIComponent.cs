using System.Collections.Generic;

namespace ET.Client
{
    public interface IUILogic
    {
    }

    public interface IUIScrollItem
    {
    }

    [ChildOf(typeof (UIBaseWindow))]
    [ComponentOf(typeof (Scene))]
    public class UIComponent: Entity, IAwake, IDestroy
    {
        /// <summary>
        /// 全部界面
        /// </summary>
        public Dictionary<int, UIBaseWindow> AllWindowsDic { get; set; } = new Dictionary<int, UIBaseWindow>();

        public List<WindowID> UIBaseWindowlistCached { get; set; } = new List<WindowID>();

        /// <summary>
        /// 正在显示中的窗口
        /// </summary>
        public Dictionary<int, UIBaseWindow> VisibleWindowsDic { get; set; } = new Dictionary<int, UIBaseWindow>();

        public Queue<WindowID> StackWindowsQueue { get; set; } = new Queue<WindowID>();

        public bool IsPopStackWndStatus { get; set; } = false;
    }
}