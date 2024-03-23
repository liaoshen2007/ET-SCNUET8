namespace ET.Server
{
    [Event(SceneType.Map)]
    public class NumericChangeEvent_NoticeToClient:AEvent<Scene,NumbericChange>
    {
        protected override async ETTask Run(Scene scene, NumbericChange args)
        {
            args.Unit.GetComponent<NumericNoticeComponent>()?.NoticeImmediately(args);
            await ETTask.CompletedTask;
        }
    }
}

