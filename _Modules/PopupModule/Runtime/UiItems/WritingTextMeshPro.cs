using System;
using System.Collections;
using UnityEngine;
using TMPro;

namespace DivineSkies.Modules.UI
{
    /// <summary>
    /// This is a text that will be filled letter by letter
    /// </summary>
    public class WritingTextMeshPro : TextMeshProUGUI
    {
        public static float DEFAULT_DELAY = 0.03f;
        private bool _skipToEnd = false;

        private Coroutine _runningWriting;
        private float _delayTimer;
        private Action _callback;
        private bool _autoFinish;

        /// <summary>
        /// Calling this will instantly display text, skipping the letter-byletter animation
        /// </summary>
        public void EndWriting()
        {
            if(_runningWriting != null)
            {
                _skipToEnd = true;
            }
            else
            {
                Finish();
            }
        }

        /// <summary>
        /// Starts writing a message letter-by-letter
        /// </summary>
        /// <param name="content">The Message to display</param>
        /// <param name="delay"> if set < 0 <see cref="DEFAULT_DELAY"/> will be used</param>
        /// <param name="writingFinishCallback">Will be called when writing is finished</param>
        /// <param name="autoFinish">Should automatically finish or wait for call of <see cref="EndWriting"/></param>
        public void StartWriting(string content, float delay = -1, Action writingFinishCallback = null, bool autoFinish = true)
        {
            _autoFinish = autoFinish;

            if (delay < 0)
                delay = DEFAULT_DELAY;

            if (_runningWriting != null)
            {
                StopCoroutine(_runningWriting);
                _runningWriting = null;
            }

            _callback = writingFinishCallback;

            if (delay == 0)
            {
                text = content;
                _callback?.Invoke();
                return;
            }

            _runningWriting = StartCoroutine(WriteContent(content, delay));
        }

        private IEnumerator WriteContent(string content, float delay)
        {
            text = "";

            for (int i = 0; i < content.Length; i++)
            {
                _delayTimer += delay;
                while(_delayTimer > 0 && !_skipToEnd)
                {
                    _delayTimer -= Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                if (_skipToEnd)
                {
                    break;
                }

                text += content[i];
            }

            text = content;
            _skipToEnd = false;
            _runningWriting = null;

            if (_autoFinish)
            {
                Finish();
            }
        }

        private void Finish()
        {
            _callback?.Invoke();
        }
    }
}