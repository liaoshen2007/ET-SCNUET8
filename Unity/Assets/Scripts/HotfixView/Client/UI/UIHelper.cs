namespace ET.Client
{
    public static class UIHelper
    {
        public static void PopMsg(Entity entity, string msg)
        {
            entity.Root().GetComponent<UIComponent>().GetDlgLogic<UIPop>().PopMsg(msg);
        }
    }
}