using UnityEngine;

namespace DivineSkies.Modules.Core
{
    /// <summary>
    /// Attach this script to a gameObject in a Scene to load all module components on this game object on scene start.
    /// </summary>
    public class SceneModuleLoader : MonoBehaviour
    {
        internal ModuleBase[] SceneModules => gameObject.GetComponents<ModuleBase>();
    }
}