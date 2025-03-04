using UnityEngine;
using UnityEngine.UI;

namespace DivineSkies.Tools.Helper
{
    public class ParentContentSizeFitter : ContentSizeFitter
    {
        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();

            ((RectTransform)transform.parent).sizeDelta = ((RectTransform)transform).sizeDelta + Vector2.one * 5 * 2;
        }
    }
}