namespace ET.Server
{

    [FriendOf(typeof (RoleInfo))]
    [MessageHandler(SceneType.Account)]
    public class C2A_GetRolesHandler: MessageHandler<Scene,C2A_GetRoles, A2C_GetRoles>
    {
        protected override async ETTask Run(Scene unit, C2A_GetRoles request, A2C_GetRoles response)
        {
            Log.Error("Last come here!!!");

            await ETTask.CompletedTask;
        }
        
        // protected override async ETTask Run(Session session, C2A_GetRoles request, A2C_GetRoles response)
        // {
        //     Log.Error("C2A_GetRolesHandler");
        //     if (session.GetComponent<SessionLockingComponent>() != null)
        //     {
        //         response.Error = ErrorCode.ERR_RequestRepeatedly;
        //         session.Disconnect().Coroutine();
        //         return;
        //     }
        //
        //
        //     using (session.AddComponent<SessionLockingComponent>())
        //     {
        //         using (await session.Fiber().Root.GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.CreateRole, request.Account.HashCode()))
        //         {
        //             var roleInfos = await session.DBComponent()
        //                     .Query<RoleInfo>(d =>
        //                             d.Account == request.Account && d.ServerId == request.ServerId && d.State == (int) RoleInfoState.Normal);
        //
        //             if (roleInfos == null || roleInfos.Count == 0)
        //             {
        //                 return;
        //             }
        //
        //             foreach (var roleInfo in roleInfos)
        //             {
        //                 response.RoleInfo.Add(roleInfo.ToMessage());
        //                 roleInfo?.Dispose();
        //             }
        //
        //             roleInfos.Clear();
        //         }
        //     }
        // }
    }
}