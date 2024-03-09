using System;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof (OperaComponent))]
    [FriendOf(typeof (OperaComponent))]
    public static partial class OperaComponentSystem
    {
        public static void SetUnit(this OperaComponent self,GameObject gameObject)
        {
            self.myGo = gameObject; //UnitHelper.GetMyUnitFromZoneScene(self.DomainScene());

        }
        
        [EntitySystem]
        private static void Awake(this OperaComponent self)
        {
            self.mapMask = LayerMask.GetMask("Map");
        }

        [EntitySystem]
        private static void Update(this OperaComponent self)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Global.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                {
                    C2M_PathfindingResult c2MPathfindingResult = new();
                    c2MPathfindingResult.Position = hit.point;
                    self.ClickPoint = hit.point;
                    self.Root().GetComponent<ClientSenderComponent>().Send(c2MPathfindingResult);
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                CodeLoader.Instance.Reload();
                return;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                C2M_TransferMap c2MTransferMap = new();
                self.Root().GetComponent<ClientSenderComponent>().Send(c2MTransferMap);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Log.Info("--------Escape--------");
            }
        }
    }
}