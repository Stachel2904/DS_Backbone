using DivineSkies.Modules.Logging;
using System.Collections;
using UnityEngine;

namespace DivineSkies.Modules.UI
{
    public class ShowHideFader
    {
        public bool IsShowing { get; private set; }

        private readonly MonoBehaviour _parent;
        private readonly CanvasGroup _target;
        private readonly float _fadeSpeed;
        private readonly bool _stayActive;

        private Coroutine _currentFadeRoutine;

        public ShowHideFader(MonoBehaviour parent, float speed, CanvasGroup target = null, bool stayActive = false)
        {
            _parent = parent;
            _fadeSpeed = speed;

            _target ??= parent.GetComponent<CanvasGroup>();
            if (_target == null)
                parent.PrintError("Fader couldn't find Canvas Group. Please set it manually instead.");

            _stayActive = stayActive;
        }

        public void Toggle(bool show)
        {
            if (IsShowing == show)
                return;

            IsShowing = show;

            if(IsShowing && !_stayActive)
                _parent.gameObject.SetActive(true);

            if (_currentFadeRoutine == null)
                _currentFadeRoutine = _parent.StartCoroutine(FadeRoutine());
        }

        private IEnumerator FadeRoutine()
        {
            _target.alpha = IsShowing ? 0 : 1;
            while (IsShowing ? _target.alpha < 1 : _target.alpha > 0)
            {
                yield return new WaitForEndOfFrame();
                _target.alpha += _fadeSpeed * (IsShowing ? Time.deltaTime : -Time.deltaTime);
            }

            _target.alpha = Mathf.Clamp01(_target.alpha);

            if (!IsShowing && !_stayActive)
                _parent.gameObject.SetActive(false);

            _currentFadeRoutine = null;
        }
    }
}