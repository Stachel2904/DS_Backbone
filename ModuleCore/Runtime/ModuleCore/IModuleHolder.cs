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

    public abstract class ModuleHolder<TSceneName> : ModuleHolderBase where TSceneName : struct, Enum
    {
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
            if(!Enum.TryParse(scene, out TSceneName sceneName))
            {
                this.PrintError("Failed to find Scene named " + scene);
                return Array.Empty<Type>();
            }

            return GetSceneModuleTypes(sceneName);
        }

        protected abstract Type[] GetSceneModuleTypes(TSceneName scene);
    }
}
