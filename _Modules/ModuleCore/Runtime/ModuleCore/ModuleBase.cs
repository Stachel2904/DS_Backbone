using UnityEngine;
using System.Collections;

namespace DivineSkies.Modules.Core
{
    public abstract class ModuleBase : MonoBehaviour
    {
        /// <summary>
        /// Use this to prioritize the init order. Default is 1. Higher number means earlier initialization
        /// </summary>
        public virtual int InitPriority => 1;

        /// <summary>
        /// Set this in async initialization to show progress on loading screen
        /// </summary>
        public int LoadingProgress { get; protected set; }

        /// <summary>
        /// Use this to override the default loading message key
        /// </summary>
        public virtual string LoadingMessageKey => GetType().ToString();

        /// <summary>
        /// This will be called after module registration. Initialization has not begun yet. If you register submodule now, they will be initialized too.
        /// </summary>
        public virtual void Register() { }

        /// <summary>
        /// Use this for long initialization times to decrease game freezes
        /// </summary>
        public virtual IEnumerator InitializeAsync()
        {
            Initialize();
            yield return null;
        }

        /// <summary>
        /// Will Initialize the Visalization for a SceneModule
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Use this for deinitialization
        /// </summary>
        public virtual void BeforeUnregister() { }

        /// <summary>
        /// Will be called after scene is fully loaded and all modules have finished initialization
        /// </summary>
        public virtual void OnSceneFullyLoaded() { }
    }
}

namespace DivineSkies.Modules
{
    /// <summary>
    /// Use this class as base for each of your modules.
    /// </summary>
    /// <typeparam name="T">Please link your created child module class here</typeparam>
    public abstract class ModuleBase<T> : Core.ModuleBase where T : Core.ModuleBase
    {
        /// <summary>
        /// Easy access to the registered Module
        /// </summary>
        public static T Main => ModuleController.Get<T>();
    }
}