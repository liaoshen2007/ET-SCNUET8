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
        public Dictionary<int, UIBaseWindow> AllWindowsDic = new Dictionary<int, UIBaseWindow>();

        public List<WindowID> UIBaseWindowlistCached = new List<WindowID>();

        /// <summary>
        /// 正在显示中的窗口
        /// </summary>
        public Dictionary<int, UIBaseWindow> VisibleWindowsDic = new Dictionary<int, UIBaseWindow>();

        /// <summary>
        /// 显示中的窗口队列
        /// </summary>
        public List<UIBaseWindow> ShowWindowsList = new List<UIBaseWindow>();

        public Queue<WindowID> StackWindowsQueue = new Queue<WindowID>();

        public bool IsPopStackWndStatus = false;

        public Dictionary<string, string> AtlasPath = new Dictionary<string, string>();
    }
}