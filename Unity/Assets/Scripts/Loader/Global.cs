using UnityEngine;

namespace ET
{
    [Code]
    public class Global: Singleton<Global>, ISingletonAwake
    {
        public Transform GlobalTrans { get; private set; }

        public Transform Unit { get; private set; }

        public Transform UI { get; private set; }

        public Transform NormalRoot { get; private set; }

        public Transform PopUpRoot { get; private set; }

        public Transform FixedRoot { get; private set; }

        public Transform PoolRoot { get; private set; }

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
        public GlobalConfig GlobalConfig { get; internal set; }

        public void Awake()
        {
            GlobalTrans = GameObject.Find("/Global").transform;
            Unit = GameObject.Find("/Global/Unit").transform;
            UI = GameObject.Find("/Global/UI").transform;
            UICamera = GameObject.Find("Global/UICamera").GetComponent<Camera>();
            MainCamera = GameObject.Find("Global/MainCamera").GetComponent<Camera>();
            NormalRoot = GameObject.Find("Global/UI/NormalRoot").transform;
            PopUpRoot = GameObject.Find("Global/UI/PopUpRoot").transform;
            FixedRoot = GameObject.Find("Global/UI/FixedRoot").transform;
            OtherRoot = GameObject.Find("Global/UI/OtherRoot").transform;
            PoolRoot = GameObject.Find("Global/Pool").transform;
            GlobalConfig = Resources.Load<GlobalConfig>(nameof (GlobalConfig));
        }
    }
}