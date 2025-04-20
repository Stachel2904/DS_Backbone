using UnityEngine;

namespace DivineSkies.Modules.Core
{
    public class ExternalSceneLoader : MonoBehaviour
    {
        private void Awake()
        {
            //will be initialized from bootstrap
            if (!ModuleController.IsReady)
            {
                return;
            }

            ModuleController.OnSceneLoaded();
        }
    }
}
