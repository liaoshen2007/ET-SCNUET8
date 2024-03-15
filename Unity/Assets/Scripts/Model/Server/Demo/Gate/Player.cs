﻿namespace ET.Server
{
    [ChildOf(typeof(PlayerComponent))]
    public sealed class Player : Entity, IAwake<string>
    {
        public string Account { get; set; }
        
        public long ChatUnitId {get; set;}
        
        public Session ClientSession { get; set; }
    }
}