using System;
using UnityEngine;
using DivineSkies.Modules.UI;

namespace DivineSkies.UiAnimations
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Animated a anchored move on this trandform
        /// </summary>
        public static BaseAnimationData MoveAnchored(this Transform self, float duration, Vector3? translationDelta = null, Vector3? rotationDelta = null, Vector3? scaleDelta = null, Vector2? sizeDelta = null, Action<BaseAnimationData> onFinished = null, bool backAndForth = false)
        {
            if (!(self is RectTransform rectSelf))
            {
                return null;
            }

            BaseAnimationData result = new BaseAnimationData(rectSelf, duration);
            result.DeltaTranslation = translationDelta ?? Vector3.zero;
            result.DeltaRotation = rotationDelta ?? Vector3.zero;
            result.DeltaScale = scaleDelta ?? Vector3.zero;
            result.DeltaSize = sizeDelta ?? Vector2.zero;
            result.OnFinished = onFinished;
            result.BackAndForth = backAndForth;

            UiAnimationController.Main.StartNewBaseAnimation(result);

            return result;
        }

        /// <summary>
        /// Animated a local move on this trandform
        /// </summary>
        public static BaseAnimationData MoveLocal(this Transform self, float duration, Vector3? translationDelta = null, Vector3? rotationDelta = null, Vector3? scaleDelta = null, Vector2? sizeDelta = null, Action<BaseAnimationData> onFinished = null, bool backAndForth = false)
        {
            if (!(self is RectTransform rectSelf))
                return null;

            BaseAnimationData result = new BaseAnimationData(rectSelf, duration, true);
            result.DeltaTranslation = translationDelta ?? Vector3.zero;
            result.DeltaRotation = rotationDelta ?? Vector3.zero;
            result.DeltaScale = scaleDelta ?? Vector3.zero;
            result.DeltaSize = sizeDelta ?? Vector2.zero;
            result.OnFinished = onFinished;
            result.BackAndForth = backAndForth;

            UiAnimationController.Main.StartNewBaseAnimation(result);

            return result;
        }
    }
}