using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET
{
    [RequireComponent(typeof(RectTransform))]
    public class DragableUIElement : MonoBehaviour, 
        IInitializePotentialDragHandler, 
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler
    {
        private List<Selectable> cache = new List<Selectable>();

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            GetComponentsInChildren(cache);
            cache.RemoveAll(x => !x.interactable);
            cache.ForEach(x => x.interactable = false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                (RectTransform) transform.parent, 
                eventData.position,
                Camera.main))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    (RectTransform) transform.parent
                    , eventData.position
                    , Camera.main
                    , out var localPos);
                transform.localPosition = localPos;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            cache.ForEach(x => x.interactable = true);
        }
    }
}