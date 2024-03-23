using System;
using System.Collections.Generic;

namespace ET
{
	public static class NumericType
	{
		public const int Max = 10000;

		/// <summary>
		/// 移动速度
		/// </summary>
		public const int Speed = 1000;

		/// <summary>
		/// 最大血量
		/// </summary>
		public const int MaxHp = 1001;

		/// <summary>
		/// AOI
		/// </summary>
		public const int AOI = 1002;

		/// <summary>
		/// 攻击
		/// </summary>
		public const int Attack = 1003;

		/// <summary>
		/// 防御
		/// </summary>
		public const int Defense = 1004;

		/// <summary>
		/// 破甲
		/// </summary>
		public const int Broken = 1005;

		/// <summary>
		/// 暴击
		/// </summary>
		public const int Cirt = 1006;

		/// <summary>
		/// 暴击伤害
		/// </summary>
		public const int CirtDamage = 1007;

		/// <summary>
		/// 火属性强化
		/// </summary>
		public const int FireAdd = 1008;

		/// <summary>
		/// 火属性抗性
		/// </summary>
		public const int FireAvoid = 1009;

		/// <summary>
		/// 雷属性强化
		/// </summary>
		public const int ThunderAdd = 1010;

		/// <summary>
		/// 雷属性抗性
		/// </summary>
		public const int ThunderAvoid = 1011;

		/// <summary>
		/// 冰属性强化
		/// </summary>
		public const int IceAdd = 1012;

		/// <summary>
		/// 冰属性抗性
		/// </summary>
		public const int IceAvoid = 1013;

		/// <summary>
		/// 伤害加成比例 万比
		/// </summary>
		public const int HurtAddRate = 1014;

		/// <summary>
		/// 伤害减少比例 万比
		/// </summary>
		public const int HurtReduceRate = 1015;

		/// <summary>
		/// 攻击速度 影响技能冷却时间和技能吟唱时间
		/// </summary>
		public const int AttackSpeed = 1016;

		/// <summary>
		/// 命中几率
		/// </summary>
		public const int HitRate = 1017;

		/// <summary>
		/// 闪避几率
		/// </summary>
		public const int AvoidRate = 1018;

		/// <summary>
		/// 格挡
		/// </summary>
		public const int Fender = 1019;

		/// <summary>
		/// 直击
		/// </summary>
		public const int Direct = 1020;

		/// <summary>
		/// 武器特性系数
		/// </summary>
		public const int WpXp = 1021;

		/// <summary>
		/// 吸血率
		/// </summary>
		public const int Suck = 1022;

		/// <summary>
		/// 治疗增加
		/// </summary>
		public const int HealAdd = 1023;

		/// <summary>
		/// 体质
		/// </summary>
		public const int Strength = 3001;
		public const int StrengthBase = Strength * 10 + 1;
		public const int StrengthAdd = Strength * 10 + 2;
		public const int StrengthPct = Strength * 10 + 3;
		public const int StrengthFinalAdd = Strength * 10 + 4;
		public const int StrengthFinalPct = Strength * 10 + 5;

		/// <summary>
		/// 优美
		/// </summary>
		public const int Energy = 3002;
		public const int EnergyBase = Energy * 10 + 1;
		public const int EnergyAdd = Energy * 10 + 2;
		public const int EnergyPct = Energy * 10 + 3;
		public const int EnergyFinalAdd = Energy * 10 + 4;
		public const int EnergyFinalPct = Energy * 10 + 5;

		/// <summary>
		/// 品德
		/// </summary>
		public const int Agility = 3003;
		public const int AgilityBase = Agility * 10 + 1;
		public const int AgilityAdd = Agility * 10 + 2;
		public const int AgilityPct = Agility * 10 + 3;
		public const int AgilityFinalAdd = Agility * 10 + 4;
		public const int AgilityFinalPct = Agility * 10 + 5;

		/// <summary>
		/// 智力
		/// </summary>
		public const int Mind = 3004;
		public const int MindBase = Mind * 10 + 1;
		public const int MindAdd = Mind * 10 + 2;
		public const int MindPct = Mind * 10 + 3;
		public const int MindFinalAdd = Mind * 10 + 4;
		public const int MindFinalPct = Mind * 10 + 5;

		/// <summary>
		/// 属性点
		/// </summary>
		public const int AttrPoint = 3005;

		/// <summary>
		/// 等级
		/// </summary>
		public const int Level = 3006;

		/// <summary>
		/// 金币
		/// </summary>
		public const int Coin = 3007;

		/// <summary>
		/// 血量
		/// </summary>
		public const int Hp = 3008;

		/// <summary>
		/// 所在地图
		/// </summary>
		public const int LocalMap = 3009;

		/// <summary>
		/// 战力值
		/// </summary>
		public const int CombatEffectiveness = 3010;

		/// <summary>
		/// 法力值
		/// </summary>
		public const int Mana = 3011;

		/// <summary>
		/// 经验
		/// </summary>
		public const int Exp = 3012;
	}
}
