using System;
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
        public int Dst;
        public int RangeType;
        public List<int> RangeArgs;

        public string Cmd;
        public int Ms;
        public List<int> Args;
        public int Rate;

        public string ViewCmd;

        public List<SubEffectArgs> SubList;

        public int this[int i]
        {
            get
            {
                if (i >= this.RangeArgs.Count)
                {
                    return 0;
                }

                return this.RangeArgs[i];
            }
        }

        public object Clone()
        {
            var effect = new SkillEffectArg()
            {
                Dst = Dst,
                Rate = Rate,
                Ms = Ms,
                Cmd = Cmd,
                Args = Args,
                ViewCmd = ViewCmd,
                SubList = SubList == null? null : new List<SubEffectArgs>(SubList),
                RangeType = RangeType,
                RangeArgs = RangeArgs,
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