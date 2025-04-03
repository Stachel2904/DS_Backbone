using UnityEngine;

namespace DivineSkies.Modules.Core
{
    public class ExternalSceneLoader : MonoBehaviour
    {
        private void Start()
        {
            ModuleController.OnExternalSceneLoaded();
        }
    }
}
