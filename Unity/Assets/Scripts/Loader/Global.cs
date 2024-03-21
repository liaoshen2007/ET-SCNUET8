using UnityEngine;

namespace ET
{
    [Code]
    public class Global: Singleton<Global>, ISingletonAwake
    {
        public Transform GlobalTrans { get; private set; }

        public Transform Unit { get; private set; }

        public Transform UI { get; private set; }

        public Transform Pool { get; private set; }

        public Transform CameraRoot { get; private set; }

        public Transform NormalRoot { get; private set; }

        public Transform PopUpRoot { get; private set; }

        public Transform FixedRoot { get; private set; }

        public Transform OtherRoot { get; private set; }

        /// <summary>
        /// UI摄像机
        /// </summary>
        public Camera UICamera { get; private set; }

        /// <summary>
        /// 主摄像机
        /// </summary>
        public Camera MainCamera { get; private set; }

        /// <summary>
        /// 游戏全局配置
        /// </summary>
        public GlobalConfig GlobalConfig { get; private set; }

        /// <summary>
        /// 摄像机配置
        /// </summary>
        public CameraScriptObject CameraConfig { get; private set; }

        public void Awake()
        {
            GlobalTrans = GameObject.Find("/Global").transform;
            var collector = this.GlobalTrans.GetComponent<ReferenceCollector>();

            UI = collector.Get<Transform>("UI");
            Pool = collector.Get<Transform>("Pool");
            Unit = collector.Get<Transform>("Unit");
            CameraRoot = collector.Get<Transform>("Camera");
            UICamera = collector.Get<Camera>("UICamera");
            MainCamera = collector.Get<Camera>("MainCamera");
            if (this.MainCamera==null)
            {
                Log.Error("this.MainCamera==null");
                MainCamera = Camera.main;
            }

            NormalRoot = this.UI.Find("NormalRoot").transform;
            PopUpRoot = this.UI.Find("PopUpRoot").transform;
            FixedRoot = this.UI.Find("FixedRoot").transform;
            OtherRoot = this.UI.Find("OtherRoot").transform;

            GlobalConfig = Resources.Load<GlobalConfig>(nameof (GlobalConfig));
            CameraConfig = Resources.Load<CameraScriptObject>(nameof (CameraConfig));
        }
    }
}