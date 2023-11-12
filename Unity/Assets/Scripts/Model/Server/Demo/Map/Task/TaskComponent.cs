using System;
using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof (Unit))]
    public class TaskComponent: Entity, IAwake, IDestroy
    {
        
    }
}