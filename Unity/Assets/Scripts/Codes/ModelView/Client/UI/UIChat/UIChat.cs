namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    public class UIChat: Entity, IAwake, IUILogic
    {
        public UIChatViewComponent View
        {
            get => GetParent<UIBaseWindow>().GetComponent<UIChatViewComponent>();
        }

        public const string Sep = "$blz$";
        public const string SpecSep = "\\u{2042}";
        public UComTweener moveTween;
    }
}