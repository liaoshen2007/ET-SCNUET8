﻿namespace ET.Server
{
    [EntitySystemOf(typeof (SkillUnit))]
    public static partial class SkillUnitSystem
    {
        [EntitySystem]
        private static void Awake(this SkillUnit self)
        {
        }
    } 
}

