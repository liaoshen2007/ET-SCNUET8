using System;

namespace ET.Server
{
    [Invoke((long)SceneType.Account)]
    public class NetComponentOnReadInvoker_Account: AInvokeHandler<NetComponentOnRead>
    {
        public override void Handle(NetComponentOnRead args)
        {
            HandleAsync(args).Coroutine();
        }
        
        private async ETTask HandleAsync(NetComponentOnRead args)
        {
            Log.Error("NetComponentOnReadInvoker_Account?");
            Session session = args.Session;
            object message = args.Message;
            Scene root = args.Session.Root();
            // 根据消息接口判断是不是Actor消息，不同的接口做不同的处理,比如需要转发给Chat Scene，可以做一个IChatMessage接口
            switch (message)
            {
                case ISessionMessage:
                {
                    MessageSessionDispatcher.Instance.Handle(session, message);
                    break;
                }
                default:
                {
                    throw new Exception($"not found handler: {message}");
                }
            }

            await ETTask.CompletedTask;
        }
    }
}

