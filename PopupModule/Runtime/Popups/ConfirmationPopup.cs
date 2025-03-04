using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DivineSkies.Modules.Popups
{
    public class ConfirmationPopup : NotificationPopup
    {
        [SerializeField] protected Button _btnAccept;
        [SerializeField] protected TextMeshProUGUI _txtDecline, _txtAccept;

        public void Init(string title, string content, Action declined, Action accepted, string declineText = "Decline", string acceptText = "Accept", bool openAfterwards = true)
        {
            _txtDecline.text = declineText;
            _txtAccept.text = acceptText;
            _btnAccept.onClick.RemoveAllListeners();
            _btnAccept.onClick.AddListener(SilentClose);
            _btnAccept.onClick.AddListener(() => accepted());
            Init(title, content, declined, openAfterwards);
        }
    }
}