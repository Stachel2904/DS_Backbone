using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DivineSkies.Modules.Popups
{
    /// <summary>
    /// Use this to display a simple message to inform the user
    /// </summary>
    public class NotificationPopup : PopupBase
    {
        [SerializeField] protected TextMeshProUGUI _txtTitle, _txtContent;
        [SerializeField] protected Button _btnDefault;

        protected bool _isInitialized;

        private Action _onClosed;

        protected override void OnCreation()
        {
            base.OnCreation();

            _btnDefault.onClick.AddListener(OnClickedDefaultButton);
        }

        /// <param name="title">The title to display in Popup</param>
        /// <param name="content">The content displayed in Popup</param>
        /// <param name="openAfterwards">Should instantly open after init?</param>
        public virtual void Init(string title, string content, bool openAfterwards = true, Action onClosed = null)
        {
            _txtTitle.text = title;
            _txtContent.text = content;
            _onClosed = onClosed;

            _isInitialized = true;

            if (openAfterwards)
            {
                Open();
            }
        }

        protected override void OnOpened()
        {
            if (!_isInitialized)
            {
                this.PrintWarning("Please open AFTER calling 'Init()");
            }

            base.OnOpened();
        }

        protected virtual void OnClickedDefaultButton()
        {
            Close();
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            _onClosed?.Invoke();
        }
    }
}