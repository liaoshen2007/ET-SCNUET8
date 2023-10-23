using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof (Scene))]
    public class ServerInfoComponent: Entity, IAwake, IDestroy
    {
        public List<ServerInfo> ServerInfoList = new();

        public int CurrentServerId = 0;
    }
}