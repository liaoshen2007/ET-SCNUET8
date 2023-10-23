namespace ET.Server
{
    [MessageSessionHandler(SceneType.Account)]
    [FriendOf(typeof (RoleInfo))]
    public class C2A_DeleteRoleHandler: MessageSessionHandler<C2A_DeleteRole, A2C_DeleteRole>
    {
        protected override async ETTask Run(Session session, C2A_DeleteRole request, A2C_DeleteRole response)
        {
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                session.Disconnect().Coroutine();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await session.Fiber().CoroutineLockComponent.Wait(CoroutineLockType.CreateRole, request.Account.HashCode()))
                {
                    var roleInfos = await session.DBComponent(request.ServerId)
                            .Query<RoleInfo>(d => d.Id == request.RoleInfoId && d.ServerId == request.ServerId);

                    if (roleInfos is not { Count: > 0 })
                    {
                        response.Error = ErrorCode.ERR_RoleNotExist;
                        return;
                    }

                    var roleInfo = roleInfos[0];
                    roleInfo.State = (int) RoleInfoState.Delete;
                    await session.DBComponent(request.ServerId).Save(roleInfo);
                    response.DeletedRoleInfoId = roleInfo.Id;
                    roleInfo.Dispose();
                }
            }

            await ETTask.CompletedTask;
        }
    }
}