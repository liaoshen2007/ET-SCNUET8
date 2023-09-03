using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

namespace ET.Server;

public enum OriginMode
{
    Actor,
    Parent,
}

[Flags]
public enum FilterMode
{
    IncludeSelf = 1,
    IncludeInvincible = 2,
}

public enum SharpType
{
    /// <summary>
    /// 直线(矩形)
    /// </summary>
    Line = 1,

    /// <summary>
    /// 圆形
    /// </summary>
    Round = 2,

    /// <summary>
    /// 扇形
    /// </summary>
    Fan = 3,
}

/// <summary>
/// Unit 选取器
/// </summary>
public static class UnitFilter
{
    private static bool CalcPointLine(double tan, double x0, double y0)
    {
        return tan * x0 - y0 <= 0;
    }

    private static double GetDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Floor(Math.Pow((x2 - x1), 2) + Math.Pow((y1 - y1), 2));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unitList"></param>
    /// <param name="srcX"></param>
    /// <param name="srcZ"></param>
    /// <param name="sharp"></param>
    /// <param name="direct"></param>
    /// <param name="repairPos"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <param name="dstSet"></param>
    /// <returns></returns>
    public static List<Unit> GetSlector(List<Unit> unitList,
    float srcX, float srcZ,
    SharpType sharp,
    float direct,
    float repairPos,
    float arg1, float arg2,
    HashSet<long> dstSet = null)
    {
        var list = new List<Unit>();
        var repair = UnitHelper.GetRepairPos(srcX, srcZ, direct, repairPos);
        srcX = repair.Key;
        srcZ = repair.Value;
        switch (sharp)
        {
            case SharpType.Line:
                double cosX = Math.Cos(Math.Atan(-direct));
                double sinX = Math.Sign(Math.Atan(-direct));
                foreach (var unit in unitList)
                {
                    var modelR = unit.Config().ModelR;
                    if (dstSet != null && dstSet.Contains(unit.Id))
                    {
                        modelR += 500;
                    }

                    float dstX0 = unit.Position.x - srcX;
                    float dstZ0 = unit.Position.z - srcZ;
                    double dstX = dstX0 * cosX - dstZ0 * sinX;
                    double dstZ = dstX0 * sinX + dstZ0 * cosX;
                    if (dstX <= arg1 + modelR && dstX >= -modelR && dstZ <= (arg2 + modelR) / 2 && dstZ >= -(arg2 + modelR) / 2)
                    {
                        list.Add(unit);
                    }
                }

                break;
            case SharpType.Round:
                foreach (var unit in unitList)
                {
                    if (GetDistance(srcX, srcZ, unit.Position.x, unit.Position.z) <= Math.Pow((arg1 + unit.Config().ModelR), 2))
                    {
                        list.Add(unit);
                    }
                }

                break;
            case SharpType.Fan:
                foreach (var unit in unitList)
                {
                    if (GetDistance(srcX, srcZ, unit.Position.x, unit.Position.z) <= Math.Pow((arg1 + unit.Config().ModelR), 2))
                    {
                        list.Add(unit);
                    }
                }

                arg2 %= 360;
                if (arg2 == 0)
                {
                    return list;
                }

                //用0.001 做角度的偏移运算使起始线、未尾线不在坐标轴上
                double tanB = Math.Tan(Math.Atan(direct - arg2 / 2 + 0.001f));
                int relB = (int) Math.Ceiling((direct - arg2 / 2 + 0.001f) % 360 / 90);
                double tanE = Math.Tan(Math.Atan(direct + arg2 / 2 - 0.001f));
                int relE = (int) Math.Ceiling((direct - arg2 / 2 - 0.001f) % 360 / 90);
                foreach (var unit in unitList)
                {
                    float dstX0 = unit.Position.x - srcX;
                    float dstZ0 = unit.Position.z - srcZ;

                    // 判断角度
                    // 根据点和起始线和末尾线的关系选择
                    bool br = CalcPointLine(tanB, dstX0, dstZ0);
                    bool be = CalcPointLine(tanE, dstX0, dstZ0);
                    if (((relB is <= 1 or >= 4 && br) || (relB is > 1 and < 4 && !br)) &&
                        ((relE is <= 1 or >= 4 && !be) || (relE is > 1 and < 4 && be)))
                    {
                        list.Add(unit);
                    }
                    else
                    {
                        var modelR = unit.Config().ModelR;
                        if (dstSet != null && dstSet.Contains(unit.Id))
                        {
                            modelR += 500;
                        }

                        double sqrtB = Math.Sqrt(Math.Pow(tanB, 2) + 1);
                        double rb = (tanB * dstX0 - dstZ0) / sqrtB;
                        if (Math.Pow(rb, 2) <= Math.Pow(modelR, 2) && ((relB == 1 && dstZ0 > -modelR && dstX0 > -modelR)
                                || (relB == 2 && dstZ0 > -modelR && dstX0 < modelR)
                                || (relB == 3 && dstZ0 < modelR && dstX0 < modelR)
                                || (relB == 4 && dstZ0 < modelR && dstX0 > -modelR)))
                        {
                            list.Add(unit);
                        }
                        else
                        {
                            double sqrtE = Math.Sqrt(Math.Pow(tanE, 2) + 1);
                            double re = (tanE * dstX0 - dstZ0) / sqrtE;
                            if (Math.Pow(re, 2) <= Math.Pow(modelR, 2) && ((relE == 1 && dstZ0 > -modelR && dstX0 > -modelR)
                                    || (relE == 2 && dstZ0 > -modelR && dstX0 < modelR)
                                    || (relE == 3 && dstZ0 < modelR && dstX0 < modelR)
                                    || (relE == 4 && dstZ0 < modelR && dstX0 > -modelR)))
                            {
                                list.Add(unit);
                            }
                        }
                    }
                }

                break;
        }

        return list;
    }

    /// <summary>
    /// 获取攻击列表
    /// </summary>
    /// <param name="self"></param>
    /// <param name="fT"></param>
    /// <param name="rT"></param>
    /// <param name="direct"></param>
    /// <param name="dstList"></param>
    /// <param name="dstPosList"></param>
    /// <param name="repairPos"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <param name="arg3"></param>
    /// <param name="arg4"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    public static List<Unit> GetAttackList(this Unit self,
    FocusType fT,
    RangeType rT,
    float direct,
    List<Unit> dstList,
    List<float3> dstPosList,
    float repairPos,
    float arg1, int arg2, float arg3, float arg4,
    float maxDistance)
    {
        HashSet<long> dstSet = new HashSet<long>();
        if (dstList != null && self.Type() == UnitType.Player)
        {
            foreach (var unit in dstList)
            {
                dstSet.Add(unit.Id);
            }
        }

        List<Unit> list = new List<Unit>();
        int maxCount = 99999;
        List<Unit> list2 = new List<Unit>();
        switch (rT)
        {
            case RangeType.Self:
            {
                list2.Add(self);
                break;
            }
            case RangeType.Single:
            {
                maxCount = 1;
                list2 = GetSlector(dstList, self.Position.x, self.Position.z, SharpType.Round, direct, repairPos, maxDistance * 1.2f, 0, dstSet);
                break;
            }
            case RangeType.Much:
            {
                maxCount = arg2;
                list2 = GetSlector(dstList, self.Position.x, self.Position.z, SharpType.Round, direct, repairPos, maxDistance * 1.2f, 0, dstSet);
                break;
            }
            case RangeType.SelfFan:
            {
                var ll = self.GetFoucslayers(fT);
                list2 = GetSlector(ll, self.Position.x, self.Position.z, SharpType.Fan, direct, repairPos, arg1, arg2);
                list2.Add(self);
                break;
            }
            case RangeType.SelfLine:
            {
                var ll = self.GetFoucslayers(fT);
                list2 = GetSlector(ll, self.Position.x, self.Position.z, SharpType.Line, direct, repairPos, arg1, arg2);
                list2.Add(self);
                break;
            }
            case RangeType.DstLine:
            {
                var ll = self.GetFoucslayers(fT);
                foreach (var pos in dstPosList)
                {
                    var rList = GetSlector(ll, pos.x, pos.z, SharpType.Line, direct, repairPos, arg1, arg2);
                    list2.AddRange(rList);
                }

                break;
            }
            case RangeType.DstFan:
            {
                var ll = self.GetFoucslayers(fT);
                foreach (var pos in dstPosList)
                {
                    var rList = GetSlector(ll, pos.x, pos.z, SharpType.Fan, direct, repairPos, arg1, arg2);
                    list2.AddRange(rList);
                }

                break;
            }
            case RangeType.DstFanLine:
            {
                var ll = self.GetFoucslayers(fT);
                var ll1 = GetSlector(ll, self.Position.x, self.Position.z, SharpType.Fan, direct, repairPos, arg1, arg2);
                var ll2 = GetSlector(ll, self.Position.x, self.Position.z, SharpType.Line, direct, repairPos, arg3, arg4);
                list2.AddRange(ll1);
                list2.AddRange(ll2);

                break;
            }
            case RangeType.UseLast:
                break;
        }

        int count = 0;
        HashSet<long> unitMap = new HashSet<long>();
        foreach (var unit in list2)
        {
            if (!unitMap.Contains(unit.Id))
            {
                list.Add(unit);
                unitMap.Add(unit.Id);
                count += 1;
                if (count >= maxCount)
                {
                    break;
                }
            }
        }

        return list;
    }
}