using System.Collections.Generic;

namespace ET.Server;

[ComponentOf(typeof (Scene))]
public class ActivityComponent: Entity, IAwake, IDestroy, ILoad
{
    public long Timer;
    public Dictionary<string, ActivityData> ActivityDataDict = new(500);
    public Dictionary<string, ActivityData> RoleActivityDataDict = new(50);
}