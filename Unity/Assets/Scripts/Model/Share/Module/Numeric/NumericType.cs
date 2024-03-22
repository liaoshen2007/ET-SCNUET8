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
		public const int SpeedBase = Speed * 10 + 1;
		public const int SpeedAdd = Speed * 10 + 2;
		public const int SpeedPct = Speed * 10 + 3;
		public const int SpeedFinalAdd = Speed * 10 + 4;
		public const int SpeedFinalPct = Speed * 10 + 5;

		/// <summary>
		/// 最大血量
		/// </summary>
		public const int MaxHp = 1001;
		public const int MaxHpBase = MaxHp * 10 + 1;
		public const int MaxHpAdd = MaxHp * 10 + 2;
		public const int MaxHpPct = MaxHp * 10 + 3;
		public const int MaxHpFinalAdd = MaxHp * 10 + 4;
		public const int MaxHpFinalPct = MaxHp * 10 + 5;

		/// <summary>
		/// AOI
		/// </summary>
		public const int AOI = 1002;
		public const int AOIBase = AOI * 10 + 1;
		public const int AOIAdd = AOI * 10 + 2;
		public const int AOIPct = AOI * 10 + 3;
		public const int AOIFinalAdd = AOI * 10 + 4;
		public const int AOIFinalPct = AOI * 10 + 5;

		/// <summary>
		/// 攻击
		/// </summary>
		public const int Attack = 1003;
		public const int AttackBase = Attack * 10 + 1;
		public const int AttackAdd = Attack * 10 + 2;
		public const int AttackPct = Attack * 10 + 3;
		public const int AttackFinalAdd = Attack * 10 + 4;
		public const int AttackFinalPct = Attack * 10 + 5;

		/// <summary>
		/// 防御
		/// </summary>
		public const int Defense = 1004;
		public const int DefenseBase = Defense * 10 + 1;
		public const int DefenseAdd = Defense * 10 + 2;
		public const int DefensePct = Defense * 10 + 3;
		public const int DefenseFinalAdd = Defense * 10 + 4;
		public const int DefenseFinalPct = Defense * 10 + 5;

		/// <summary>
		/// 破甲
		/// </summary>
		public const int Broken = 1005;
		public const int BrokenBase = Broken * 10 + 1;
		public const int BrokenAdd = Broken * 10 + 2;
		public const int BrokenPct = Broken * 10 + 3;
		public const int BrokenFinalAdd = Broken * 10 + 4;
		public const int BrokenFinalPct = Broken * 10 + 5;

		/// <summary>
		/// 暴击
		/// </summary>
		public const int Cirt = 1006;
		public const int CirtBase = Cirt * 10 + 1;
		public const int CirtAdd = Cirt * 10 + 2;
		public const int CirtPct = Cirt * 10 + 3;
		public const int CirtFinalAdd = Cirt * 10 + 4;
		public const int CirtFinalPct = Cirt * 10 + 5;

		/// <summary>
		/// 暴击伤害
		/// </summary>
		public const int CirtDamage = 1007;
		public const int CirtDamageBase = CirtDamage * 10 + 1;
		public const int CirtDamageAdd = CirtDamage * 10 + 2;
		public const int CirtDamagePct = CirtDamage * 10 + 3;
		public const int CirtDamageFinalAdd = CirtDamage * 10 + 4;
		public const int CirtDamageFinalPct = CirtDamage * 10 + 5;

		/// <summary>
		/// 火属性强化
		/// </summary>
		public const int FireAdd = 1008;
		public const int FireAddBase = FireAdd * 10 + 1;
		public const int FireAddAdd = FireAdd * 10 + 2;
		public const int FireAddPct = FireAdd * 10 + 3;
		public const int FireAddFinalAdd = FireAdd * 10 + 4;
		public const int FireAddFinalPct = FireAdd * 10 + 5;

		/// <summary>
		/// 火属性抗性
		/// </summary>
		public const int FireAvoid = 1009;
		public const int FireAvoidBase = FireAvoid * 10 + 1;
		public const int FireAvoidAdd = FireAvoid * 10 + 2;
		public const int FireAvoidPct = FireAvoid * 10 + 3;
		public const int FireAvoidFinalAdd = FireAvoid * 10 + 4;
		public const int FireAvoidFinalPct = FireAvoid * 10 + 5;

		/// <summary>
		/// 雷属性强化
		/// </summary>
		public const int ThunderAdd = 1010;
		public const int ThunderAddBase = ThunderAdd * 10 + 1;
		public const int ThunderAddAdd = ThunderAdd * 10 + 2;
		public const int ThunderAddPct = ThunderAdd * 10 + 3;
		public const int ThunderAddFinalAdd = ThunderAdd * 10 + 4;
		public const int ThunderAddFinalPct = ThunderAdd * 10 + 5;

		/// <summary>
		/// 雷属性抗性
		/// </summary>
		public const int ThunderAvoid = 1011;
		public const int ThunderAvoidBase = ThunderAvoid * 10 + 1;
		public const int ThunderAvoidAdd = ThunderAvoid * 10 + 2;
		public const int ThunderAvoidPct = ThunderAvoid * 10 + 3;
		public const int ThunderAvoidFinalAdd = ThunderAvoid * 10 + 4;
		public const int ThunderAvoidFinalPct = ThunderAvoid * 10 + 5;

		/// <summary>
		/// 冰属性强化
		/// </summary>
		public const int IceAdd = 1012;
		public const int IceAddBase = IceAdd * 10 + 1;
		public const int IceAddAdd = IceAdd * 10 + 2;
		public const int IceAddPct = IceAdd * 10 + 3;
		public const int IceAddFinalAdd = IceAdd * 10 + 4;
		public const int IceAddFinalPct = IceAdd * 10 + 5;

		/// <summary>
		/// 冰属性抗性
		/// </summary>
		public const int IceAvoid = 1013;
		public const int IceAvoidBase = IceAvoid * 10 + 1;
		public const int IceAvoidAdd = IceAvoid * 10 + 2;
		public const int IceAvoidPct = IceAvoid * 10 + 3;
		public const int IceAvoidFinalAdd = IceAvoid * 10 + 4;
		public const int IceAvoidFinalPct = IceAvoid * 10 + 5;

		/// <summary>
		/// 伤害加成比例 万比
		/// </summary>
		public const int HurtAddRate = 1014;
		public const int HurtAddRateBase = HurtAddRate * 10 + 1;
		public const int HurtAddRateAdd = HurtAddRate * 10 + 2;
		public const int HurtAddRatePct = HurtAddRate * 10 + 3;
		public const int HurtAddRateFinalAdd = HurtAddRate * 10 + 4;
		public const int HurtAddRateFinalPct = HurtAddRate * 10 + 5;

		/// <summary>
		/// 伤害减少比例 万比
		/// </summary>
		public const int HurtReduceRate = 1015;
		public const int HurtReduceRateBase = HurtReduceRate * 10 + 1;
		public const int HurtReduceRateAdd = HurtReduceRate * 10 + 2;
		public const int HurtReduceRatePct = HurtReduceRate * 10 + 3;
		public const int HurtReduceRateFinalAdd = HurtReduceRate * 10 + 4;
		public const int HurtReduceRateFinalPct = HurtReduceRate * 10 + 5;

		/// <summary>
		/// 攻击速度 影响技能冷却时间和技能吟唱时间
		/// </summary>
		public const int AttackSpeed = 1016;
		public const int AttackSpeedBase = AttackSpeed * 10 + 1;
		public const int AttackSpeedAdd = AttackSpeed * 10 + 2;
		public const int AttackSpeedPct = AttackSpeed * 10 + 3;
		public const int AttackSpeedFinalAdd = AttackSpeed * 10 + 4;
		public const int AttackSpeedFinalPct = AttackSpeed * 10 + 5;

		/// <summary>
		/// 命中几率
		/// </summary>
		public const int HitRate = 1017;
		public const int HitRateBase = HitRate * 10 + 1;
		public const int HitRateAdd = HitRate * 10 + 2;
		public const int HitRatePct = HitRate * 10 + 3;
		public const int HitRateFinalAdd = HitRate * 10 + 4;
		public const int HitRateFinalPct = HitRate * 10 + 5;

		/// <summary>
		/// 闪避几率
		/// </summary>
		public const int AvoidRate = 1018;
		public const int AvoidRateBase = AvoidRate * 10 + 1;
		public const int AvoidRateAdd = AvoidRate * 10 + 2;
		public const int AvoidRatePct = AvoidRate * 10 + 3;
		public const int AvoidRateFinalAdd = AvoidRate * 10 + 4;
		public const int AvoidRateFinalPct = AvoidRate * 10 + 5;

		/// <summary>
		/// 格挡
		/// </summary>
		public const int Fender = 1019;
		public const int FenderBase = Fender * 10 + 1;
		public const int FenderAdd = Fender * 10 + 2;
		public const int FenderPct = Fender * 10 + 3;
		public const int FenderFinalAdd = Fender * 10 + 4;
		public const int FenderFinalPct = Fender * 10 + 5;

		/// <summary>
		/// 直击
		/// </summary>
		public const int Direct = 1020;
		public const int DirectBase = Direct * 10 + 1;
		public const int DirectAdd = Direct * 10 + 2;
		public const int DirectPct = Direct * 10 + 3;
		public const int DirectFinalAdd = Direct * 10 + 4;
		public const int DirectFinalPct = Direct * 10 + 5;

		/// <summary>
		/// 武器特性系数
		/// </summary>
		public const int WpXp = 1021;
		public const int WpXpBase = WpXp * 10 + 1;
		public const int WpXpAdd = WpXp * 10 + 2;
		public const int WpXpPct = WpXp * 10 + 3;
		public const int WpXpFinalAdd = WpXp * 10 + 4;
		public const int WpXpFinalPct = WpXp * 10 + 5;

		/// <summary>
		/// 吸血率
		/// </summary>
		public const int Suck = 1022;
		public const int SuckBase = Suck * 10 + 1;
		public const int SuckAdd = Suck * 10 + 2;
		public const int SuckPct = Suck * 10 + 3;
		public const int SuckFinalAdd = Suck * 10 + 4;
		public const int SuckFinalPct = Suck * 10 + 5;

		/// <summary>
		/// 治疗增加
		/// </summary>
		public const int HealAdd = 1023;
		public const int HealAddBase = HealAdd * 10 + 1;
		public const int HealAddAdd = HealAdd * 10 + 2;
		public const int HealAddPct = HealAdd * 10 + 3;
		public const int HealAddFinalAdd = HealAdd * 10 + 4;
		public const int HealAddFinalPct = HealAdd * 10 + 5;

		/// <summary>
		/// 体质
		/// </summary>
		public const int Strength = 3001;

		/// <summary>
		/// 优美
		/// </summary>
		public const int Energy = 3002;

		/// <summary>
		/// 品德
		/// </summary>
		public const int Agility = 3003;

		/// <summary>
		/// 智力
		/// </summary>
		public const int Mind = 3004;

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
	}
}
