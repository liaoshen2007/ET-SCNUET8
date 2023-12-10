namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    public class UIChat: Entity, IAwake, IUILogic
    {
        public UIChatViewComponent View
        {
            get => GetParent<UIBaseWindow>().GetComponent<UIChatViewComponent>();
        }
        
        public UComTweener moveTween;
    }
}