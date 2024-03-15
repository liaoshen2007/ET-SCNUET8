﻿using System;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_EnterMapHandler: MessageSessionHandler<C2G_EnterMap, G2C_EnterMap>
    {
        protected override async ETTask Run(Session session, C2G_EnterMap request, G2C_EnterMap response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            response.MyId = player.Id;

            try
            {
                if (player.GetComponent<GateMapComponent>() != null)
                {
                    return;
                }

                (bool isNewPlayer, Unit unit) = await UnitHelper.LoadUnit(player);
                await UnitHelper.InitUnit(unit, isNewPlayer);

                // 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
                StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(player.Zone(), "Map1");
                TransferHelper.TransferAtFrameFinish(unit, startSceneConfig.ActorId, true).Coroutine();
                //EventSystem.Instance.Publish(player.Scene(), new EnterGame() { Player = player ,RoleId = 0});
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