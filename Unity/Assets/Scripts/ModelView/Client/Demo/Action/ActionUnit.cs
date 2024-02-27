namespace ET.Client
{
    public enum ActionState
    {
        Ready,

        /// <summary>
        /// 行为运行中
        /// </summary>
        Run,

        /// <summary>
        /// 行为已完成
        /// </summary>
        Complete,

        /// <summary>
        /// 行为已结束
        /// </summary>
        Finish
    }

    [ChildOf(typeof (ActionComponent))]
    public class ActionUnit: Entity, IAwake<string>, IDestroy
    {
        public string actionName;

        public AActionSubConfig config;

        public AAction action;

        public ActionState state;
        public float duration;
        public float startTime;
    }
}