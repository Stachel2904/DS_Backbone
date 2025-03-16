using System;
using UnityEngine;
using UnityEngine.UI;

namespace DivineSkies.Modules.UI
{
    /// <summary>
    /// Use this to implement a TabBar
    /// </summary>
    public class TabBar : MonoBehaviour
    {
        /// <summary>
        /// Will be called after the tab was changed
        /// </summary>
        public event Action<int> OnTabChanged;

        [SerializeField] private Button[] _tabs;
        [SerializeField] private Color _activeColor, _inactiveColor;

        /// <summary>
        /// Current active index
        /// </summary>
        public int ActiveTabIndex { get; private set; } = 0;

        /// <summary>
        /// Set this to display Message in case tab should not be clickable
        /// </summary>
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