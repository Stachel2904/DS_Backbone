using System;
using UnityEngine;
using TMPro;

namespace DivineSkies.Modules.Popups
{
    public class InputPopup : NotificationPopup
    {
        [SerializeField] protected TMP_InputField _input;

        private Action<string> _closeAction;

        public void Init(string title, string content, string defaultValue, Action<string> onClose, bool openAfterwards = true)
        {
            _closeAction = onClose;
            _input.text = defaultValue;
            Init(title, content, OnClose, openAfterwards);
        }

        private void OnClose()
        {
            _closeAction?.Invoke(_input.text);
        }
    }
}