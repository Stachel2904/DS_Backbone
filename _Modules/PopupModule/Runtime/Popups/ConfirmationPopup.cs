using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DivineSkies.Modules.Popups
{
    /// <summary>
    /// Use this popup to let the user decide
    /// </summary>
    public class ConfirmationPopup : NotificationPopup
    {
        [SerializeField] protected Button _btnDecline, _btnAccept;
        [SerializeField] protected TextMeshProUGUI _txtDecline, _txtAccept;

        public void Init(string title, string content, Action declined, Action accepted, bool forceDecision, string declineText = "Decline", string acceptText = "Accept", bool openAfterwards = true, Action onClosed = null)
        {
            _closeButton.gameObject.SetActive(!forceDecision);

            _txtDecline.text = declineText;
            _btnDecline.onClick.RemoveAllListeners();
            _btnDecline.onClick.AddListener(Close);
            _btnDecline.onClick.AddListener(() => declined?.Invoke());

            _txtAccept.text = acceptText;
            _btnAccept.onClick.RemoveAllListeners();
            _btnAccept.onClick.AddListener(Close);
            _btnAccept.onClick.AddListener(() => accepted?.Invoke());

            Init(title, content, openAfterwards, onClosed);
        }
    }
}