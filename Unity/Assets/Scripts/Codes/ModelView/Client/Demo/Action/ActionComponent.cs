using System.Collections.Generic;

namespace ET.Client;

[ComponentOf(typeof (Unit))]
public class ActionComponent: Entity, IAwake, IUpdate, IDestroy
{
    public List<ActionUnit> pushActions = new List<ActionUnit>();

    public EntityRef<ActionUnit> curAction;

    public List<ActionUnit> playActions = new List<ActionUnit>();
}