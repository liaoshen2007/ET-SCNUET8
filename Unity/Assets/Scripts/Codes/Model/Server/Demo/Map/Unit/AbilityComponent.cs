namespace ET.Server;

[ComponentOf(typeof (Unit))]
public class AbilityComponent: Entity, IAwake, ITransfer
{
    /// <summary>
    /// 能力值
    /// </summary>
    public int Value;

    /// <summary>
    /// 能力列表
    /// </summary>
    public int[] abilityList = new int[(int) RoleAbility.End];
}