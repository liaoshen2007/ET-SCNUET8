namespace ET.Client;

[Event(SceneType.Demo)]
public class ChatMenuSelectEventHandler: AEvent<Scene, MenuSelectEvent>
{
    protected override async ETTask Run(Scene scene, MenuSelectEvent a)
    {
        if (a.MenuType != SystemMenuType.Chat)
        {
            return;
        }

        scene.GetComponent<UIComponent>().GetDlgLogic<UIChat>().RefreshChatList(a.Index);
        await ETTask.CompletedTask;
    }
}