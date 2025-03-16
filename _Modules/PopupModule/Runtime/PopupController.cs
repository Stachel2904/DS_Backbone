using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DivineSkies.Tools.Extensions;
using DivineSkies.Modules.ResourceManagement;
using DivineSkies.Modules.Popups.Internal;
using DivineSkies.Modules.Logging;
using DivineSkies.Modules.ToolTip;

namespace DivineSkies.Modules.Popups
{
    /// <summary>
    /// Use this class to easily access popups
    /// </summary>
    public static class Popup
    {
        public static TPopup Create<TPopup>(bool createNew = false) where TPopup : PopupBase => PopupController.Main.GetPopup<TPopup>(createNew);
    }
}

namespace DivineSkies.Modules.Popups.Internal
{
    public class PopupController : ModuleBase<PopupController>
    {
        private Transform _popupParent;
        private readonly List<PopupBase> _createdPopups = new List<PopupBase>();
        private readonly HashSet<PopupBase> _openPopups = new HashSet<PopupBase>();
        private Transform _faderBackground;

        public override void Initialize()
        {
            _popupParent = new GameObject("PopupParent").AddComponent<GraphicRaycaster>().transform;
            _popupParent.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            _popupParent.gameObject.GetComponent<Canvas>().sortingOrder = 1;
            CanvasScaler scaler = _popupParent.gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.referenceResolution = new Vector2(1920, 1080);
            DontDestroyOnLoad(_popupParent);

            _faderBackground = new GameObject("Fader", typeof(RectTransform)).transform;
            _faderBackground.parent = _popupParent;
            _faderBackground.gameObject.AddComponent<Image>().color = Color.black * 0.5f;
            RectTransform fader = _faderBackground as RectTransform;
            fader.anchorMin = Vector2.zero;
            fader.anchorMax = Vector2.one;
            fader.sizeDelta = Vector2.zero;
            _faderBackground.localPosition = Vector3.zero;
            _faderBackground.localScale = Vector3.one;
            _faderBackground.gameObject.SetActive(false);

            Log.Main.OnScreenMessagePrinted += DisplayLoggedMessage;
        }

        private void DisplayLoggedMessage(string message)
        {
            Popup.Create<NotificationPopup>(true).Init("Information", message);
        }

        public override void OnSceneFullyLoaded()
        {
            ModuleController.GetLoadData<PopupLoadData>()?.OpenPopup();
        }

        internal TPopup GetPopup<TPopup>(bool createNew = false) where TPopup : PopupBase
        {
            if (!createNew && _createdPopups.TryFind(m => m.GetType() == typeof(TPopup) && !m.IsOpen, out PopupBase popup))
            {
                return popup as TPopup;
            }

            popup = ResourceController.Main.LoadAndInstatiatePrefab<TPopup>(_popupParent);
            _createdPopups.Add(popup);
            return (TPopup)popup;
        }

        internal void RegisterOpenPopup(PopupBase popup)
        {
            _faderBackground.gameObject.SetActive(true);
            _faderBackground.SetAsLastSibling();
            popup.transform.SetAsLastSibling();
            _openPopups.Add(popup);
        }

        internal void UnregisterClosedPopup(PopupBase popup)
        {
            popup.transform.SetAsFirstSibling();
            _faderBackground.SetSiblingIndex(_popupParent.childCount - 2);
            _openPopups.Remove(popup);
            if (_openPopups.Count == 0)
            {
                _faderBackground.gameObject.SetActive(false);
            }
        }

        internal ToolTipDisplay CreateToolTipDisplay() => ResourceController.Main.LoadAndInstatiatePrefab<ToolTipDisplay>(_popupParent);
    }
}