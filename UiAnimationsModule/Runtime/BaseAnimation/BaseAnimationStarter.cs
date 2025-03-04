using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivineSkies.Modules.UI
{
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