using System.Collections.Generic;

namespace ET
{
    public partial class SystemMenuCategory
    {
        private Dictionary<int, List<SystemMenu>> classifyDict = new Dictionary<int, List<SystemMenu>>();

        public List<SystemMenu> GetList(int classify)
        {
            this.classifyDict.TryGetValue(classify, out List<SystemMenu> list);
            return list;
        }

        public override void EndInit()
        {
            foreach (var config in this.dict.Values)
            {
                var classify = config.Classify;
                if (!this.classifyDict.TryGetValue(classify, out var list))
                {
                    list = new List<SystemMenu>();
                    this.classifyDict.Add(classify, list);
                }

                list.Add(config);
            }
        }
    }

    public partial class SystemMenu
    {
        public List<CmdArg> ShowCmdList { get; private set; }

        public List<CmdArg> OpenCmdList { get; private set; }

        public List<CmdArg> CloseCmdList { get; private set; }

        public override void EndInit()
        {
            ShowCmdList = this.ShowCmdStr.ParseCmdArgs();
            OpenCmdList = this.OpenCmdStr.ParseCmdArgs();
            CloseCmdList = this.CloseCmdStr.ParseCmdArgs();
        }
    }
}