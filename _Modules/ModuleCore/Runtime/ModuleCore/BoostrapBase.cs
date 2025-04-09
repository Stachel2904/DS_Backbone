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
        [Tooltip("Enable this to start game from this scene")]
        [SerializeField] private bool _isDebugStart;

        /// <summary>
        /// This scene will determine which scene will be loaded on <see cref="ModuleController.LoadDefaultScene"/>. Will also load this scene after booting if set.
        /// </summary>
        protected virtual TSceneName? StartScene => null;

        private void Start()
        {
            if (ModuleController.IsReady)
            {
                return;
            }

            ModuleController controller = ModuleController.Create(new TModuleHolder());

            if (StartScene.HasValue)
            {
                controller.SetDefaultScene(StartScene.ToString());
            }

            controller.InitializeConstantModules(OnConstantModulesInitialized);
        }

        private void OnConstantModulesInitialized()
        {
            if (StartScene == null || _isDebugStart)
            {
                ModuleController.OnExternalSceneLoaded();
                OnStarted();
            }
            else
            {
                ModuleController.OnSceneChanged += OnAfterDefaultSceneLoaded;
                ModuleController.LoadDefaultScene();
            }
        }

        private void OnAfterDefaultSceneLoaded()
        {
            ModuleController.OnSceneChanged -= OnAfterDefaultSceneLoaded;
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