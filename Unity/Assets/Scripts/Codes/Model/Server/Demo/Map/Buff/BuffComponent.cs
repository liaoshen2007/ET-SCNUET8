using System;
using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof (Unit))]
    [EnableMethod]
    public class BuffComponent: Entity, IAwake, IUpdate, ITransfer
    {
        static BuffComponent()
        {
            var list = CodeTypes.Instance.GetTypes(typeof (BuffAttribute));
            foreach (var effect in list)
            {
                var attrs = effect.GetCustomAttributes(typeof (BuffAttribute), true);
                if (attrs.Length == 0)
                {
                    continue;
                }

                if (attrs[0] is BuffAttribute attr)
                {
                    BuffEffectDict.Add(attr.Cmd, effect);
                }
            }
        }

        public Dictionary<int, int> ScribeEventMap { get; } = new();

        public Dictionary<long, Buff> BuffDict { get; } = new();

        public Dictionary<int, bool> EventMap { get; } = new();

        [StaticField]
        public static Dictionary<string, Type> BuffEffectDict = new();
    }
}