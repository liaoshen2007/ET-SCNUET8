using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof (Unit))]
    [EnableMethod]
    public class BuffComponent: Entity, IAwake, IUpdate, ITransfer
    {
        public Dictionary<int, int> ScribeEventMap { get; } = new();

        public Dictionary<long, BuffUnit> BuffDict { get; } = new();

        public Dictionary<int, bool> EventMap { get; } = new();
    }
}