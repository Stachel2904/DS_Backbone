using UnityEngine;
using UnityEngine.UI;
using DivineSkies.Modules.ToolTip;
using DivineSkies.Modules.UI;

namespace DivineSkies.Modules.Popups
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PopupBase : MonoBehaviour
    {        
        [SerializeField] private Button _closeButton;

        public bool IsOpen => _fader.IsShowing;

        private ShowHideFader _fader;
        protected virtual float FadeSpeed => 5f;

        protected virtual void Awake()
        {
            if (_closeButton == null)
                this.PrintWarning("You forgot to set a Close Button, this is neccessary!");
            else
                _closeButton.onClick.AddListener(Close);

            _fader = new ShowHideFader(this, FadeSpeed);
        }


        public virtual void Open()
        {
            _fader.Toggle(true);
            Internal.PopupController.Main.RegisterOpenPopup(this);
            OnOpened();
        }

        protected virtual void OnOpened()
        {

        }

        public virtual void Close()
        {
            _fader.Toggle(false);
            Internal.PopupController.Main.UnregisterClosedPopup(this);
            
            ToolTipController.Main.HideActiveTooltip();
        }
    }
}
