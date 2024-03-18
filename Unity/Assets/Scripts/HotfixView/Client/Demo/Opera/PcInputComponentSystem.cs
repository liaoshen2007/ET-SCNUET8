using System;
using System.Collections.Generic;
using Lean.Touch;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof (PcInputComponent))]
    [FriendOf(typeof (PcInputComponent))]
    public static partial class PcInputComponentSystem
    {
        [EntitySystem]
        private static void Awake(this PcInputComponent self)
        {
            self.AddHotKey("Alpha1", KeyCode.Alpha1);
            self.AddHotKey("Alpha2", KeyCode.Alpha2);
            self.AddHotKey("Alpha3", KeyCode.Alpha3);
            self.AddHotKey("Alpha4", KeyCode.Alpha4);
            self.AddHotKey("Alpha5", KeyCode.Alpha5);
            self.AddHotKey("Alpha6", KeyCode.Alpha6);
            Unit myunit = UnitHelper.GetMyUnitFromClientScene(self.Root());
            self.Mytran=myunit.GetComponent<GameObjectComponent>().Transform;
            self.MyAnimator = myunit.GetComponent<GameObjectComponent>().Animator;
            self.MySpeed = myunit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed);
        }

        [EntitySystem]
        private static void Update(this PcInputComponent self)
        {
            // Get the fingers we want to use
            var fingers = self.Use.GetFingers();

            var pinchScale = LeanGesture.GetPinchRatio(fingers, 0.1f);
            if (pinchScale != 1.0f)
            {
                self.Scene().GetComponent<CameraComponent>().Scale(pinchScale - 1);
            }
            
            //切换视角
            if (Input.GetKeyDown(KeyCode.V))
            {
                self.Scene().GetComponent<CameraComponent>().ChangeCfg();
            }
            
            if (Input.anyKeyDown)
            {
                // foreach (KeyCode keyCode in System.Enum.GetValues(typeof (KeyCode)))
                // {
                //     if (Input.GetKeyDown(keyCode))
                //     {
                //         Log.Info("当前按下的按键类型：" + keyCode);
                //         break;
                //     }
                // }
            }
        }

        private static void AddHotKey(this PcInputComponent self, string name, params KeyCode[] keys)
        {
            if (self.hotKeyDict.TryGetValue(name, out var hotKey))
            {
                return;
            }

            hotKey = new HotKey() { Keys = new List<KeyCode>() };
            foreach (var code in keys)
            {
                hotKey.Keys.Add(code);
            }

            self.hotKeyDict.Add(name, hotKey);
        }

        private static void CheckHotKey(this PcInputComponent self)
        {
            foreach (var pair in self.hotKeyDict)
            {
                if (pair.Value.Check())
                {
                    EventSystem.Instance.Publish(self.Scene(), new HotKeyEvent() { Name = pair.Key });
                }
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
        private static void LateUpdate(this PcInputComponent self)
        {
            self.CheckHotKey();

            if (Input.GetKey(KeyCode.Q))
            {
                self.Scene().GetComponent<CameraComponent>().Yaw(2f);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                self.Scene().GetComponent<CameraComponent>().Yaw(-2f);
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool bMoving = (h != 0.0f) || (v != 0.0f);
            if (bMoving)
            {
                self.moving = true;
                Vector3 dir = AxisToSceneDir(h, v);

                self.Mytran.position+=dir * (5 * Time.smoothDeltaTime);
                self.Mytran.rotation=Quaternion.LookRotation(dir);
                self.Mytran.TransformDirection(dir);
                //GameObjectComponent myGo = myUnit.GetComponent<GameObjectComponent>();
                self.MyAnimator.SetFloat ("Speed", self.MySpeed);							// Animator側で設定している"Speed"パラメタにvを渡す
                // if (self.trunRate<=10f)//Math.Abs(self.trunRate - h) > 0.01f
                // {
                //     self.MyAnimator.SetFloat ("Direction", h); 						// Animator側で設定している"Direction"パラメタにhを渡す
                //     self.trunRate += 10 * Time.smoothDeltaTime; //= h;
                // }
                // else
                // {
                //     self.MyAnimator.SetFloat ("Direction", 0f);
                //     //self.trunRate = 0;
                //     Debug.LogError("self.trunRate = 0");
                // }

                if (Math.Abs(self.trunRate - h) > 0.1f)
                {
                    if (h>0)
                    {
                        self.trunRate += Time.smoothDeltaTime;
                        self.MyAnimator.SetFloat ("Direction", h-self.trunRate); 	
                    }
                    else
                    {
                        self.trunRate -= Time.smoothDeltaTime;
                        self.MyAnimator.SetFloat ("Direction", self.trunRate-h); 	
                    }
                }
                // else
                // {
                //     //self.MyAnimator.SetFloat ("Direction", 0f);
                //     //self.trunRate = 0;
                //     Debug.LogError("self.trunRate = 0");
                // }
                
                // C2M_PathfindingResult c2MPathfindingResult = C2M_PathfindingResult.Create(true);
                // c2MPathfindingResult.Position = myUnit.Position + new float3(dir * (5 * Time.smoothDeltaTime));
                // self.Root().GetComponent<ClientSenderCompnent>().Send(c2MPathfindingResult);
                //Log.Info(dir);
            }
            else if (self.moving)
            {
                self.moving = false;
                self.MyAnimator.SetFloat ("Speed", 0f);	
                self.MyAnimator.SetFloat ("Direction", 0f);
                self.trunRate = 0f;
                UnitHelper.GetMyUnitFromClientScene(self.Root()).GetComponent<GameObjectComponent>().Transform.position = self.Mytran.position;
                //self.Root().GetComponent<ClientSenderComponent>().Send(new C2M_Stop());
            }
        }
    }
}