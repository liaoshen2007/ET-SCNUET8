using System;

using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(Scene))]
	public class OperaComponent: Entity, IAwake, IUpdate, ILateUpdate
    {
        public Vector3 ClickPoint;

        public bool moving;
	    public int mapMask;
    }
}
