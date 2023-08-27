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
		/// 症状
		/// </summary>
		public const int Symptom = 1005;
		public const int SymptomBase = Symptom * 10 + 1;
		public const int SymptomAdd = Symptom * 10 + 2;
		public const int SymptomPct = Symptom * 10 + 3;
		public const int SymptomFinalAdd = Symptom * 10 + 4;
		public const int SymptomFinalPct = Symptom * 10 + 5;

		/// <summary>
		/// 暴击几率
		/// </summary>
		public const int CirtRate = 1006;
		public const int CirtRateBase = CirtRate * 10 + 1;
		public const int CirtRateAdd = CirtRate * 10 + 2;
		public const int CirtRatePct = CirtRate * 10 + 3;
		public const int CirtRateFinalAdd = CirtRate * 10 + 4;
		public const int CirtRateFinalPct = CirtRate * 10 + 5;

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
		/// 伤害增加比例 万比
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
		/// 力量
		/// </summary>
		public const int Strength = 3001;

		/// <summary>
		/// 体力
		/// </summary>
		public const int Energy = 3002;

		/// <summary>
		/// 敏捷
		/// </summary>
		public const int Agility = 3003;

		/// <summary>
		/// 精神
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


	}
}
