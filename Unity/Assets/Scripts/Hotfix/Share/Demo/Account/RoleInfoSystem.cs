namespace ET
{
    public static class RoleInfoSystem
    {
        public static void FromMessage(this RoleInfo self,RoleInfoProto roleInfoProto)
        {
            self.RoleId = roleInfoProto.Id;
            self.Name = roleInfoProto.Name;
            self.State = roleInfoProto.State;
            self.Account = roleInfoProto.Account;
            self.CreateTime = roleInfoProto.CreateTime;
            self.ServerId = roleInfoProto.ServerId;
            self.LastLoginTime = roleInfoProto.LastLoginTime;

        }

        public static RoleInfoProto ToMessage(this RoleInfo self)
        {
            return new RoleInfoProto()
            {
                Id=self.Id,
                Name = self.Name,
                State = self.State,
                Account = self.Account,
                CreateTime = self.CreateTime,
                ServerId = self.ServerId,
                LastLoginTime = self.LastLoginTime
                
            };

        }
        
        
    }
}