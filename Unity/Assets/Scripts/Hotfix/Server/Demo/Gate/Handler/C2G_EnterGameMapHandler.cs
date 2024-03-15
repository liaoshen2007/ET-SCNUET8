using System;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_EnterGameMapHandler: MessageSessionHandler<C2G_EnterGameMap, G2C_EnterGameMap>
    {
        protected override async ETTask Run(Session session, C2G_EnterGameMap request, G2C_EnterGameMap response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            response.MyId = player.Id;

            
            try
            {
                if (player.GetComponent<GateMapComponent>() != null)
                {
                    return;
                }

                (bool isNewPlayer, Unit unit) = await UnitHelper.LoadUnitWithRoleId(player,request.RoleId);
                await UnitHelper.InitUnit(unit, isNewPlayer);

                // 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
                var numeric = unit.GetComponent<NumericComponent>();
                string mapindex ="Map"+ numeric.GetAsInt(NumericType.LocalMap);
                StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(player.Zone(), mapindex);
                TransferHelper.TransferAtFrameFinish(unit, startSceneConfig.ActorId, true).Coroutine();
                EventSystem.Instance.Publish(player.Scene(), new EnterGame() { Player = player });
            }
            catch (Exception e)
            {
                Log.Error($"角色进入游戏服出错 {player.Account} {player.Id} {e}");
                response.Error = ErrorCode.ERR_EnterGame;
                session.Disconnect().Coroutine();
            }
        }
    }  
}

