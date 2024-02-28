namespace ET.Client
{
    [Event(SceneType.Client)]
    public class UINetErrorEventHandler: AEvent<Scene, NetError>
    {
        protected override async ETTask Run(Scene scene, NetError e)
        {
            var errCfg = ErrorCfgCategory.Instance.Get(e.Error);
            var lan = LanguageCategory.Instance.Get(errCfg.Desc);
            UIHelper.PopMsg(scene, lan.Msg);
            await ETTask.CompletedTask;
        }
    }
}