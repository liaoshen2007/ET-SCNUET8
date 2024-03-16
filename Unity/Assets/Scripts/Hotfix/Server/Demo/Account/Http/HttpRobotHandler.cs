using System;
using System.Net;

namespace ET.Server
{
    [HttpHandler(SceneType.Account, "/robot")]
    public class HttpRobotHandler: IHttpHandler
    {
        public async ETTask Handle(Scene root, HttpListenerContext context)
        {
            HttpRobot resp = new();
            if (root.GetComponent<SessionLockComponent>() != null)
            {
                resp.Error = ErrorCode.ERR_RequestRepeatedly;
                HttpHelper.Response(context, string.Empty);
                //root.Disconnect().Coroutine();
                return;
            }
            var account = context.Request.QueryString["Account"];
            if (account.IsNullOrEmpty())
            {
                resp.Error = ErrorCode.ERR_InputInvaid;
                HttpHelper.Response(context, string.Empty);
                return;
            }
            
            var num=Convert.ToInt32(context.Request.QueryString["Num"]);
            Log.Error("RobotName:"+num);
            RobotManagerComponent robotManagerComponent =
                    root.GetComponent<RobotManagerComponent>() ?? root.AddComponent<RobotManagerComponent>();
            // 创建机器人
            TimerComponent timerComponent = root.GetComponent<TimerComponent>();
            for (int i = 0; i < num; ++i)
            {
                await robotManagerComponent.NewRobot($"Robot_{i}");
                Log.Console($"create robot {i}");
                await timerComponent.WaitAsync(2000);
            }
            HttpHelper.Response(context, resp);
            await ETTask.CompletedTask;
        }
    }
}

