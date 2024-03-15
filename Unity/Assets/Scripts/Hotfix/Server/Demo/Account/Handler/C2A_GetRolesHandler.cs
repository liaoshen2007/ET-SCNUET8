namespace ET.Server
{

    [FriendOf(typeof (RoleInfo))]
    [MessageHandler(SceneType.Account)]
    public class C2A_GetRolesHandler: MessageHandler<Scene,C2A_GetRoles, A2C_GetRoles>
    {
        protected override async ETTask Run(Scene root, C2A_GetRoles request, A2C_GetRoles response)
        {
            //Log.Error("Last come here!!!");
            if (root.GetComponent<SessionLockComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                //root.Disconnect().Coroutine();
                return;
            }

            using (root.AddComponent<SessionLockComponent>())
            {
                using (await root.GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.GetRoles,request.Account.GetLongHashCode()))
                {
                    DBComponent dbComponent = root.GetComponent<DBManagerComponent>().GetZoneDB(root.Zone());
                    var roleInfos = await dbComponent.Query<RoleInfo>(d => d.Account == request.Account
                            && d.ServerId == request.ServerId && d.State == (int) RoleInfoState.Normal);
                
                    if (roleInfos == null || roleInfos.Count == 0)
                    {
                        return;
                    }
                
                    foreach (var roleInfo in roleInfos)
                    {
                        response.RoleInfo.Add(roleInfo.ToMessage());
                        roleInfo?.Dispose();
                    }
                
                    roleInfos.Clear();
                    
                }
            }
            

            await ETTask.CompletedTask;
        }
        
 
    }
}