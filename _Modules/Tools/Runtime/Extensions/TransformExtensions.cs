using UnityEngine;
using UnityEngine.EventSystems;

namespace DivineSkies.Tools.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// easy access to rectTransform
        /// </summary>
        public static RectTransform GetRectTransform(this UIBehaviour self) => self.transform as RectTransform;

        /// <summary>
        /// Destroys all children of this trandform (reversive if it matters)
        /// </summary>
        public static void DestroyChildren(this Transform parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}