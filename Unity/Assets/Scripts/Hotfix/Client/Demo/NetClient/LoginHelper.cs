using System;

namespace ET.Client
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene root, string account, string password)
        {
            root.RemoveComponent<ClientSenderCompnent>();
            ClientSenderCompnent clientSenderCompnent = root.AddComponent<ClientSenderCompnent>();

            long playerId = await clientSenderCompnent.LoginAsync(account, password);

            root.GetComponent<PlayerComponent>().MyId = playerId;

            await EventSystem.Instance.PublishAsync(root, new LoginFinish());

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetServerInfos(Scene root, string account)
        {
            string url = $"http://{ConstValue.RouterHttpHost}:{ConstValue.AccoutHttpPort}/server_list?ServerType=1";
            string str = await HttpClientHelper.Get(url);
            HttpServerList httpServer = MongoHelper.FromJson<HttpServerList>(str);
            foreach (var serverInfoProto in httpServer.ServerList)
            {
                var serverInfo = root.GetComponent<ServerInfoComponent>().AddChildWithId<ServerInfo>(serverInfoProto.Id);
                serverInfo.FromMessage(serverInfoProto);
                root.GetComponent<ServerInfoComponent>().Add(serverInfo);
            }

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRoles(Scene zoneScene)
        {
            // A2C_GetRoles a2CGetRoles = null;
            //
            // try
            // {
            //     a2CGetRoles = (A2C_GetRoles) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRoles()
            //     {
            //         AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
            //         Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
            //         ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurrentServerId,
            //     });
            // }
            // catch (Exception e)
            // {
            //     Log.Error(e.ToString());
            //     return ErrorCode.ERR_NetWorkError;
            // }
            //
            // if (a2CGetRoles.Error != ErrorCode.ERR_Success)
            // {
            //     Log.Error(a2CGetRoles.Error.ToString());
            //     return a2CGetRoles.Error;
            // }
            //
            //
            // zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Clear();
            // foreach (var roleInfoProto in a2CGetRoles.RoleInfo)
            // {
            //     RoleInfo roleInfo = zoneScene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
            //     roleInfo.FromMessage(roleInfoProto);
            //     zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Add(roleInfo);
            // }

            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
    }
}