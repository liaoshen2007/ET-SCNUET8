using System.Net;

namespace ET.Server
{
    [HttpHandler(SceneType.Gate, "/object_list")]
    public class HttpUnitListHandler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            var playerCom = scene.GetComponent<PlayerComponent>();
            HttpHelper.Response(context, playerCom.GetAll());
            await ETTask.CompletedTask;
        }
    }
}