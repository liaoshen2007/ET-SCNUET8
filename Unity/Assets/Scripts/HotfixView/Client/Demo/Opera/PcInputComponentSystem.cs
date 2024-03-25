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
            self.MyCapsuleCollider = myunit.GetComponent<GameObjectComponent>().CapsuleCollider;
            self.orgColHight = self.MyCapsuleCollider .height;
            self.orgVectColCenter = self.MyCapsuleCollider .center;
            self.MyRigidbody = myunit.GetComponent<GameObjectComponent>().Rigidbody;

            self.MyAnimator.speed = 1.5f;
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
            self.TestJump();
            bool bMoving = (h != 0.0f) || (v != 0.0f);
            if (bMoving)
            {
                self.moving = true;
                Vector3 dir = AxisToSceneDir(h, v);

                self.Mytran.localPosition+=dir * (5 * Time.smoothDeltaTime);
                self.Mytran.localRotation=Quaternion.LookRotation(dir);
                self.Mytran.TransformDirection(dir);
                //GameObjectComponent myGo = myUnit.GetComponent<GameObjectComponent>();
                self.MyAnimator.SetFloat ("Speed", self.MySpeed);							// Animator側で設定している"Speed"パラメタにvを渡す
                
                #region Fixing

                // if (Math.Abs(self.trunRate) > 0.01f)
                // {
                //     if (self.trunRate>0)
                //     {
                //         self.MyAnimator.SetFloat ("Direction", self.trunRate); 	
                //         self.trunRate -= 0.1f*Time.smoothDeltaTime;
                //         Debug.LogError("self.trunRate"+self.trunRate);
                //
                //     }
                //     else
                //     {
                //         self.MyAnimator.SetFloat ("Direction", self.trunRate); 	
                //         self.trunRate += 0.1f*Time.smoothDeltaTime;
                //         Debug.LogError("self.trunRate"+self.trunRate);
                //     }
                // }

                #endregion
                
                if (Math.Abs(self.trunRate - h) > 0.1f)
                {
                    if (h>0)
                    {
                        self.trunRate += Time.smoothDeltaTime;
                        //self.MyAnimator.SetFloat ("Direction", h-self.trunRate); 	
                    }
                    else
                    {
                        self.trunRate -= Time.smoothDeltaTime;
                        //self.MyAnimator.SetFloat ("Direction", self.trunRate-h); 	
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

        private static void TestJump(this PcInputComponent self)
        {
            self.currentBaseState = self.MyAnimator.GetCurrentAnimatorStateInfo (0);	// 参照用のステート変数にBase Layer (0)の現在のステートを設定する
            self.MyRigidbody.useGravity = true;//ジャンプ中に重力を切るので、それ以外は重力の影響を受けるようにする
            
            if (Input.GetButtonDown ("Jump")) {	// スペースキーを入力したら

                //アニメーションのステートがLocomotionの最中のみジャンプできる
                if (self.currentBaseState.nameHash == self.locoState) {
                    //ステート遷移中でなかったらジャンプできる
                    if (!self.MyAnimator.IsInTransition (0)) {
                        self.MyRigidbody.AddForce (Vector3.up * self.jumpPower, ForceMode.VelocityChange);
                        self.MyAnimator.SetBool ("Jump", true);		// Animatorにジャンプに切り替えるフラグを送る
                    }
                }
            }
            
            // 以下、Animatorの各ステート中での処理
            // Locomotion中
            // 現在のベースレイヤーがlocoStateの時
            if (self.currentBaseState.nameHash == self.locoState) {
                //カーブでコライダ調整をしている時は、念のためにリセットする
                if (self.useCurves) {
                    self.resetCollider();
                }
            }
            
            else if (self.currentBaseState.nameHash == self.jumpState) {
                // ステートがトランジション中でない場合
                if (!self.MyAnimator.IsInTransition (0)) {
				
                    // 以下、カーブ調整をする場合の処理
                    if (self.useCurves) {
                        // 以下JUMP00アニメーションについているカーブJumpHeightとGravityControl
                        // JumpHeight:JUMP00でのジャンプの高さ（0〜1）
                        // GravityControl:1⇒ジャンプ中（重力無効）、0⇒重力有効
                        float jumpHeight = self.MyAnimator.GetFloat ("JumpHeight");
                        float gravityControl = self.MyAnimator.GetFloat ("GravityControl"); 
                        if (gravityControl > 0)
                            self.MyRigidbody.useGravity = false;	//ジャンプ中の重力の影響を切る
										
                        // レイキャストをキャラクターのセンターから落とす
                        Ray ray = new Ray (self.Mytran.position + Vector3.up, -Vector3.up);
                        RaycastHit hitInfo = new RaycastHit ();
                        // 高さが useCurvesHeight 以上ある時のみ、コライダーの高さと中心をJUMP00アニメーションについているカーブで調整する
                        if (Physics.Raycast (ray, out hitInfo)) {
                            if (hitInfo.distance > self.useCurvesHeight) {
                                self.MyCapsuleCollider.height = self.orgColHight - jumpHeight;			// 調整されたコライダーの高さ
                                float adjCenterY = self.orgVectColCenter.y + jumpHeight;
                                self.MyCapsuleCollider.center = new Vector3 (0, adjCenterY, 0);	// 調整されたコライダーのセンター
                            } else {
                                // 閾値よりも低い時には初期値に戻す（念のため）					
                                self.resetCollider();
                            }
                        }
                    }
                    // Jump bool値をリセットする（ループしないようにする）				
                    self.MyAnimator.SetBool ("Jump", false);
                }
            }
            
            else if (self.currentBaseState.nameHash == self.idleState) {
                //カーブでコライダ調整をしている時は、念のためにリセットする
                if (self.useCurves) {
                    self.resetCollider();
                }
                // スペースキーを入力したらRest状態になる
                if (Input.GetButtonDown ("Jump")) {
                    self.MyAnimator.SetBool ("Rest", true);
                }
            }
            // REST中の処理
            // 現在のベースレイヤーがrestStateの時
            else if (self.currentBaseState.nameHash == self.restState) {
                //cameraObject.SendMessage("setCameraPositionFrontView");		// カメラを正面に切り替える
                // ステートが遷移中でない場合、Rest bool値をリセットする（ループしないようにする）
                if (!self.MyAnimator.IsInTransition (0)) {
                    self.MyAnimator.SetBool ("Rest", false);
                }
            }
            
        }

        private static void resetCollider(this PcInputComponent self)
        {
            self.MyCapsuleCollider.height = self.orgColHight;
            self.MyCapsuleCollider.center = self.orgVectColCenter;
        }
    }
}