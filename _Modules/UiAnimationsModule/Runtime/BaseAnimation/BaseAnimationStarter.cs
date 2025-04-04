using UnityEngine;

namespace DivineSkies.Modules.UI
{
    /// <summary>
    /// Attach this script anywhere to create a simple animation in editor
    /// </summary>
    public class BaseAnimationStarter : MonoBehaviour
    {
        [SerializeField] private BaseAnimationData _animationData;

        private void Start()
        {
            _animationData.SetStartValues();
            UiAnimationController.Main.StartNewBaseAnimation(_animationData);
        }
    }
}