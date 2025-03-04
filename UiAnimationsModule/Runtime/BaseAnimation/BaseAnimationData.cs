using System;
using UnityEngine;

namespace DivineSkies.Modules.UI
{
    public enum InterpolationType { Linear, Bezier }

    [Serializable]
    public class BaseAnimationData
    {
        public RectTransform AnimatedObject;
        public float Duration;
        [HideInInspector] public Vector3 StartTranslation;
        [HideInInspector] public Vector3 StartRotation;
        [HideInInspector] public Vector3 StartScale;
        [HideInInspector] public Vector2 StartSize;
        public Vector3 DeltaTranslation;
        public Vector3 DeltaRotation;
        public Vector3 DeltaScale;
        public Vector2 DeltaSize;
        public bool UseLocalPosition;
        public bool BackAndForth;
        public bool Looping;
        public InterpolationType Interpolation = InterpolationType.Linear;
        [HideInInspector] public Action<BaseAnimationData> OnFinished;

        [HideInInspector] public float Elapsed;

        public BaseAnimationData(){ }

        public BaseAnimationData(RectTransform target, float duration)
        {
            AnimatedObject = target;
            Duration = duration;
            SetStartValues();
        }
        public BaseAnimationData(RectTransform target, float duration, bool useLocalPosition)
        {
            AnimatedObject = target;
            Duration = duration;
            UseLocalPosition = useLocalPosition;
            SetStartValues();
        }

        public void SetStartValues()
        {
            StartTranslation = UseLocalPosition ? AnimatedObject.localPosition : AnimatedObject.anchoredPosition;
            StartRotation = AnimatedObject.localRotation.eulerAngles;
            StartScale = AnimatedObject.localScale;
            StartSize = AnimatedObject.sizeDelta;
        }
    }
}