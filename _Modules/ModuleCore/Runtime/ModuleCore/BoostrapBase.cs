using System;
using UnityEngine;

namespace DivineSkies.Modules.Core
{
    /// <summary>
    /// Use this class to boot your game. Put this Component in your Start Scene.
    /// </summary>
    /// <typeparam name="TSceneName">The enum for you SceneNames</typeparam>
    /// <typeparam name="TModuleHolder">The class, which holds your module mappings</typeparam>
    public abstract class BootstrapBase<TSceneName, TModuleHolder> : MonoBehaviour where TSceneName : struct, Enum where TModuleHolder : ModuleHolder<TSceneName>, new()
    {
        private void Start()
        {
            if (ModuleController.IsReady)
            {
                return;
            }

            ModuleController controller = ModuleController.Create(new TModuleHolder());

            controller.InitializeConstantModules(OnConstantModulesInitialized);
        }

        private void OnConstantModulesInitialized()
        {
            ModuleController.OnSceneLoaded();
            OnStarted();
        }

        /// <summary>
        /// This will be called after booting was completed
        /// </summary>
        protected virtual void OnStarted()
        {

        }
    }
}