using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DivineSkies.Modules.ToolTip
{
    public class ToolTipController : ModuleBase<ToolTipController>
    {
        private const float SHOWDELAY = .5f;
        private ToolTipProvider _currentToolTipProvider;
        private ToolTipDisplay _display;
        private string _currentKey;

        private float _currentShowDelayProgress;

        public override void Initialize()
        {
            _display = GetTooltipDisplay();
            _display.gameObject.SetActive(false);
        }

        protected virtual ToolTipDisplay GetTooltipDisplay()
        {
            return new GameObject("DefaultTooltip").AddComponent<ToolTipDisplay>();
        }

        public void OnToolTipProviderHoverEnter(ToolTipProvider target)
        {
            _currentToolTipProvider = target;
        }

        public void OnToolTipProviderHoverExit(ToolTipProvider target)
        {
            if (_currentToolTipProvider == target)
                HideToolTip();
        }

        private void ShowToolTip(TextMeshProUGUI text)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (linkIndex < 0)
                return;
            string linkID = text.textInfo.linkInfo[linkIndex].GetLinkID();
            ShowToolTip(linkID, false);
        }

        private void ShowToolTip(Image image)
        {
            ShowToolTip(image.sprite.name, false);
        }

        protected virtual string TranslateKey(string key)
        {
            return "404: missing translation for " + key;
        }

        private void ShowToolTip(string key, bool printKey)
        {
            RectTransform toolTip = _display.transform as RectTransform;
            if(key != _currentKey)
            {
                _currentKey = key;
                _display.SetText(printKey ? key : TranslateKey(key));
                _display.gameObject.SetActive(true);
            }

            var factor = 1 / _display.transform.parent.lossyScale.x;
            var x = Mathf.Clamp(Input.mousePosition.x * factor, 0, Screen.width * factor - toolTip.rect.width);
            factor = 1 / _display.transform.parent.lossyScale.y;
            var y = Mathf.Clamp(Input.mousePosition.y * factor, 0, Screen.height * factor - toolTip.rect.height);
            toolTip.anchoredPosition = new Vector2(x, y);
        }

        public void HideActiveTooltip()
        {
            if (_currentToolTipProvider != null)
                HideToolTip();
        }

        private void HideToolTip()
        {
            _display.gameObject?.SetActive(false);
            _currentToolTipProvider = null;
            _currentKey = null;
            _currentShowDelayProgress = 0;
        }

        private void Update()
        {
            if (_currentToolTipProvider == null)
                return;

            if(_currentShowDelayProgress < SHOWDELAY)
            {
                _currentShowDelayProgress += Time.deltaTime;
                return;
            }

            if (_currentToolTipProvider.KeyOverride != string.Empty)
                ShowToolTip(_currentToolTipProvider.KeyOverride, _currentToolTipProvider.UseKeyAsTooltip);
            else if (_currentToolTipProvider.TryGetComponent(out TextMeshProUGUI text))
                ShowToolTip(text);
            else if (_currentToolTipProvider.TryGetComponent(out Image image))
                ShowToolTip(image);
            else
                return;
        }
    }
}