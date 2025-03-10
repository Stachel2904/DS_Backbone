using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DivineSkies.Tools.Helper
{
    /// <summary>
    /// USe this class so you dont have to implement unity interfaces
    /// </summary>
    public class DragDropCaller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<PointerEventData> OnDragBegin;
        public event Action<PointerEventData> OnDragUpdate;
        public event Action<PointerEventData> OnDragEnd;

        public void OnBeginDrag(PointerEventData eventData) => OnDragBegin?.Invoke(eventData);

        public void OnDrag(PointerEventData eventData) => OnDragUpdate?.Invoke(eventData);

        public void OnEndDrag(PointerEventData eventData) => OnDragEnd?.Invoke(eventData);
    }
}