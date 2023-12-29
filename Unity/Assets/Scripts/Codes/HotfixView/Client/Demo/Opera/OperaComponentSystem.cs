using System;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof (OperaComponent))]
    [FriendOf(typeof (OperaComponent))]
    public static partial class OperaComponentSystem
    {
        [EntitySystem]
        private static void Awake(this OperaComponent self)
        {
            self.mapMask = LayerMask.GetMask("Map");
        }

        [EntitySystem]
        private static void Update(this OperaComponent self)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Global.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                {
                    C2M_PathfindingResult c2MPathfindingResult = new();
                    c2MPathfindingResult.Position = hit.point;
                    self.Root().GetComponent<ClientSenderCompnent>().Send(c2MPathfindingResult);
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
                self.Root().GetComponent<ClientSenderCompnent>().Send(c2MTransferMap);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Log.Info("--------Escape--------");
            }
        }

        private static Vector3 AxisToSceneDir(float h, float v)
        {
            Vector3 moveDir = Vector3.zero;
            moveDir.Set(h, 0, v);
            Vector3 vDir = Global.Instance.MainCamera.transform.rotation.eulerAngles;
            vDir.x = 0;
            Quaternion qDir = Quaternion.Euler(vDir);
            moveDir = qDir * moveDir;
            moveDir.Normalize();
            return moveDir;
        }

        [EntitySystem]
        private static void LateUpdate(this OperaComponent self)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool bMoving = (h != 0.0f) || (v != 0.0f);
            if (bMoving)
            {
                self.moving = true;
                Vector3 dir = AxisToSceneDir(h, v);
                Unit myUnit = UnitHelper.GetMyUnitFromClientScene(self.Root());
                // C2M_PathfindingResult c2MPathfindingResult = C2M_PathfindingResult.Create(true);
                // c2MPathfindingResult.Position = myUnit.Position + new float3(dir * (5 * Time.smoothDeltaTime));
                // self.Root().GetComponent<ClientSenderCompnent>().Send(c2MPathfindingResult);
                Log.Info(dir);
            }
            else if (self.moving)
            {
                self.moving = false;
                self.Root().GetComponent<ClientSenderCompnent>().Send(new C2M_Stop());
            }
        }
    }
}