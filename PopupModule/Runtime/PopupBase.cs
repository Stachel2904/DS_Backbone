using UnityEngine;
using UnityEngine.UI;
using DivineSkies.Modules.ToolTip;
using DivineSkies.Modules.UI;

namespace DivineSkies.Modules.Popups
{
    /// <summary>
    /// Use this as base for your popups. It comes with fade-in and fade-out and an overlay to hide the background.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PopupBase : MonoBehaviour
    {        
        [SerializeField] protected Button _closeButton;

        public bool IsOpen => _fader.IsShowing;

        private ShowHideFader _fader;

        /// <summary>
        /// override this property to change the fade in/fade out transition speed
        /// </summary>
        protected virtual float FadeSpeed => 5f;

        private void Awake()
        {
            if (_closeButton == null)
            {
                this.PrintWarning("You forgot to set a Close Button, this is neccessary!");
            }
            else
            {
                _closeButton.onClick.AddListener(Close);
            }

            _fader = new ShowHideFader(this, FadeSpeed);
            OnCreation();
        }

        /// <summary>
        /// This will be called directly after Awake
        /// </summary>
        protected virtual void OnCreation() { }

        /// <summary>
        /// Call this to open the popup
        /// </summary>
        public void Open()
        {
            _fader.Toggle(true);
            Internal.PopupController.Main.RegisterOpenPopup(this);
            OnOpened();
        }

        /// <summary>
        /// This will be called after the popup was opened
        /// </summary>
        protected virtual void OnOpened()
        {

        }

        /// <summary>
        /// Call this to close the popup
        /// </summary>
        public void Close()
        {
            _fader.Toggle(false);
            Internal.PopupController.Main.UnregisterClosedPopup(this);

            if (ModuleController.Has<ToolTipController>())
            {
                ToolTipController.Main.HideActiveTooltip();
            }

            OnClosed();
        }

        /// <summary>
        /// This will be called after the popup was closed
        /// </summary>
        protected virtual void OnClosed()
        {

        }
    }
}
