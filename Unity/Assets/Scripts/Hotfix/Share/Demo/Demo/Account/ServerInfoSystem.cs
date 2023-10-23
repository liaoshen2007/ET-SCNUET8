namespace ET
{
    public static class ServerInfoSystem
    {
        public static void FromMessage(this ServerInfo self, ServerInfoProto serverInfoProto)
        {
            self.Status = (ServerStatus) serverInfoProto.Status;
            self.ServerName = serverInfoProto.ServerName;
        }

        public static ServerInfoProto ToMessage(this ServerInfo self)
        {
            return new ServerInfoProto() { Id = (int) self.Id, ServerName = self.ServerName, Status = (int) self.Status };
        }
    }
}