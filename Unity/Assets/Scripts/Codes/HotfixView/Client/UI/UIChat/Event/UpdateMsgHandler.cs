namespace ET.Client;

[Event(SceneType.Demo)]
public class UpdateMsgHandler: AEvent<Scene, UpdateMsg>
{
    protected override async ETTask Run(Scene scene, UpdateMsg a)
    {
        UIChat chat = scene.GetComponent<UIComponent>().GetDlgLogic<UIChat>(true);
        if (chat == null)
        {
            return;
        }

        chat.AddMsg(a.Msg);
        await ETTask.CompletedTask;
    }
}