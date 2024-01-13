using System;

namespace ET.Client
{
    [EntitySystemOf(typeof (ActionComponent))]
    [FriendOf(typeof (ActionComponent))]
    [FriendOf(typeof (ActionUnit))]
    public static partial class ActionComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ActionComponent self)
        {
        }

        [EntitySystem]
        private static void Update(this ActionComponent self)
        {
            if (self.curAction != null)
            {
                try
                {
                    ActionUnit action = self.curAction;
                    action.Update();

                    if (action.state == ActionState.Finish)
                    {
                        self.curAction = null;
                        self.pushActions.Remove(action);
                        if (self.pushActions.Count > 0)
                        {
                            self.curAction = self.pushActions[0];
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"push action 执行报错 {e}");
                }
            }

            if (self.playActions.Count <= 0)
            {
                return;
            }

            using var list = ListComponent<long>.Create();
            foreach (ActionUnit action in self.playActions)
            {
                try
                {
                    action.Update();
                    if (action.state == ActionState.Finish)
                    {
                        list.Add(action.InstanceId);
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"play action 执行报错 {action.actionName} {e}");
                }
            }

            foreach (long l in list)
            {
                ActionUnit c = self.GetChild<ActionUnit>(l);
                self.playActions.Remove(c);
                self.RemoveChild(l);
            }
        }

        [EntitySystem]
        private static void Destroy(this ActionComponent self)
        {
            self.StopAllAction();
        }

        public static long PlayAction(this ActionComponent self, string name)
        {
            ActionUnit action = self.AddChild<ActionUnit, string>(name);
            self.playActions.Add(action);

            return action.InstanceId;
        }

        public static long PushAction(this ActionComponent self, string name)
        {
            ActionUnit action = self.AddChild<ActionUnit, string>(name);
            self.pushActions.Add(action);
            self.pushActions.Sort((a, b) => a.config.Priority.CompareTo(b.config.Priority));
            if (self.curAction == null)
            {
                self.curAction = self.pushActions[0];
            }

            return action.InstanceId;
        }

        public static void StopAllAction(this ActionComponent self)
        {
            if (self.curAction != null)
            {
                ActionUnit action = self.curAction;
                action.Finish();
            }

            self.curAction = null;
            self.pushActions.Clear();

            foreach (ActionUnit action in self.playActions)
            {
                action.Finish();
            }

            self.playActions.Clear();
        }
    }
}