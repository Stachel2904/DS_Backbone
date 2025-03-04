using System.Collections.Generic;
using UnityEngine;
using DivineSkies.Tools.Extensions;

namespace DivineSkies.Modules.UI
{
    public class UiAnimationController : ModuleBase<UiAnimationController>
    {
        private readonly List<BaseAnimationData> _runningAnimations = new List<BaseAnimationData>();

        public void StartNewBaseAnimation(BaseAnimationData data)
        {
            if (_runningAnimations.TryFind(a => a.AnimatedObject == data.AnimatedObject, out var previousAnim))
            {
                SkipAnimation(data);
            }

            _runningAnimations.Add(data);
        }

        private void LateUpdate()
        {
            _runningAnimations.ReverseForEach(a => DoAnimation(a));
        }

        private void DoAnimation(BaseAnimationData data)
        {
            data.Elapsed += Time.deltaTime;
            if (data.AnimatedObject == null || (data.Elapsed >= data.Duration && !data.Looping))
            {
                SkipAnimation(data);
                return;
            }
            else if (data.Elapsed >= data.Duration && data.Looping)
            {
                data.Elapsed -= data.Duration;
            }

            float progress = data.Elapsed / data.Duration * (data.BackAndForth ? 2 : 1);
            progress = Mathf.Clamp(progress, 0, data.BackAndForth ? 2 : 1);
            progress = Interpolate(data.Interpolation, progress);

            if(data.UseLocalPosition)
                data.AnimatedObject.localPosition = data.StartTranslation + data.DeltaTranslation * progress;
            else
                data.AnimatedObject.anchoredPosition = data.StartTranslation + data.DeltaTranslation * progress;
            data.AnimatedObject.localRotation = Quaternion.Euler(data.StartRotation + data.DeltaRotation * progress);
            data.AnimatedObject.localScale = data.StartScale + data.DeltaScale * progress;
            data.AnimatedObject.sizeDelta = data.StartSize + data.DeltaSize * progress;
        }

        private float Interpolate(InterpolationType type, float value)
        {
            return type switch
            {
                InterpolationType.Linear => 1f / Mathf.PI * Mathf.Asin(Mathf.Sin(Mathf.PI * (value - 0.5f))) + 0.5f,
                InterpolationType.Bezier => (Mathf.Sin(Mathf.PI * value - Mathf.PI / 2f) + 1) / 2,
                _ => value
            };
        }

        private void SkipAnimation(BaseAnimationData data)
        {
            if(data.AnimatedObject != null)
            {
                if(data.UseLocalPosition)
                    data.AnimatedObject.localPosition = data.StartTranslation + (data.BackAndForth ? Vector3.zero : data.DeltaTranslation);
                else
                    data.AnimatedObject.anchoredPosition = data.StartTranslation + (data.BackAndForth ? Vector3.zero : data.DeltaTranslation);
                data.AnimatedObject.localRotation = Quaternion.Euler(data.StartRotation + (data.BackAndForth ? Vector3.zero : data.DeltaRotation));
                data.AnimatedObject.localScale = data.StartScale + (data.BackAndForth ? Vector3.zero : data.DeltaScale);
                data.AnimatedObject.sizeDelta = data.StartSize + (data.BackAndForth ? Vector2.zero : data.DeltaSize);
            }

            _runningAnimations.Remove(data);

            data.OnFinished?.Invoke(data);
        }
    }
}