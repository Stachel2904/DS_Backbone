using System;
using UnityEngine;
using TMPro;

namespace DivineSkies.Modules.Popups
{
    /// <summary>
    /// Use this Popup to ask the user for input
    /// </summary>
    public class InputPopup : NotificationPopup
    {
        [SerializeField] protected TMP_InputField _input;

        private Action<string> _closeAction;
        private bool _forceInput;
        private string _defaultInput;

        /// <param name="title">The title to display in Popup</param>
        /// <param name="content">The content displayed in Popup</param>
        /// <param name="defaultValue">Default Value of input</param>
        /// <param name="forceInput">The User will be forced to chance the input and enter at least something</param>
        /// <param name="onClose">will be called after the popup was closed (contains input)</param>
        /// <param name="openAfterwards">Should instantly open after init?</param>
        public void Init(string title, string content, string defaultValue, bool forceInput, Action<string> onClose, bool openAfterwards = true)
        {
            _closeAction = onClose;
            _input.text = defaultValue;
            _forceInput = forceInput;
            Init(title, content, openAfterwards);
        }

        protected override void OnClickedDefaultButton()
        {
            if (_forceInput && (string.IsNullOrEmpty(_input.text) || _input.text == _defaultInput))
            {
                this.PrintMessage("Please enter something first");
                return;
            }
            base.OnClickedDefaultButton();
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            _closeAction?.Invoke(_input.text);
        }
    }
}