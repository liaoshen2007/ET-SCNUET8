using System;
using System.Collections.Generic;

namespace ET.Client
{
    [Code]
    public class ActionSingleton: Singleton<ActionSingleton>, ISingletonAwake
    {
        private Dictionary<string, Type> actionTypeDic;

        public void Awake()
        {
            actionTypeDic = new Dictionary<string, Type>();
            foreach (var v in CodeTypes.Instance.GetTypes(typeof (ActionAttribute)))
            {
                var attr = v.GetCustomAttributes(typeof (ActionAttribute), false)[0] as ActionAttribute;
                actionTypeDic.Add(attr.ActionName, v);
            }
        }

        public AAction GetAction(string name)
        {
            if (!actionTypeDic.TryGetValue(name, out var t))
            {
                Thrower.Throw($"Action {name} not found");
            }

            return Activator.CreateInstance(t) as AAction;
        }
    }
}