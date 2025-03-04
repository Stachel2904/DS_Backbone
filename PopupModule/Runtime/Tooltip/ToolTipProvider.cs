using UnityEngine;
using UnityEngine.EventSystems;

namespace DivineSkies.Modules.ToolTip
{
    public class ToolTipProvider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string KeyOverride;
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