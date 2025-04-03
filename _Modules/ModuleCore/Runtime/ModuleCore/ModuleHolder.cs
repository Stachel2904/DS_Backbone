using System;
using System.Linq;
using System.Collections.Generic;

namespace DivineSkies.Modules.Core
{
    public abstract class ModuleHolderBase
    {
        internal abstract Type[] GetConstantModuleTypes();

        internal abstract Type[] GetSceneModuleTypes(string scene);
    }

    /// <summary>
    /// USe this class to map your modules needed in your game.
    /// </summary>
    public abstract class ModuleHolder<TSceneName> : ModuleHolderBase where TSceneName : struct, Enum
    {
        /// <summary>
        /// Create an Array of all Modules that you always need in your game.
        /// </summary>
        protected abstract Type[] ConstantModules { get; }
        private Type[] _constantModules;

        internal sealed override Type[] GetConstantModuleTypes()
        {
            if (_constantModules != null)
            {
                return _constantModules;
            }

            List<Type> constantModules = new List<Type>();
            constantModules.Add(typeof(Logging.Log));
            constantModules.AddRange(ConstantModules);
            _constantModules = constantModules.Distinct().ToArray();

            return _constantModules;
        }

        internal sealed override Type[] GetSceneModuleTypes(string scene)
        {
            if (!Enum.TryParse(scene, out TSceneName sceneName))
            {
                this.PrintError("Failed to find Scene named " + scene);
                return Array.Empty<Type>();
            }

            return GetSceneModuleTypes(sceneName);
        }

        /// <summary>
        /// Return the modules you want to load in each scene
        /// </summary>
        protected abstract Type[] GetSceneModuleTypes(TSceneName scene);
    }
}