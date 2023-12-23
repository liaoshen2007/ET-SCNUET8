﻿using System;
using System.Collections.Generic;

namespace ET
{
    [Serializable]
    public class SubEffectArgs
    {
        public string Cmd;

        public List<int> Args;
    }

    /// <summary>
    /// 效果参数
    /// </summary>
    [Serializable]
    public class EffectArg: ICloneable
    {
        public string Cmd;

        public List<int> Args;

        public string ViewCmd;

        public List<SubEffectArgs> SubList;

        public object Clone()
        {
            var effect = new EffectArg()
            {
                Cmd = Cmd, Args = Args, ViewCmd = ViewCmd, SubList = SubList == null? null : new List<SubEffectArgs>(SubList)
            };

            return effect;
        }
    }

    /// <summary>
    /// 效果参数
    /// </summary>
    [Serializable]
    public class SkillEffectArg: ICloneable
    {
        public string Cmd;

        public int Ms;
        
        public int Rate;

        public List<int> Args;

        public string ViewCmd;

        public List<SubEffectArgs> SubList;

        public object Clone()
        {
            var effect = new SkillEffectArg()
            {
                Ms = Ms,
                Cmd = Cmd,
                Args = Args,
                ViewCmd = ViewCmd,
                Rate = Rate,
                SubList = SubList == null? null : new List<SubEffectArgs>(SubList)
            };

            return effect;
        }
    }

    [Serializable]
    public class TalentEffectArgs
    {
        public List<TalentEffectArg> List;
    }

    [Serializable]
    public class TalentEffectArg
    {
        public string Cmd;

        public List<OftCfg> OftList;

        public List<string> Args;
    }

    [Serializable]
    public class OftCfg
    {
        public int TimeType;

        public int Idx;

        public List<int> DstList;
    }
}