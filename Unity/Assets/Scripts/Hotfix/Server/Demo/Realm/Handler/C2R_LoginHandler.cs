namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    public class C2R_LoginHandler : MessageSessionHandler<C2R_Login, R2C_Login>
    {
        protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response)
        {
            if (session.Root().GetComponent<SessionLockComponent>()!=null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                session.Disconnect().Coroutine();
                return;
            }
            
            //bug 有可能机器人框架会出问题~~
            using (session.Root().AddComponent<SessionLockComponent>())
            {
                using (await session.Root().GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.LoginReam, request.Account.GetLongHashCode()))
                {
                    // 随机分配一个Gate
                    StartSceneConfig config = RealmGateAddressHelper.GetGate(session.Zone(), request.Account);
                    Log.Debug($"gate address: {config}");
                    // 向gate请求一个key,客户端可以拿着这个key连接gate
                    G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey) await session.Scene().GetComponent<MessageSender>().Call(
                        config.ActorId, new R2G_GetLoginKey() {Account = request.Account});

                    response.Address = config.InnerIPPort.ToString();
                    response.Key = g2RGetLoginKey.Key;
                    response.GateId = g2RGetLoginKey.GateId;
                    session.Disconnect().Coroutine();
                }
            }
            
        }
    }
}