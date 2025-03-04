using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DivineSkies.Modules.Popups
{
    public class NotificationPopup : PopupBase
    {
        [SerializeField] protected TextMeshProUGUI _txtTitle, _txtContent;
        [SerializeField] protected Button _btnDefault;

        protected Action _onClose;
        protected bool _isInitialized;

        protected override void Awake()
        {
            base.Awake();

            _btnDefault.onClick.AddListener(Close);
        }

        public virtual void Init(string title, string content, Action onClosed = null, bool openAfterwards = true)
        {
            _onClose = onClosed;

            _txtTitle.text = title;
            _txtContent.text = content;

            _isInitialized = true;

            if (openAfterwards)
                Open();
        }

        public override void Open()
        {
            if (!_isInitialized)
            {
                this.PrintWarning("Please open AFTER calling 'Init()");
                return;
            }

            base.Open();
        }

        public void SilentClose()
        {
            base.Close();
        }

        public override void Close()
        {
            base.Close();

            _onClose?.Invoke();
        }
    }
}