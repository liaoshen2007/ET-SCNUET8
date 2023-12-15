namespace ET.Client;

[Event(SceneType.Demo)]
public class UpdateGroupHandler: AEvent<Scene, UpdateGroup>
{
    protected override async ETTask Run(Scene scene, UpdateGroup a)
    {
        var chat = scene.GetComponent<UIComponent>().GetDlgLogic<UIChat>(true);
        if (chat == null)
        {
            return;
        }

        await ETTask.CompletedTask;
    }
}