using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DivineSkies.Modules.Popups
{
    /// <summary>
    /// Use this popup to let the user enter a number
    /// </summary>
    public class NumberInputPopup : InputPopup
    {
        [SerializeField] private Button _increase, _decrease;
        [SerializeField] private TMP_Text _additionalInfo;

        /// <summary>
        /// In which steps can the user decrease or increase the value?
        /// </summary>
        public int Delta { get; set; } = 1;

        /// <summary>
        /// Use this for extra input validation (like bounds)
        /// </summary>
        public Predicate<int> InputValidation { get; set; }

        /// <summary>
        /// Will be called after Input changed
        /// </summary>
        public event Action<int> OnInputChanged;

        private int _default;

        protected override void OnCreation()
        {
            base.OnCreation();
            _increase.onClick.AddListener(() => ChangeInput(Delta));
            _decrease.onClick.AddListener(() => ChangeInput(-Delta));
            _input.onValueChanged.AddListener(OnInputEntered);
        }

        /// <param name="title">The title to display in Popup</param>
        /// <param name="content">The content displayed in Popup</param>
        /// <param name="defaultValue"></param>
        /// <param name="forceChange">The User will be forced to chance the input and enter at least something</param>
        /// <param name="onClose">the entered input after popup closed</param>
        /// <param name="openAfterwards">Should instantly open after init?</param>
        public void Init(string title, string content, int defaultValue, bool forceChange, Action<int> onClose, bool openAfterwards = true)
        {
            _default = defaultValue;
            
            Init(title, content, _default.ToString(), forceChange, s => onClose?.Invoke(ParseInput(s)), openAfterwards);
            SetAdditionalInfo(string.Empty);
            OnInputChanged?.Invoke(_default);
        }

        private void OnInputEntered(string newInput) => OnInputEntered(ParseInput(newInput));

        private void OnInputEntered(int newInput)
        {
            if (InputValidation != null && !InputValidation(newInput))
            {
                newInput = default;
            }

            _input.text = newInput.ToString();
            OnInputChanged?.Invoke(newInput);
        }

        private int ParseInput(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return _default;
            }

            bool success = int.TryParse(s, out int result);

            if (!success)
            {
                result = _default;
                this.PrintMessage($"{s} is no valid number. Please only enter whole numbers.");
            }

            return result;
        }

        private void ChangeInput(int delta)
        {
            int newValue = ParseInput(_input.text) + delta;
            OnInputEntered(newValue);
        }

        /// <summary>
        /// Use this to explain why <see cref="InputValidation"/> may be false
        /// </summary>
        /// <param name="message"></param>
        public void SetAdditionalInfo(string message)
        {
            _additionalInfo.text = message;
        }
    }
}
