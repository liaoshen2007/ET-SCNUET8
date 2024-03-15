﻿using System.Net;
using System.Net.Sockets;

namespace ET.Client
{
    [MessageHandler(SceneType.NetClient)]
    public class Main2NetClient_LoginGameHandler: MessageHandler<Scene, Main2NetClient_LoginGame, NetClient2Main_LoginGame>
    {
        protected override async ETTask Run(Scene root, Main2NetClient_LoginGame request, NetClient2Main_LoginGame response)
        {
           string account = request.Account;
            string password = request.Password;
            root.RemoveComponent<RouterAddressComponent>();
            RouterAddressComponent routerAddressComponent =
                    root.AddComponent<RouterAddressComponent, string, int>(ConstValue.RouterHttpHost, ConstValue.RouterHttpPort);
            await routerAddressComponent.Init();
            root.AddComponent<NetComponent, AddressFamily, NetworkProtocol>(routerAddressComponent.RouterManagerIPAddress.AddressFamily, NetworkProtocol.UDP);
            root.GetComponent<FiberParentComponent>().ParentFiberId = request.OwnerFiberId;

            NetComponent netComponent = root.GetComponent<NetComponent>();
            IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);
            Log.Debug("realmAddress:"+realmAddress);
            R2C_Login r2CLogin;
            using (Session session = await netComponent.CreateRouterSession(realmAddress, account, password))
            {
                r2CLogin = (R2C_Login)await session.Call(new C2R_Login() { Account = account, Password = password });
            }

            if (r2CLogin.Error != ErrorCode.ERR_Success)
            {
                response.Error = r2CLogin.Error;
                response.Message = r2CLogin.Message;
                return;
            }

            // 创建一个gate Session,并且保存到SessionComponent中
            Session gateSession = await netComponent.CreateRouterSession(NetworkHelper.ToIPEndPoint(r2CLogin.Address), account, password);
            gateSession.AddComponent<ClientSessionErrorComponent>();
            root.AddComponent<SessionComponent>().Session = gateSession;
            G2C_LoginGameGate g2CLoginGate = (G2C_LoginGameGate)await gateSession.Call(new C2G_LoginGameGate() { Key = r2CLogin.Key, GateId = r2CLogin.GateId, Id = request.Id ,RoleId = request.RoleId});
            
            if (g2CLoginGate.Error != ErrorCode.ERR_Success)
            {
                response.Error = g2CLoginGate.Error;
                response.Message = g2CLoginGate.Message;
                return;
            }
            
            response.PlayerId = g2CLoginGate.PlayerId;
            Log.Debug("登陆gate成功!");
        }
    } 
}

