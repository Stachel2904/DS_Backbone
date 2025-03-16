using System;
using System.Collections;
using UnityEngine;

namespace DivineSkies.Tools.Extensions
{
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// You can start a tick routine that will call the callback each tick
        /// </summary>
        /// <param name="frameTick">will give you delta time to alter progress, return x2 to call callback every 0.5s</param>
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

        /// <summary>
        /// Stop all coroutine if routine is not set
        /// </summary>
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