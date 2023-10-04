namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class EnterMapFinish_CreateMainUI : AEvent<Scene, EnterMapFinish>
    {
        protected override async ETTask Run(Scene scene, EnterMapFinish e)
        {
            await scene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.Win_Main);
        }
    }   
}