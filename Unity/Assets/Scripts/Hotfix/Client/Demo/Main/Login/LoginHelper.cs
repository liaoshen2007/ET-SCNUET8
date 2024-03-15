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
            root.RemoveComponent<ClientSenderComponent>();
            ClientSenderComponent clientSenderComponent = root.AddComponent<ClientSenderComponent>();
            (bool ok, long playerId) r = await clientSenderComponent.LoginAsync(account, password, accountId);
            if (!r.ok)
            {
                return (int)r.playerId;
            }

            root.GetComponent<PlayerComponent>().MyId = r.playerId;

            await EventSystem.Instance.PublishAsync(root, new LoginFinish());

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> LoginGameGate(Scene root, string account, string password, long accountId,long roleId)
        {
            root.RemoveComponent<ClientSenderComponent>();
            ClientSenderComponent clientSenderComponent = root.AddComponent<ClientSenderComponent>();
            (bool ok, long playerId) r = await clientSenderComponent.LoginGameAsync(account, password, accountId,roleId);
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
            Log.Debug("AccountId:"+httpAcc.Account.Id);
            root.GetComponent<AccountInfoComponent>().AccountId=httpAcc.Account.Id;
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

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static async ETTask<int> GetRoleInfos(Scene root)
        {
            string account = root.GetComponent<AccountInfoComponent>().AccountId.ToString();
            int serverId = root.GetComponent<ServerInfoComponent>().CurrentServerId;
            string url = $"http://{ConstValue.RouterHttpHost}:{ConstValue.AccoutHttpPort}/role_list?Account={account}&ServerId={serverId}";
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
            root.GetComponent<RoleInfoComponent>().RoleInfos.Clear();
            HttpRoleList httproles = MongoHelper.FromJson<HttpRoleList>(str);
            foreach (var roleInfoProto in httproles.RoleList)
            {
                RoleInfo roleInfo = root.GetComponent<RoleInfoComponent>().AddChild<RoleInfo>();
                roleInfo.FromMessage(roleInfoProto);
                root.GetComponent<RoleInfoComponent>().RoleInfos.Add(roleInfo);
            }

            //await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;

        }

        public static async ETTask<int> CreateHttpRoles(Scene root,string name)
        {
            string account = root.GetComponent<AccountInfoComponent>().AccountId.ToString();
            int serverId = root.GetComponent<ServerInfoComponent>().CurrentServerId;
            string url = $"http://{ConstValue.RouterHttpHost}:{ConstValue.AccoutHttpPort}/create_role?Account={account}&ServerId={serverId}&Name={name}";
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
            
            HttpCreateRole httproles = MongoHelper.FromJson<HttpCreateRole>(str);
            if (httproles.Error!=ErrorCode.ERR_Success)
            {
                Log.Error(httproles.Error.ToString());
                return httproles.Error;
            }

            RoleInfo newRoleInfo = root.GetComponent<RoleInfoComponent>().AddChild<RoleInfo>();
            newRoleInfo.FromMessage(httproles.RoleInfo);
            
            root.GetComponent<RoleInfoComponent>().RoleInfos.Add(newRoleInfo);
            
            return ErrorCode.ERR_Success;
            
        }

        public static async ETTask<int> DeleteHttpRole(Scene root)
        {
            string account = root.GetComponent<AccountInfoComponent>().AccountId.ToString();
            int serverId = root.GetComponent<ServerInfoComponent>().CurrentServerId;
            long roleId = root.GetComponent<RoleInfoComponent>().CurrentRoleId;
            string url = $"http://{ConstValue.RouterHttpHost}:{ConstValue.AccoutHttpPort}/delete_role?Account={account}&ServerId={serverId}&RoleId={roleId}";
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
            
            HttpDeleteRole httpDeleteRole = MongoHelper.FromJson<HttpDeleteRole>(str);
            if (httpDeleteRole.Error!=ErrorCode.ERR_Success)
            {
                Log.Error(httpDeleteRole.Error.ToString());
                return httpDeleteRole.Error;
            }
            if (httpDeleteRole.Error!=ErrorCode.ERR_Success)
            {
                Log.Error(httpDeleteRole.Error.ToString());
                return httpDeleteRole.Error;
            }
            
            int deleteIndex=root.GetComponent<RoleInfoComponent>().RoleInfos.FindIndex((info) => info.RoleId == httpDeleteRole.DeletedRoleInfoId);
            root.GetComponent<RoleInfoComponent>().RoleInfos.RemoveAt(deleteIndex);
            
            await ETTask.CompletedTask;
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

            //await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
        
        public static async ETTask<int> CreatRole(Scene root,string name)
        {
            ClientSenderComponent clientSenderComponent = root.GetComponent<ClientSenderComponent>();
            if (clientSenderComponent == null)
            {
                clientSenderComponent = root.AddComponent<ClientSenderComponent>();
            }

            A2C_CreateRole a2CCreatRole = null;
            try
            {
                //todo ServerId后续要做成选择了服务器列表后的ServerId
                a2CCreatRole = (A2C_CreateRole) await clientSenderComponent.Call(new C2A_CreateRole()
                {
                    Account = root.GetComponent<AccountInfoComponent>().AccountId.ToString(),
                    Token = root.GetComponent<AccountInfoComponent>().Token,
                    Name = name,
                    ServerId = root.GetComponent<ServerInfoComponent>().CurrentServerId,
                });

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CCreatRole.Error!=ErrorCode.ERR_Success)
            {
                Log.Error(a2CCreatRole.Error.ToString());
                return a2CCreatRole.Error;
            }

            RoleInfo newRoleInfo = root.GetComponent<RoleInfoComponent>().AddChild<RoleInfo>();
            newRoleInfo.FromMessage(a2CCreatRole.RoleInfo);
            
            root.GetComponent<RoleInfoComponent>().RoleInfos.Add(newRoleInfo);
            

            return ErrorCode.ERR_Success;

        }

        public static async ETTask<int> DeleteRole(Scene root)
        {
            ClientSenderComponent clientSenderComponent = root.GetComponent<ClientSenderComponent>();
            if (clientSenderComponent == null)
            {
                clientSenderComponent = root.AddComponent<ClientSenderComponent>();
            }
            
            A2C_DeleteRole a2CDeleteRole = null;
            try
            {
                a2CDeleteRole = (A2C_DeleteRole)await clientSenderComponent.Call(new C2A_DeleteRole()
                {
                    Account = root.GetComponent<AccountInfoComponent>().AccountId.ToString(),
                    Token = root.GetComponent<AccountInfoComponent>().Token,
                    RoleInfoId = root.GetComponent<RoleInfoComponent>().CurrentRoleId,
                    ServerId = root.GetComponent<ServerInfoComponent>().CurrentServerId
                });

            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                return a2CDeleteRole.Error;
            }

            if (a2CDeleteRole.Error!=ErrorCode.ERR_Success)
            {
                Log.Error(a2CDeleteRole.Error.ToString());
                return a2CDeleteRole.Error;
            }
            
            int deleteIndex=root.GetComponent<RoleInfoComponent>().RoleInfos.FindIndex((info) => info.RoleId == a2CDeleteRole.DeletedRoleInfoId);
            root.GetComponent<RoleInfoComponent>().RoleInfos.RemoveAt(deleteIndex);
            
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
        
    }
}