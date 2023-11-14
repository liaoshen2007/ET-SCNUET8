using System.Collections.Generic;

namespace ET
{
    public partial class TaskConfig
    {
        public List<CmdArg> GetCmdList { get; private set; }

        public List<CmdArg> CommitCmdList { get; private set; }

        public override void EndInit()
        {
            GetCmdList = this.GetConditionListStr.ParseCmdArgs();
            CommitCmdList = this.CommitCmdListStr.ParseCmdArgs();
        }
    }
}