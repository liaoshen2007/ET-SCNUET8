using System;

namespace ET.Client
{
    public static class NumericHelper
    {
        public static async ETTask<int> TestUpdateNumeric(Scene zoneScene)
        {
            M2C_TestUnitNumeric m2CTestUnitNumeric = null;
            try
            {
                m2CTestUnitNumeric = (M2C_TestUnitNumeric)await zoneScene.GetComponent<ClientSenderComponent>().Call(new C2M_TestUnitNumeric() { });

            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (m2CTestUnitNumeric.Error!=ErrorCode.ERR_Success)
            {
                Log.Debug(m2CTestUnitNumeric.Error.ToString());
                return m2CTestUnitNumeric.Error;
            }

            return ErrorCode.ERR_Success;

        }

        public static async ETTask<int> RequestAddAttributePoint(Scene zoneScene, int numeric)
        {
            M2C_AddAttributePoint m2CAddAttributePoint = null;
            try
            {
                m2CAddAttributePoint = (M2C_AddAttributePoint)await zoneScene.GetComponent<ClientSenderComponent>().Call(new C2M_AddAttributePoint() {NumericType = numeric});
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (m2CAddAttributePoint.Error!=ErrorCode.ERR_Success)
            {
                Log.Error(m2CAddAttributePoint.Error.ToString());
                return m2CAddAttributePoint.Error;
            }
            
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> RequestRoleLevel(Scene zoneScene)
        {
            M2C_UpRoleLevel m2CUpRoleLevel = null;
            try
            {
                m2CUpRoleLevel =(M2C_UpRoleLevel) await zoneScene.GetComponent<ClientSenderComponent>().Call(new C2M_UpRoleLevel() { });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (m2CUpRoleLevel.Error!=ErrorCode.ERR_Success)
            {
                Log.Error(m2CUpRoleLevel.Error.ToString());
                return m2CUpRoleLevel.Error;
            }

            return ErrorCode.ERR_Success;

        }


    }
}