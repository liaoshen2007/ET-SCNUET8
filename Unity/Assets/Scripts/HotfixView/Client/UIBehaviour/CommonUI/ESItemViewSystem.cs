using UnityEngine;
using UnityEngine.UI;

namespace ET.Client

{
    [EntitySystemOf(typeof (ESItem))]
    [FriendOf(typeof (ESItem))]
    public static partial class ESItemSystem
    {
        [EntitySystem]
        private static void Awake(this ESItem self, Transform transform)
        {
            self.uiTransform = transform;
            self.itemView = transform.GetComponent<ItemMonoView>();
        }

        [EntitySystem]
        private static void Destroy(this ESItem self)
        {
            self.DestroyWidget();
        }

        /// <summary>
        /// 设置道具数量显示
        /// </summary>
        /// <param name="self"></param>
        /// <param name="count"></param>
        public static void SetCount(this ESItem self, long count)
        {
            var text = self.itemView.GetChild<ExtendText>(ItemTagType.Count);
            text.text = AmountHelper.GetAmountText(count, out var color);
            text.color = color;
        }

        public static async ETTask SetIcon(this ESItem self, string icon)
        {
            var img = self.itemView.GetChild<ExtendImage>(ItemTagType.Icon);
            img.sprite = await self.LoadIconSpriteAsync(icon);
        }

        public static async ETTask SetFrame(this ESItem self, QualityType quality)
        {
            var img = self.itemView.GetChild<ExtendImage>(ItemTagType.Frame);
            var qualityCfg = QualityConfigCategory.Instance.Get((int) quality);
            img.sprite = await self.LoadWidgetSpriteAsync(qualityCfg.ItemFrame);
        }

        public static void SetName(this ESItem self, int name, QualityType quality)
        {
            var text = self.itemView.GetChild<ExtendText>(ItemTagType.Name);
            var qualityCfg = QualityConfigCategory.Instance.Get((int) quality);
            var nameCfg = LanguageCategory.Instance.Get(name);
            text.text = nameCfg.Msg;
            text.color = qualityCfg.ColorBytes.BytesColor();
        }
    }
}