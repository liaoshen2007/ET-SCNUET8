namespace ET.Server
{
    public static partial class TransferHelper
    {
        public static async ETTask TransferAtFrameFinish(Unit unit, ActorId sceneInstanceId, bool isEnterGame = false)
        {
            await unit.Fiber().WaitFrameFinish();

            Scene root = unit.Root();

            // location加锁
            long unitId = unit.Id;

            M2M_UnitTransferRequest request = M2M_UnitTransferRequest.Create();
            request.OldActorId = unit.GetActorId();
            request.Unit = unit.ToBson();
            request.IsEnterGame = isEnterGame;
            foreach (Entity entity in unit.Components.Values)
            {
                if (entity is ITransfer)
                {
                    request.Entitys.Add(entity.ToBson());
                }
            }
            
            //bugfix 会导致传送至副本时寻路出问题~~
            var aoiEntity=unit.GetComponent<AOIEntity>();
            if (aoiEntity!=null)
            {
                unit.RemoveComponent<AOIEntity>();
            }
            unit.Dispose();

            await root.GetComponent<LocationProxyComponent>().Lock(LocationType.Unit, unitId, request.OldActorId);
            await root.GetComponent<MessageSender>().Call(sceneInstanceId, request);
        }
    }
}