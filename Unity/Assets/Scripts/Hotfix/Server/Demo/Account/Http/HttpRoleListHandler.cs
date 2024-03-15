using System;
using System.Net;

namespace ET.Server
{
    //bug 都要记得using~~
    [HttpHandler(SceneType.Account, "/role_list")]
    public class HttpRoleListHandler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            //todo 要加root.GetComponent<SessionLockComponent>()
            HttpRoleList roleList = new(); 
            var account = context.Request.QueryString["Account"];
            if (account.IsNullOrEmpty())
            {
                HttpHelper.Response(context, string.Empty);
                return;
            }
            
            var serverId=Convert.ToInt32(context.Request.QueryString["ServerId"]);

            //Log.Error("Comehere!");
            DBComponent dbComponent = scene.GetComponent<DBManagerComponent>().GetZoneDB(serverId);
            var roles = await dbComponent.Query<RoleInfo>(d => d.Account == account
                    && d.ServerId == serverId && d.State == (int) RoleInfoState.Normal);//scene.GetComponent<RoleInfosComponent>().GetRoleList(account,Convert.ToInt32(serverId));
            
            foreach (var roleInfo in roles)
            {
                roleList.RoleList.Add(roleInfo.ToMessage());
                roleInfo?.Dispose();
            }
            
            HttpHelper.Response(context, roleList);
            await ETTask.CompletedTask;
        }
    }

    [HttpHandler(SceneType.Account, "/create_role")]
    public class HttpCreateRoleHanler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            HttpCreateRole resp = new();
            string account = context.Request.QueryString["Account"];
            string name=context.Request.QueryString["Name"];
            if (account.IsNullOrEmpty()||name.IsNullOrEmpty())
            {
                HttpHelper.Response(context, string.Empty);
                return;
            }
            
            int serverId=Convert.ToInt32(context.Request.QueryString["ServerId"]);
            DBComponent dbComponent = scene.GetComponent<DBManagerComponent>().GetZoneDB(serverId);
            var roleInfos = await dbComponent.Query<RoleInfo>(d => d.Name == name && d.ServerId == serverId);

            if (roleInfos is { Count: > 0 })
            {
                resp.Error = ErrorCode.ERR_RoleNameSame;
                return ;
            }
            RoleInfo newRoleInfo = scene.AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateId());
            newRoleInfo.RoleId = newRoleInfo.Id;//todo RoleId可以besonIgnore~~
            newRoleInfo.Name = name;
            newRoleInfo.State = (int) RoleInfoState.Normal;
            newRoleInfo.ServerId = serverId;
            newRoleInfo.Account = account;
            newRoleInfo.CreateTime = TimeInfo.Instance.ServerNow();
            newRoleInfo.LastLoginTime = 0;

            await dbComponent.Save(newRoleInfo);

            resp.RoleInfo = newRoleInfo.ToMessage();
            HttpHelper.Response(context, resp);
            newRoleInfo.Dispose();
        }
    }

    [HttpHandler(SceneType.Account, "/delete_role")]
    public class HttpDeleteRoleHanler: IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            using (HttpDeleteRole resp = new())
            {
                string account = context.Request.QueryString["Account"];

                if (account.IsNullOrEmpty())
                {
                    HttpHelper.Response(context, string.Empty);
                    return;
                }
                long roleId=Convert.ToInt64(context.Request.QueryString["RoleId"]);
                int serverId=Convert.ToInt32(context.Request.QueryString["ServerId"]);
            
                DBComponent dbComponent = scene.GetComponent<DBManagerComponent>().GetZoneDB(serverId);
                var roleInfos = await dbComponent
                        .Query<RoleInfo>(d => d.Id == roleId && d.ServerId == serverId);

                if (roleInfos.Count==0)
                {
                    resp.Error= ErrorCode.ERR_RoleNotExist;
                }
                RoleInfo roleInfo = roleInfos[0];
                roleInfo.State = (int) RoleInfoState.Delete;
                await dbComponent.Save(roleInfo);
                resp.DeletedRoleInfoId = roleInfo.Id;
                HttpHelper.Response(context, resp);
                roleInfo.Dispose();
            }

            
            
            
        }
    }

}