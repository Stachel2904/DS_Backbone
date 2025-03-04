using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DivineSkies.Modules.Popups
{
    public class NumberInputPopup : InputPopup
    {
        [SerializeField] private Button _increase, _decrease;
        [SerializeField] private TMP_Text _additionalInfo;

        public int Delta { get; set; } = 1;
        public Predicate<int> InputValidation { get; set; }
        public event Action<int> OnInputChanged;

        private int _default;

        protected override void Awake()
        {
            base.Awake();
            _increase.onClick.AddListener(() => ChangeInput(Delta));
            _decrease.onClick.AddListener(() => ChangeInput(-Delta));
            _input.onValueChanged.AddListener(OnInputEntered);
        }

        public void Init(string title, string content, int defaultValue, Action<int> onClose, bool openAfterwards = true)
        {
            _default = defaultValue;
            Init(title, content, _default.ToString(), s => onClose?.Invoke(ParseInput(s)), openAfterwards);
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
                this.PrintError($"{s} is no valid number. Please only enter whole numbers.");
            }

            return result;
        }

        private void ChangeInput(int delta)
        {
            int newValue = ParseInput(_input.text) + delta;
            OnInputEntered(newValue);
        }

        public void SetAdditionalInfo(string message)
        {
            _additionalInfo.text = message;
        }
    }
}
