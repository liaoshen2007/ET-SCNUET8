using System.Collections.Generic;

namespace ET.Server
{
    public struct UnitEnterGame
    {
        public Unit Unit;
    }

    public struct UnitPerHurt
    {
        public Unit Attacker;
        public Unit Unit;
        public long Hurt;
        public long ShieldHurt;
        public int Id;
    }

    public struct UnitDead
    {
        public Unit Unit;
        public long Killer;
        public int Id;
    }

    /// <summary>
    /// 攻击事件
    /// </summary>
    public struct UnitDoAttack
    {
        public Unit Unit;
        public List<HurtInfo> HurtList;
        public int Element;
    }

    public struct UnitHpChange
    {
        public Unit Unit;
    }

    public struct UnitBeHurt
    {
        public Unit Unit;
        public Unit Attacker;
        public long Hurt;
        public int Id;
    }

    public struct UnitAddHp
    {
        public Unit Unit;
        public Unit Attacker;
        public long Hp;
        public long RealHp;
    }
}