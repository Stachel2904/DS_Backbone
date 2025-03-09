using UnityEngine;
using UnityEngine.EventSystems;

namespace DivineSkies.Modules.ToolTip
{
    /// <summary>
    /// Attach this component to any Raycast-able UI Element to display a Tooltip
    /// </summary>
    public class ToolTipProvider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// Set this to override the default Key gained from Text or Image Component
        /// </summary>
        public string KeyOverride;

        /// <summary>
        /// You may use this if don't want any translation on the tooltip (e.g. for Details on hover)
        /// </summary>
        public bool UseKeyAsTooltip;

        public void OnPointerEnter(PointerEventData eventData)
        {
            ToolTipController.Main.OnToolTipProviderHoverEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToolTipController.Main.OnToolTipProviderHoverExit(this);
        }
    }
}