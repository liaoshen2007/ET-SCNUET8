namespace ET.Server
{
    public struct EnterGame
    {
        public Player Player;
        public long RoleId;
    }

    public struct LeaveGame
    {
        public Player Player;
    }
}