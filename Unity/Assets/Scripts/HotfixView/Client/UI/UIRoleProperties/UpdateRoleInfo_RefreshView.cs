namespace ET.Client
{
    [Event(SceneType.Client)]
    public class UpdateRoleInfo_RefreshView: AEvent<Scene, RefreshRoleInfo>
    {
        protected override async ETTask Run(Scene scene, RefreshRoleInfo a)
        {
            scene.GetComponent<UIComponent>().GetDlgLogic<UIRoleProperties>()?.Refresh();
            await ETTask.CompletedTask;
        }
    }
}

