using System.Collections.Generic;

namespace ET
{
    public partial class TaskConfig
    {
        public List<CmdArg> GetConditionList { get; private set; }

        public List<CmdArg> CommitConditionList { get; private set; }
        
        public List<CmdArg> CommitCmdList { get; private set; }

        public override void EndInit()
        {
            GetConditionList = this.GetConditionListStr.ParseCmdArgs();
            CommitConditionList = this.CommitConditionListStr.ParseCmdArgs();
            CommitCmdList = this.CommitCmdListStr.ParseCmdArgs();
        }
    }
}