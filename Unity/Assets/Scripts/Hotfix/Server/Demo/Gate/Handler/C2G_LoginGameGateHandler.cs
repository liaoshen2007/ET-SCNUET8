using System.Collections.Generic;

namespace ET.Server
{
    /// <summary>
    /// 登录Gate
    /// </summary>
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_LoginGameGateHandler: MessageSessionHandler<C2G_LoginGameGate, G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response)
        {
            Scene root = session.Root();
            if (root.GetComponent<SessionLockComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                return;
            }
            
            string account = root.GetComponent<GateSessionKeyComponent>().Get(request.Key);
            if (account == null)
            {
                response.Error = ErrorCore.ERR_ConnectGateKeyError;
                response.Message = new List<string>() { "Gate key验证失败!" };
                session?.Disconnect().Coroutine();
                return;
            }

            session.RemoveComponent<SessionAcceptTimeoutComponent>();
            using (root.AddComponent<SessionLockComponent>())
            {
                using (await root.GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.LoginGameGate, request.RoleId.GetHashCode()))
                {
                    
                    //todo 通知登录中心服 记录本次登录的服务器Zone
                    // StartSceneConfig loginCenterConfig = StartSceneConfigCategory.Instance.LoginCenterConfig;
                    // L2G_AddLoginRecord l2ARoleLogin    = (L2G_AddLoginRecord) await MessageHelper.CallActor(loginCenterConfig.InstanceId, 
                    //     new G2L_AddLoginRecord() { AccountId = request.Account, ServerId = scene.Zone});
                    
                    PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();
                    Player player = playerComponent.GetByAccount(account);
                    if (player == null)
                    {
                        player = playerComponent.GetChild<Player>(request.RoleId);
                        if (player==null)
                        {
                            //player = playerComponent.AddChildWithId<Player, string>(request.Id, account);
                            //todo player = scene.GetComponent<PlayerComponent>().AddChildWithId<Player,long,long>(request.RoleId,request.Account,request.RoleId);
                            player = playerComponent.AddChildWithId<Player,string>(request.RoleId,account);
                        }
                        playerComponent.Add(player);
                        PlayerSessionComponent playerSessionComponent = player.AddComponent<PlayerSessionComponent>();
                        playerSessionComponent.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.GateSession);
                        await playerSessionComponent.AddLocation(LocationType.GateSession);

                        player.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
                        await player.AddLocation(LocationType.Player);

                        session.AddComponent<SessionPlayerComponent>().Player = player;
                        playerSessionComponent.Session = session;
                        
                        //bug 还是不太清楚这个到底有什么用~~
                        player.ClientSession = session;
                        //todo PlayerOfflineOutTimeComponent
                        
                    }
                    else
                    {
                        // 判断是否在战斗
                        PlayerRoomComponent playerRoomComponent = player.GetComponent<PlayerRoomComponent>();
                        if (playerRoomComponent != null && playerRoomComponent.RoomActorId != default)
                        {
                            CheckRoom(player, session).Coroutine();
                        }
                        else
                        {
                            session.AddComponent<SessionPlayerComponent>().Player = player;
                            PlayerSessionComponent playerSessionComponent = player.GetComponent<PlayerSessionComponent>();
                            playerSessionComponent.Session = session;
                        }
                    }

                    response.PlayerId = player.Id;
                }
            }


            await ETTask.CompletedTask;
        }
        
        private static async ETTask CheckRoom(Player player, Session session)
        {
            Fiber fiber = player.Fiber();
            await fiber.WaitFrameFinish();

            using Room2G_Reconnect room2GateReconnect = await fiber.Root.GetComponent<MessageSender>().Call(
                player.GetComponent<PlayerRoomComponent>().RoomActorId,
                new G2Room_Reconnect() { PlayerId = player.Id }) as Room2G_Reconnect;
            G2C_Reconnect g2CReconnect = new() { StartTime = room2GateReconnect.StartTime, Frame = room2GateReconnect.Frame };
            g2CReconnect.UnitInfos.AddRange(room2GateReconnect.UnitInfos);
            session.Send(g2CReconnect);

            session.AddComponent<SessionPlayerComponent>().Player = player;
            player.GetComponent<PlayerSessionComponent>().Session = session;
        }
    }
}

