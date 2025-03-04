using System;
using System.Collections;
using UnityEngine;

namespace DivineSkies.Tools.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static Coroutine StartTickRoutine(this MonoBehaviour parent, Func<float, float> frameTick, Action callback)
        {
            return parent.StartCoroutine(TickRoutine(frameTick, callback));
        }

        private static IEnumerator TickRoutine(Func<float, float> frameTick, Action callback)
        {
            float progress = 0;
            while (progress < 1)
            {
                yield return new WaitForEndOfFrame();
                progress += frameTick?.Invoke(Time.deltaTime) ?? Time.deltaTime;
            }
            callback?.Invoke();
        }

        public static void StopTickRoutine(this MonoBehaviour parent, ref Coroutine routine)
        {
            if (routine == null)
            {
                parent.StopAllCoroutines();
                return;
            }

            parent.StopCoroutine(routine);

            routine = null;
        }
    }
}