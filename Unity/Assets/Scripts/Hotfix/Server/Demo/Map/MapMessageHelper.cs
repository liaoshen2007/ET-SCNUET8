﻿using System.Collections.Generic;
using System.IO;

namespace ET.Server
{
    public static partial class MapMessageHelper
    {
        public static void NoticeUnitAdd(Unit unit, Unit sendUnit)
        {
            M2C_CreateUnits createUnits = M2C_CreateUnits.Create();
            createUnits.Units.Add(UnitHelper.CreateUnitInfo(sendUnit));
            SendToClient(unit, createUnits);
        }
        
        public static void NoticeUnitRemove(Unit unit, Unit sendUnit)
        {
            M2C_RemoveUnits removeUnits = M2C_RemoveUnits.Create();
            removeUnits.Units.Add(sendUnit.Id);
            SendToClient(unit, removeUnits);
        }
        
        public static void Broadcast(Unit unit, IMessage message)
        {
            (message as MessageObject).IsFromPool = false;
            Dictionary<long, AOIEntity> dict = unit.GetBeSeePlayers();
            // 网络底层做了优化，同一个消息不会多次序列化
            MessageLocationSenderOneType oneTypeMessageLocationType = unit.Root().GetComponent<MessageLocationSenderComponent>().Get(LocationType.GateSession);
            foreach (AOIEntity u in dict.Values)
            {
                oneTypeMessageLocationType.Send(u.Unit.Id, message);
            }
        }
        
        /// <summary>
        /// 注意！！！！！！！这条函数使用的Message一定要new过来的，不然多线程会缓存然后客户端会收到错误的消息！！！！
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="message"></param>
        public static void SendToClient(this Unit unit, IMessage message)
        {
            unit.Root().GetComponent<MessageLocationSenderComponent>().Get(LocationType.GateSession).Send(unit.Id, message);
        }
        
        // public static void SendNumericToClient(this Unit unit, IMessage message)
        // {
        //     Log.Error("SendNumericToClient:"+message);
        //     unit.Root().GetComponent<MessageLocationSenderComponent>().Get(LocationType.GateSession).Send(unit.Id, message);
        // }
        
        /// <summary>
        /// 发送协议给Actor
        /// </summary>
        public static void Send(Scene root, ActorId actorId, IMessage message)
        {
            root.GetComponent<MessageSender>().Send(actorId, message);
        }
    }
}