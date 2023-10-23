namespace ET.Server
{
    [MessageSessionHandler(SceneType.Account)]
    [FriendOf(typeof (RoleInfo))]
    public class C2A_CreateRoleHandler: MessageSessionHandler<C2A_CreateRole, A2C_CreateRole>
    {
        protected override async ETTask Run(Session session, C2A_CreateRole request, A2C_CreateRole response)
        {
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                session.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_InputInvaid;
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await session.Fiber().CoroutineLockComponent.Wait(CoroutineLockType.CreateRole, request.Account.HashCode()))
                {
                    var roleInfos = await session.DBComponent().Query<RoleInfo>(d => d.Name == request.Name && d.ServerId == request.ServerId);

                    if (roleInfos is { Count: > 0 })
                    {
                        response.Error = ErrorCode.ERR_RoleNameSame;
                        return;
                    }

                    RoleInfo newRoleInfo = session.AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateId());
                    newRoleInfo.Name = request.Name;
                    newRoleInfo.State = (int) RoleInfoState.Normal;
                    newRoleInfo.ServerId = request.ServerId;
                    newRoleInfo.Account = request.Account;
                    newRoleInfo.CreateTime = TimeInfo.Instance.ServerNow();
                    newRoleInfo.LastLoginTime = 0;

                    await session.DBComponent().Save(newRoleInfo);

                    response.RoleInfo = newRoleInfo.ToMessage();
                    newRoleInfo.Dispose();
                }
            }

            await ETTask.CompletedTask;
        }
    }
}