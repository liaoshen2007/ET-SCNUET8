using UnityEngine;
using UnityEngine.UI;

namespace ET.Client

{
    [EntitySystemOf(typeof (ES_SelfText))]
    [FriendOf(typeof (ES_SelfText))]
    public static partial class ES_SelfTextSystem
    {
        [EntitySystem]
        private static void Awake(this ES_SelfText self, Transform transform)
        {
            self.uiTransform = transform;
            if (self.initWidth == 0)
            {
                Rect textRect = self.E_MsgSymbolText.rectTransform.rect;
                Rect imageTrans = self.E_BgExtendImage.rectTransform.rect;
                self.xOffset = imageTrans.width - textRect.width;
                self.yOffset = imageTrans.height - textRect.height;
                self.initWidth = textRect.width;
            }
        }

        [EntitySystem]
        private static void Destroy(this ES_SelfText self)
        {
            self.DestroyWidget();
        }

        public static Vector2 Refresh(this ES_SelfText self, Scroll_Item_Chat item)
        {
            self.E_MsgSymbolText.text = item.Data.Msg;
            self.E_MsgLongClickButton.OnLongClick.AddListener(self.OnLongClick);

            self.E_MsgSymbolText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, self.E_MsgSymbolText.preferredHeight);
            self.E_BgExtendImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                self.E_MsgSymbolText.preferredHeight + self.yOffset);
            float width = self.E_MsgSymbolText.preferredWidth;
            if (width > self.initWidth)
            {
                self.E_BgExtendImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, self.initWidth + self.xOffset);
            }
            else
            {
                self.E_BgExtendImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                    self.E_MsgSymbolText.preferredWidth + self.xOffset);
            }

            return self.GetSize();
        }

        private static Vector2 GetSize(this ES_SelfText self)
        {
            RectTransform imageTrans = self.E_BgExtendImage.rectTransform;
            float x = (self.uiTransform as RectTransform).sizeDelta.x;
            return new Vector2(x, imageTrans.rect.height + Mathf.Abs(imageTrans.anchoredPosition.y) + ES_SelfText.gap);
        }

        private static void OnLongClick(this ES_SelfText self, bool isOver)
        {
        }
    }
}