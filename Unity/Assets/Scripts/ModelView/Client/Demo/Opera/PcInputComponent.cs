using System;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using Unity.Mathematics;

namespace ET.Client
{
    public struct HotKeyEvent
    {
        public string Name;
    }

    public struct HotKey
    {
        public List<KeyCode> Keys;

        public bool Check()
        {
            bool hasDown = false;
            for (int i = 0; i < this.Keys.Count; i++)
            {
                KeyCode code = this.Keys[i];
                if (!Input.GetKeyDown(code))
                {
                    continue;
                }

                hasDown = true;
                break;
            }

            if (!hasDown)
            {
                return false;
            }

            for (int i = 0; i < this.Keys.Count; i++)
            {
                KeyCode code = this.Keys[i];
                if (!Input.GetKey(code))
                {
                    return false;
                }
            }

            return true;
        }
    }

    [ComponentOf(typeof (Scene))]
    public class PcInputComponent: Entity, IAwake, IUpdate, ILateUpdate
    {
        public LeanFingerFilter Use = new LeanFingerFilter(true);
        
        public bool moving;
        public Dictionary<string, HotKey> hotKeyDict = new Dictionary<string, HotKey>();

        public Transform Mytran;

        public Animator MyAnimator;

        public float MySpeed;

        public float trunRate;
        
        public bool useCurves = true;				// Mecanimでカーブ調整を使うか設定する
        // このスイッチが入っていないとカーブは使われない
        public float useCurvesHeight = 0.5f;		// カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）
        
        public Vector3 velocity;
        public float orgColHight;
        public Vector3 orgVectColCenter;
        public AnimatorStateInfo currentBaseState;			// base layerで使われる、アニメーターの現在の状態の参照
        
        public float jumpPower = 3.0f; 
        
        public int idleState = Animator.StringToHash ("Base Layer.Idle");
        public int locoState = Animator.StringToHash ("Base Layer.Locomotion");
        public int jumpState = Animator.StringToHash ("Base Layer.Jump");
        public int restState = Animator.StringToHash ("Base Layer.Rest");

        public Rigidbody MyRigidbody;

        public CapsuleCollider MyCapsuleCollider;

    }
}