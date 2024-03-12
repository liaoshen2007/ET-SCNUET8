using System;

namespace ET.Client
{
    [FriendOf(typeof(Account))]
    [FriendOfAttribute(typeof(ET.Client.RoleInfoComponent))]
    public static class LoginHelper
    {
        /// <summary>
        /// 登录游戏
        /// </summary>
        /// <param name="root"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static async ETTask<int> Login(Scene root, string account, string password, long accountId)
        {
            var clientSenderComponent = root.GetComponent<ClientSenderComponent>();
            if (clientSenderComponent != null)
            {
                await clientSenderComponent.DisposeClientSender();
                await root.GetComponent<TimerComponent>().WaitFrameAsync();
            }

            clientSenderComponent = root.AddComponent<ClientSenderComponent>();
            (bool ok, long playerId) r = await clientSenderComponent.LoginAsync(account, password, accountId);
            if (!r.ok)
            {
                return (int)r.playerId;
            }

            root.GetComponent<PlayerComponent>().MyId = r.playerId;

            await EventSystem.Instance.PublishAsync(root, new LoginFinish());

            return ErrorCode.ERR_Success;
        }

        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <param name="root"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async ETTask<int> QueryAccount(Scene root, string account, string password)
        {
            string url = $"http://{ConstValue.RouterHttpHost}:{ConstValue.AccoutHttpPort}/account?Account={account}&Password={password}";
            string str = string.Empty;
            try
            {
                str = await HttpClientHelper.Get(url);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return ErrorCode.ERR_NetWorkError;
            }

            HttpAccount httpAcc = MongoHelper.FromJson<HttpAccount>(str);
            if (httpAcc.Error != ErrorCode.ERR_Success)
            {
                return httpAcc.Error;
            }

            var child = root.GetChild<Account>();
            if (child != null)
            {
                root.RemoveChild(child.Id);
            }

            var acc = root.AddChildWithId<Account>(httpAcc.Account.Id);
            acc.AccountName = account;
            acc.Password = password;
            acc.AccountType = (AccountType)httpAcc.Account.AccountType;
            acc.CreateTime = httpAcc.Account.CreateTime;

            return ErrorCode.ERR_Success;
        }

        /// <summary>
        /// 获取服务器列表
        /// </summary>
        /// <param name="root"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static async ETTask<int> GetServerInfos(Scene root, string account)
        {
            string url = $"http://{ConstValue.RouterHttpHost}:{ConstValue.AccoutHttpPort}/server_list?ServerType=1";
            string str = string.Empty;
            try
            {
                str = await HttpClientHelper.Get(url);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return ErrorCode.ERR_NetWorkError;
            }

            HttpServerList httpServer = MongoHelper.FromJson<HttpServerList>(str);
            root.GetComponent<ServerInfoComponent>().Clear();
            foreach (var serverInfoProto in httpServer.ServerList)
            {
                var serverInfo = root.GetComponent<ServerInfoComponent>().AddChildWithId<ServerInfo>(serverInfoProto.Id);
                serverInfo.FromMessage(serverInfoProto);
                root.GetComponent<ServerInfoComponent>().Add(serverInfo);
            }

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRoles(Scene root)
        {
            ClientSenderComponent clientSenderComponent = root.GetComponent<ClientSenderComponent>();
            if (clientSenderComponent == null)
            {
                clientSenderComponent = root.AddComponent<ClientSenderComponent>();
            }


            A2C_GetRoles a2CGetRoles = null;

            try
            {
                a2CGetRoles = (A2C_GetRoles)await clientSenderComponent.Call(new C2A_GetRoles()
                {
                    Account = root.GetComponent<AccountInfoComponent>().AccountId.ToString(),
                    Token = root.GetComponent<AccountInfoComponent>().Token,
                    ServerId = root.GetComponent<ServerInfoComponent>().CurrentServerId,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CGetRoles.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2CGetRoles.Error.ToString());
                return a2CGetRoles.Error;
            }


            root.GetComponent<RoleInfoComponent>().RoleInfos.Clear();
            foreach (var roleInfoProto in a2CGetRoles.RoleInfo)
            {
                RoleInfo roleInfo = root.GetComponent<RoleInfoComponent>().AddChild<RoleInfo>();
                roleInfo.FromMessage(roleInfoProto);
                root.GetComponent<RoleInfoComponent>().RoleInfos.Add(roleInfo);
            }

            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
    }
}