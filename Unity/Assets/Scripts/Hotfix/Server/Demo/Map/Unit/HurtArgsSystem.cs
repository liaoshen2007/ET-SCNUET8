using System.Collections.Generic;

namespace ET.Server
{
    public static class HurtArgsSystem
    {
        private static void Awake(this HurtArgs self)
    {
        self.HurtList = new List<HurtInfo>();
    }
    }
}