using System.Collections.Generic;

namespace ET.Client;

[ComponentOf(typeof (Scene))]
public class ClientItemComponent: Entity, IAwake, IDestroy
{
    public Dictionary<int, ItemData> ItemDict;
}