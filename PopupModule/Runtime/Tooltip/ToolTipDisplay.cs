using UnityEngine;
using TMPro;
using FEA.UI;
using DivineSkies.Tools.Helper;

namespace DivineSkies.Modules.ToolTip
{
    public class ToolTipDisplay : UiItemBase
    {
        private const int MaxLettersPerLine = 50;
        [SerializeField] private TextMeshProUGUI _toolTipText;

        public void SetText(string text)
        {
            _toolTipText.text = UiUtils.WrapText(text, MaxLettersPerLine);
        }
    }
}