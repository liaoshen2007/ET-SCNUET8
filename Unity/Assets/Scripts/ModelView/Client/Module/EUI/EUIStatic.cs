using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    public static class EUIStatic
    {
        public static void Normalize(this RectTransform self)
        {
            self.localScale = Vector3.one;
            self.anchorMin = Vector2.zero;
            self.anchorMax = Vector2.one;
            self.anchoredPosition = Vector2.zero;
            self.sizeDelta = Vector2.zero;
        }
        
        public static void SetActive(this Transform self, bool active)
        {
            self.gameObject.SetActive(active);
        }
        
        public static void SetActive(this Graphic self, bool active)
        {
            self.gameObject.SetActive(active);
        }
        
        public static void SetActive(this Component self, bool active)
        {
            self.gameObject.SetActive(active);
        }
    }   
}