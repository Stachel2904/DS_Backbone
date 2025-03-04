using UnityEngine;
using UnityEngine.EventSystems;

namespace DivineSkies.Tools.Extensions
{
    public static class TransformExtensions
    {
        public static RectTransform GetRectTransform(this UIBehaviour self) => self.transform as RectTransform;

        public static void DestroyChildren(this Transform parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}