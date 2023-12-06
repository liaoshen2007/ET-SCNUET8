using System;
using System.Net;

namespace ET.Server
{
    [HttpHandler(SceneType.Account, "/query_info")]
    public class HttpQueryInfoHandler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            string sceneName = context.Request.QueryString["scene"];
            if (sceneName.IsNullOrEmpty())
            {
                HttpHelper.Response(context, "scene type is null");
                return;
            }

            string componentName = context.Request.QueryString["component"];
            if (componentName.IsNullOrEmpty())
            {
                HttpHelper.Response(context, "component name is null");
                return;
            }

            var actorId = StartSceneConfigCategory.Instance.GetBySceneName(scene.Zone(), sceneName).ActorId;
            var rep = await scene.GetComponent<MessageSender>().Call(actorId, new ConmponentQueryRequest() { ComponentName = componentName });
            if (rep == null)
            {
                HttpHelper.Response(context, "component not found");
                return;
            }

            byte[] data = (rep as ComponentQueryResponse).Entity;
            HttpHelper.Response(context, MongoHelper.Deserialize<Entity>(data));
        }
    }
}