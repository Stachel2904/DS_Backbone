using System;
using UnityEngine;

namespace DivineSkies.Modules.UI
{
    public enum InterpolationType { Linear, Bezier }

    /// <summary>
    /// Use this Data-Class to customize your animation values
    /// </summary>
    [Serializable]
    public class BaseAnimationData
    {
        /// <summary>
        /// Set the Rect Transform you want to animate
        /// </summary>
        public RectTransform AnimatedObject;

        /// <summary>
        /// Duration of Animation
        /// </summary>
        public float Duration;

        /// <summary>
        /// Delta Translation, no anim if set to default
        /// </summary>
        public Vector3 DeltaTranslation;

        /// <summary>
        /// Delta Rotation, no anim if set to default
        /// </summary>
        public Vector3 DeltaRotation;

        /// <summary>
        /// Delta Scale, no anim if set to default
        /// </summary>
        public Vector3 DeltaScale;

        /// <summary>
        /// Delta Size, no anim if set to default
        /// </summary>
        public Vector2 DeltaSize;

        /// <summary>
        /// Should use local position? Will use anchored otherwise
        /// </summary>
        public bool UseLocalPosition;

        /// <summary>
        /// Will animate back to origin (Duration will only determine one way)
        /// </summary>
        public bool BackAndForth;

        /// <summary>
        /// Set to true if you want to loop the animation
        /// </summary>
        public bool Looping;

        /// <summary>
        /// Interpolation: Linear ist default, Bezier is more like a parabel
        /// </summary>
        public InterpolationType Interpolation = InterpolationType.Linear;

        /// <summary>
        /// Will be called after animation ended (will never be called if set to looping)
        /// </summary>
        [HideInInspector] public Action<BaseAnimationData> OnFinished;

        internal float Elapsed;
        internal Vector3 StartTranslation;
        internal Vector3 StartRotation;
        internal Vector3 StartScale;
        internal Vector2 StartSize;

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

        internal void SetStartValues()
        {
            StartTranslation = UseLocalPosition ? AnimatedObject.localPosition : AnimatedObject.anchoredPosition;
            StartRotation = AnimatedObject.localRotation.eulerAngles;
            StartScale = AnimatedObject.localScale;
            StartSize = AnimatedObject.sizeDelta;
        }
    }
}