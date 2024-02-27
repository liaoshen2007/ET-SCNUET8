using System;
using System.Collections.Generic;
using System.Net;

namespace ET.Server
{
    [HttpHandler(SceneType.Account, "/save")]
    public class HttpSaveDataRequestHandler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            var set = new HashSet<string>() { "Cache", "Chat", "Map", "Rank" };
            foreach (StartSceneConfig config in StartSceneConfigCategory.Instance.GetAll().Values)
            {
                if (set.Contains(config.Name))
                {
                    IResponse resp = null;
                    try
                    {
                        resp = await scene.GetComponent<MessageSender>().Call(config.ActorId, W2Other_SaveDataRequest.Create());
                    }
                    catch (Exception)
                    {
                        HttpHelper.Response(context, $"保存数据错误: {resp.Error}, {config.Name} - {config.Id}");
                        return;
                    }
                }
            }

            HttpHelper.Response(context, "Success");
            await ETTask.CompletedTask;
        }
    }
}