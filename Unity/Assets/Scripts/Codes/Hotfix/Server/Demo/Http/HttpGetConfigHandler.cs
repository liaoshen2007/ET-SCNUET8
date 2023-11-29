using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using MongoDB.Bson;

namespace ET.Server
{
    [HttpHandler(SceneType.Map, "/favicon.ico")]
    public class DefaultHandler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            await ETTask.CompletedTask;
        }
    }

    [HttpHandler(SceneType.Map, "/get_config")]
    public class HttpGetConfigHandler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            var xx = new { context.Request.QueryString.AllKeys, StartSceneConfigCategory.Instance.GetAll().Values };
            HttpHelper.Response(context, xx);
            await ETTask.CompletedTask;
        }
    }
}