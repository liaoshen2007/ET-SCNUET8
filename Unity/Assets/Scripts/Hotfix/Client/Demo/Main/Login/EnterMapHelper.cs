using System;

namespace ET.Client
{
    public static partial class EnterMapHelper
    {
        public static async ETTask<int> EnterMapAsync(Scene root)
        {
            try
            {
                G2C_EnterMap g2CEnterMap = await root.GetComponent<ClientSenderCompnent>().Call(new C2G_EnterMap()) as G2C_EnterMap;

                if (g2CEnterMap.Error != ErrorCode.ERR_Success)
                {
                    return g2CEnterMap.Error;
                }

                // 等待场景切换完成
                await root.GetComponent<ObjectWait>().Wait<Wait_SceneChangeFinish>();

                await EventSystem.Instance.PublishAsync(root, new EnterMapFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return ErrorCode.ERR_Success;
        }

        public static async ETTask Match(Fiber fiber)
        {
            try
            {
                G2C_Match g2CEnterMap = await fiber.Root.GetComponent<ClientSenderCompnent>().Call(new C2G_Match()) as G2C_Match;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}