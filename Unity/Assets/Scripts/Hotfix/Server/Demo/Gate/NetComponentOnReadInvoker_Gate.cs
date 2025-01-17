﻿using System;

namespace ET.Server
{
    [Invoke((long)SceneType.Gate)]
    public class NetComponentOnReadInvoker_Gate: AInvokeHandler<NetComponentOnRead>
    {
        public override void Handle(NetComponentOnRead args)
        {
            HandleAsync(args).Coroutine();
        }

        private async ETTask HandleAsync(NetComponentOnRead args)
        {
            Session session = args.Session;
            object message = args.Message;
            Scene root = args.Session.Root();
            //Log.Error("NetComponentOnReadInvoker_Gate"+args);
            // 根据消息接口判断是不是Actor消息，不同的接口做不同的处理,比如需要转发给Chat Scene，可以做一个IChatMessage接口
            switch (message)
            {
                case ISessionMessage:
                {
                    MessageSessionDispatcher.Instance.Handle(session, message);
                    break;
                }
                case FrameMessage frameMessage:
                {
                    Player player = session.GetComponent<SessionPlayerComponent>().Player;
                    ActorId roomActorId = player.GetComponent<PlayerRoomComponent>().RoomActorId;
                    frameMessage.PlayerId = player.Id;
                    root.GetComponent<MessageSender>().Send(roomActorId, frameMessage);
                    break;
                }
                case IRoomMessage actorRoom:
                {
                    Player player = session.GetComponent<SessionPlayerComponent>().Player;
                    ActorId roomActorId = player.GetComponent<PlayerRoomComponent>().RoomActorId;
                    actorRoom.PlayerId = player.Id;
                    root.GetComponent<MessageSender>().Send(roomActorId, actorRoom);
                    break;
                }
                case ILocationMessage actorLocationMessage:
                {
                    long unitId = session.GetComponent<SessionPlayerComponent>().Player.Id;
                    root.GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit).Send(unitId, actorLocationMessage);
                    break;
                }
                case ILocationRequest actorLocationRequest: // gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
                {
                    long unitId = session.GetComponent<SessionPlayerComponent>().Player.Id;
                    int rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
                    long instanceId = session.InstanceId;
                    IResponse iResponse = await root.GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit).Call(unitId, actorLocationRequest);
                    iResponse.RpcId = rpcId;
                    // session可能已经断开了，所以这里需要判断
                    if (session.InstanceId == instanceId)
                    {
                        session.Send(iResponse);
                    }
                    break;
                }
                case IAccountRequest accountRequest:
                {
                    var id=StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), nameof(SceneType.Account)).ActorId;
                    int rpcId = accountRequest.RpcId;//这里要保存客户端的RpcId
                    long instanceId = session.InstanceId;
                    //Log.Error("accountRequest is here!");
                    IResponse response = await root.GetComponent<MessageSender>().Call(id, accountRequest);
                    response.RpcId = rpcId;
                    if (session.InstanceId == instanceId)
                    {
                        session.Send(response);
                    }
                    
                    //todo 真的那么简单吗？
                    // MessageSessionDispatcher.Instance.Handle(session, message);
                    break;
                }
                case IAccountMessage accountMessage:
                {
                    var id = StartSceneConfigCategory.Instance.Account.ActorId;//GetBySceneName(session.Zone(), nameof(SceneType.Account)).ActorId;
                    root.GetComponent<MessageSender>().Send(id, accountMessage);
                    break;
                }
                case IRankRequest rankRequest:
                {
                    var id = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), nameof(SceneType.Rank)).ActorId;
                    int rpcId = rankRequest.RpcId; // 这里要保存客户端的rpcId
                    long instanceId = session.InstanceId;
                    IResponse response = await root.GetComponent<MessageSender>().Call(id, rankRequest);
                    response.RpcId = rpcId;
                    // session可能已经断开了，所以这里需要判断
                    if (session.InstanceId == instanceId)
                    {
                        session.Send(response);
                    }

                    break;
                }
                case IRankMessage rankMessage:
                {
                    var id = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), nameof(SceneType.Rank)).ActorId;
                    root.GetComponent<MessageSender>().Send(id, rankMessage);
                    break;
                }
                case IChatRequest chatRequest:
                {
                    var id = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), nameof(SceneType.Chat)).ActorId;
                    int rpcId = chatRequest.RpcId; // 这里要保存客户端的rpcId
                    long instanceId = session.InstanceId;
                    IResponse response = await root.GetComponent<MessageSender>().Call(id, chatRequest);
                    response.RpcId = rpcId;
                    // session可能已经断开了，所以这里需要判断
                    if (session.InstanceId == instanceId)
                    {
                        session.Send(response);
                    }

                    break;
                }
                case IChatMessage chatMessage:
                {
                    var id = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), nameof(SceneType.Chat)).ActorId;
                    root.GetComponent<MessageSender>().Send(id, chatMessage);
                    break;
                }
                case IRequest actorRequest:  // 分发IActorRequest消息，目前没有用到，需要的自己添加
                {
                    break;
                }
                case IMessage actorMessage:  // 分发IActorMessage消息，目前没有用到，需要的自己添加
                {
                    break;
                }
                default:
                {
                    throw new Exception($"not found handler: {message}");
                }
            }
        }
    }
}