using System;
using UnityEngine;
using UnityEngine.UI;

namespace FEA.UI
{
    public class TabBar : MonoBehaviour
    {
        public event Action<int> OnTabChanged;

        [SerializeField] Button[] _tabs;
        [SerializeField] Color _activeColor, _inactiveColor;

        public int ActiveTabIndex { get; private set; } = 0;

        public string NonInteractableMessage { get; set; } = string.Empty;

        private void Start()
        {
            for (int i = 0; i < _tabs.Length; i++)
            {
                int iCopy = i;
                _tabs[i].onClick.AddListener(() => OnTabClicked(iCopy));
                _tabs[i].image.color = _inactiveColor;
            }

            _tabs[ActiveTabIndex].image.color = _activeColor;
        }

        private void OnTabClicked(int clickedIndex)
        {
            if (!string.IsNullOrEmpty(NonInteractableMessage))
            {
                this.PrintMessage(NonInteractableMessage);
                return;
            }

            _tabs[ActiveTabIndex].image.color = _inactiveColor;

            ActiveTabIndex = clickedIndex;

            _tabs[ActiveTabIndex].image.color = _activeColor;

            OnTabChanged?.Invoke(ActiveTabIndex);
        }
    }
}