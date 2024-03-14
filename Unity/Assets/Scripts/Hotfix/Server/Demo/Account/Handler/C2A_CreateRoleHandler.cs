namespace ET.Server
{
    [MessageHandler(SceneType.Account)]
    [FriendOf(typeof (RoleInfo))]
    public class C2A_CreateRoleHandler: MessageHandler<Scene,C2A_CreateRole, A2C_CreateRole>
    {
        protected override async ETTask Run(Scene root, C2A_CreateRole request, A2C_CreateRole response)
        {
            if (root.GetComponent<SessionLockComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                //root.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_InputInvaid;
                return;
            }

            using (root.AddComponent<SessionLockComponent>())
            {
                using (await root.GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.CreateRole, request.Account.HashCode()))
                {
                    DBComponent dbComponent = root.GetComponent<DBManagerComponent>().GetZoneDB(root.Zone());
                    var roleInfos = await dbComponent.Query<RoleInfo>(d => d.Name == request.Name && d.ServerId == request.ServerId);

                    if (roleInfos is { Count: > 0 })
                    {
                        response.Error = ErrorCode.ERR_RoleNameSame;
                        return;
                    }

                    RoleInfo newRoleInfo = root.AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateId());
                    //newRoleInfo.RoleId = newRoleInfo.Id;//todo RoleId可以besonIgnore~~
                    newRoleInfo.Name = request.Name;
                    newRoleInfo.State = (int) RoleInfoState.Normal;
                    newRoleInfo.ServerId = request.ServerId;
                    newRoleInfo.Account = request.Account;
                    newRoleInfo.CreateTime = TimeInfo.Instance.ServerNow();
                    newRoleInfo.LastLoginTime = 0;

                    await dbComponent.Save(newRoleInfo);

                    response.RoleInfo = newRoleInfo.ToMessage();
                    newRoleInfo.Dispose();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}