using System;
using UnityEngine;

namespace DivineSkies.Modules.Core
{
    public abstract class BootstrapBase<TSceneName, TModuleHolder> : MonoBehaviour where TSceneName : struct, Enum where TModuleHolder : ModuleHolder<TSceneName>, new()
    {
        protected virtual TSceneName? StartScene => null;

        private void Start()
        {
            ModuleController controller = ModuleController.Create(new TModuleHolder());

            if (StartScene.HasValue)
            {
                controller.SetDefaultScene(StartScene.ToString());
            }

            controller.InitializeConstantModules(OnConstantModulesInitialized);
        }

        private void OnConstantModulesInitialized()
        {
            if (StartScene == null)
            {
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

        protected virtual void OnStarted()
        {

        }
    }
}