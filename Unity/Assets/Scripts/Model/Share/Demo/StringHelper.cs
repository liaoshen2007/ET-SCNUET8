using System;
using System.Collections.Generic;

namespace ET
{
    public struct CmdArg
    {
        public string Cmd { get; set; }
    
        public List<long> Args { get; set; }
    }
    
    public struct ItemArg
    {
        public int Id;
    
        public long Count;
    }
    
    public static class StringParseHelper
    {
        public static List<CmdArg> ParseCmdArgs(this string cmdStr)
        {
            List<CmdArg> cmdArgs = new List<CmdArg>();
            if (string.IsNullOrEmpty(cmdStr))
            {
                return cmdArgs;
            }
    
            var ll1 = cmdStr.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in ll1)
            {
                CmdArg arg = new CmdArg();
                string[] ll2 = s.Split(':', StringSplitOptions.RemoveEmptyEntries);
                arg.Cmd = ll2[0];
                arg.Args = new List<long>();
                for (int i = 1; i < ll2.Length; i++)
                {
                    long.TryParse(ll2[i], out long v);
                    arg.Args.Add(v);
                }
    
                cmdArgs.Add(arg);
            }
    
            return cmdArgs;
        }
    
        public static List<ItemArg> ParseItemArgs(this string cmdStr)
        {
            List<ItemArg> itemArgs = new List<ItemArg>();
            if (string.IsNullOrEmpty(cmdStr))
            {
                return itemArgs;
            }
    
            var ll1 = cmdStr.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in ll1)
            {
                ItemArg arg = new ItemArg();
                string[] ll2 = s.Split(':', StringSplitOptions.RemoveEmptyEntries);
                arg.Id = Convert.ToInt32(ll2[0]);
                arg.Count = ll2.Length > 1? Convert.ToInt64(ll2[1]) : 0;
                itemArgs.Add(arg);
            }
    
            return itemArgs;
        }
    }
}