namespace ET.Server
{
    [MessageHandler(SceneType.Account)]
    [FriendOf(typeof (RoleInfo))]
    public class C2A_DeleteRoleHandler: MessageHandler<Scene,C2A_DeleteRole, A2C_DeleteRole>
    {
        protected override async ETTask Run(Scene root, C2A_DeleteRole request, A2C_DeleteRole response)
        {
            if (root.GetComponent<SessionLockComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                //session.Disconnect().Coroutine();
                return;
            }

            using (root.AddComponent<SessionLockComponent>())
            {
                using (await root.GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.DeleteRole, request.Account.HashCode()))
                {
                    DBComponent dbComponent = root.GetComponent<DBManagerComponent>().GetZoneDB(root.Zone());
                    var roleInfos = await dbComponent
                            .Query<RoleInfo>(d => d.Id == request.RoleInfoId && d.ServerId == request.ServerId);

                    if (roleInfos is not { Count: > 0 })
                    {
                        response.Error = ErrorCode.ERR_RoleNotExist;
                        return;
                    }

                    var roleInfo = roleInfos[0];
                    roleInfo.State = (int) RoleInfoState.Delete;
                    await dbComponent.Save(roleInfo);
                    response.DeletedRoleInfoId = roleInfo.Id;
                    roleInfo.Dispose();
                }
            }

            await ETTask.CompletedTask;
        }
    }
}