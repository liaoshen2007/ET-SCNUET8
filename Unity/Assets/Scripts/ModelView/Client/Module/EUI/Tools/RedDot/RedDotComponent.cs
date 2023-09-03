using System.Collections.Generic;

namespace ET.Client
{
    [EntitySystemOf(typeof (RedDotComponent))]
    [FriendOf(typeof (RedDotComponent))]
    public static partial class RedDotComponentSystem
    {
        [EntitySystem]
        private static void Awake(this RedDotComponent self)
        {

        }

        [EntitySystem]
        private static void Destroy(this RedDotComponent self)
        {
            foreach (var List in self.RedDotNodeParentsDict.Values)
            {
                List.Dispose();
            }

            self.RedDotNodeParentsDict.Clear();
            self.ToParentDict.Clear();
            self.RedDotNodeRetainCount.Clear();
            self.RedDotMonoViewDict.Clear();
            self.RedDotNodeNeedShowSet.Clear();
            self.RetainViewCount.Clear();
        }
    }

    [ComponentOf(typeof(Scene))]
    public class RedDotComponent: Entity, IAwake, IDestroy
    {
        public Dictionary<string, ListComponent<string>> RedDotNodeParentsDict = new Dictionary<string, ListComponent<string>>();

        public HashSet<string> RedDotNodeNeedShowSet = new HashSet<string>();

        public Dictionary<string, int> RetainViewCount = new Dictionary<string, int>();

        public Dictionary<string, string> ToParentDict = new Dictionary<string, string>();

        public Dictionary<string, int> RedDotNodeRetainCount = new Dictionary<string, int>();

        public Dictionary<string, RedDotMonoView> RedDotMonoViewDict = new Dictionary<string, RedDotMonoView>();
    }
}