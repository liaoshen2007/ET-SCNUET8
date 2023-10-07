using UnityEngine;

namespace ET.Client
{
    /// <summary>
    /// Unit 对应的Unity 对象
    /// </summary>
    [ComponentOf(typeof (Unit))]
    [EnableMethod]
    public class GameObjectComponent: Entity, IAwake, IDestroy
    {
        public GameObject GameObject { get; private set; }
        
        public Animator Animator {get; private set;}

        public Transform Transform { get; private set; }

        public Transform ChestTrans { get; private set; }

        public Transform DownTrans { get; private set; }

        public Transform HudTrans { get; private set; }

        public AudioSource AudioSource { get; private set; }

        public void SetGo(GameObject go)
        {
            this.GameObject = go;
            this.Transform = go.transform;
            this.AudioSource = go.transform.Find("AudioSource").GetComponent<AudioSource>();
            this.Animator = go.GetComponentInChildren<Animator>();
            
            var child = go.transform.Find("Bones");
            this.ChestTrans = child.Find("Chest");
            this.DownTrans = child.Find("Down");
            this.HudTrans = child.Find("Name");
        }
    }
}