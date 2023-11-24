using System;
using System.Collections.Generic;

namespace ET.Client
{
    [Code]
    public class UIEvent: Singleton<UIEvent>, ISingletonAwake
    {
        public Dictionary<WindowID, IAUIEventHandler> UIEventHandlers;

        public bool IsClick { get; set; }

        public void Awake()
        {
            UIEventHandlers = new Dictionary<WindowID, IAUIEventHandler>();
            foreach (var v in CodeTypes.Instance.GetTypes(typeof (AUIEventAttribute)))
            {
                AUIEventAttribute attr = v.GetCustomAttributes(typeof (AUIEventAttribute), false)[0] as AUIEventAttribute;
                UIEventHandlers.Add(attr.WindowID, Activator.CreateInstance(v) as IAUIEventHandler);
            }
        }

        public IAUIEventHandler GetUIEventHandler(WindowID windowID)
        {
            if (UIEventHandlers.TryGetValue(windowID, out IAUIEventHandler handler))
            {
                return handler;
            }

            Log.Error($"windowId : {windowID} is not have any uiEvent");
            return null;
        }

        public void SetUIClick(bool click)
        {
            IsClick = click;
        }
    }
}