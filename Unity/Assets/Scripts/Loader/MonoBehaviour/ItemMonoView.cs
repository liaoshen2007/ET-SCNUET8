using System;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace ET.Client
{
    [Flags]
    public enum ItemTagType
    {
        Frame = 1,
        Icon = 1 << 2,
        Name = 1 << 3,
        Count = 1 << 4,
    }

    public class ItemMonoView: MonoBehaviour
    {
        /// <summary>
        /// 获取道具子节点
        /// </summary>
        /// <param name="tag"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetChild<T>(ItemTagType tag) where T : UObject
        {
            this.instDict.TryGetValue(tag, out var obj);
            return obj as T;
        }

        private void Awake()
        {
            if (this.canvasGroup)
            {
                this.canvasGroup.interactable = this.canClick;
                this.canvasGroup.blocksRaycasts = this.blockRaycasts;
            }

            this.trans.sizeDelta = this.size;
            UpdateView();
        }

        [ContextMenu("UpdateView")]
        private void UpdateView()
        {
            this.instDict.Clear();
            if ((tagType & ItemTagType.Frame) != 0)
            {
                this.CreateInst(ItemTagType.Frame);
            }

            if ((tagType & ItemTagType.Icon) != 0)
            {
                this.CreateInst(ItemTagType.Icon);
            }

            if ((tagType & ItemTagType.Name) != 0)
            {
                this.CreateInst(ItemTagType.Name);
            }

            if ((tagType & ItemTagType.Count) != 0)
            {
                this.CreateInst(ItemTagType.Count);
            }
        }

        private void CreateInst(ItemTagType tag)
        {
            var prefab = this.collector.Get<GameObject>(tag.ToString());
            var inst = Instantiate(prefab, this.trans);
            UObject com = null;
            switch (tagType)
            {
                case ItemTagType.Frame:
                    com = inst.GetComponent<ExtendImage>();
                    break;
                case ItemTagType.Icon:
                    com = inst.GetComponent<ExtendImage>();
                    break;
                case ItemTagType.Name:
                    com = inst.GetComponent<ExtendText>();
                    break;
                case ItemTagType.Count:
                    com = inst.GetComponent<ExtendText>();
                    break;
            }

            this.instDict.Add(tag, com);
        }

        private void OnDestroy()
        {
            this.instDict.Clear();
        }

        private void OnValidate()
        {
            this.canvasGroup = this.GetComponentInChildren<CanvasGroup>();
            this.trans = this.transform.GetChild(0) as RectTransform;
            collector = this.GetComponent<ReferenceCollector>();
        }

        private Dictionary<ItemTagType, UObject> instDict = new Dictionary<ItemTagType, UObject>();

        [SerializeField]
        private Vector2 size = new Vector2(100, 100);

        [SerializeField]
        private ItemTagType tagType = ItemTagType.Frame | ItemTagType.Icon;

        [SerializeField]
        private bool canClick = true;

        [SerializeField]
        private bool blockRaycasts = true;

        [SerializeField]
        private RectTransform trans;

        [SerializeField]
        private ReferenceCollector collector;

        [SerializeField]
        private CanvasGroup canvasGroup;
    }
}