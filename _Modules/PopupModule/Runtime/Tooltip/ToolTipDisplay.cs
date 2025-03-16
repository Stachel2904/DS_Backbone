using UnityEngine;
using TMPro;
using DivineSkies.Tools.Helper;
using DivineSkies.Modules.UI;

namespace DivineSkies.Modules.ToolTip
{
    internal class ToolTipDisplay : UiItemBase
    {
        private const int MaxLettersPerLine = 50;
        [SerializeField] private TextMeshProUGUI _toolTipText;

        internal void SetText(string text)
        {
            _toolTipText.text = UiUtils.WrapText(text, MaxLettersPerLine);
        }
    }
}