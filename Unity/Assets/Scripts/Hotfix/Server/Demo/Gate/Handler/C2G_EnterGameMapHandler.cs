using System;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_EnterGameMapHandler: MessageSessionHandler<C2G_EnterGameMap, G2C_EnterGameMap>
    {
        protected override async ETTask Run(Session session, C2G_EnterGameMap request, G2C_EnterGameMap response)
        {
            if (session.Root().GetComponent<SessionLockComponent>()!=null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                return;
            }
            
            Player player = session.GetComponent<SessionPlayerComponent>()?.Player;
            if (player==null)
            {
                response.Error = ErrorCode.ERR_NoPlayer;
                return;
            }
            

            
            try
            {
                long instanceId = session.InstanceId;
                using (session.Root().AddComponent<SessionLockComponent>())
                {
                    using (await session.Root().GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.EnterGameMap, request.RoleId.GetHashCode()))
                    {
                        if (instanceId!=session.InstanceId||player.IsDisposed)
                        {
                            response.Error = ErrorCode.ERR_SessionError;
                            return;
                        }
                        
                        //todo 这里应该要判断一下sessionstate和playerstate~~
                        
                        if (player.GetComponent<GateMapComponent>() != null)
                        {
                            Log.Error("player.GetComponent<GateMapComponent>()!= null");
                            return;
                        }

                        (bool isNewPlayer, Unit unit) = await UnitHelper.LoadUnit(player);
                        await UnitHelper.InitUnit(unit, isNewPlayer);
                        
                        //其实此刻playerId和unitId是一致的~~
                        response.MyId = unit.Id;
                        
                        // 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
                        var numeric = unit.GetComponent<NumericComponent>();
                        string mapindex ="Map"+ numeric.GetAsInt(NumericType.LocalMap);
                        StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(player.Zone(), mapindex);
                        Log.Error(mapindex);
                        TransferHelper.TransferAtFrameFinish(unit, startSceneConfig.ActorId, true).Coroutine();
                        EventSystem.Instance.Publish(player.Scene(), new EnterGame() { Player = player });
                    }
                }
                

            }
            catch (Exception e)
            {
                Log.Error($"角色进入游戏服出错 {player.Account} {request.RoleId} {e}");
                response.Error = ErrorCode.ERR_EnterGame;
                session.Disconnect().Coroutine();
            }
        }
    }  
}

