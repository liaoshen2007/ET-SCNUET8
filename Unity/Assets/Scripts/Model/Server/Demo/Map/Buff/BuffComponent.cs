using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof (Unit))]
    [EnableMethod]
    public class BuffComponent: Entity, IAwake, IUpdate, ITransfer
    {
        public Dictionary<int, int> scribeEventMap = new();

        public Dictionary<long, BuffUnit> buffDict = new();

        public Dictionary<int, bool> eventMap = new();
    }
}