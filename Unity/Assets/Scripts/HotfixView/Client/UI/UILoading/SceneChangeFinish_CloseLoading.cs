namespace ET.Client
{
    [Event(SceneType.Client)]
    public class SceneChangeFinish_CloseLoading: AEvent<Scene, SceneChangeFinish>
    {
        protected override async ETTask Run(Scene scene, SceneChangeFinish a)
        {
            if (a.UnitId<10000)
            {
                Log.Error("I am robot,so i need to stop"+a.UnitId);
                return;
            }
            
            scene.GetComponent<UIComponent>().CloseWindow(WindowID.Win_UILoading);
            await ETTask.CompletedTask;
        }
    }
}