using System;

namespace DivineSkies.Modules.Core
{
    public abstract class SceneLoadData
    {
        internal abstract Type ModuleType { get; }
    }
}

namespace DivineSkies.Modules
{
    /// <summary>
    /// Use sceneloadData to transport data through scenes
    /// </summary>
    public abstract class SceneLoadData<TModule> : Core.SceneLoadData where TModule : Core.ModuleBase
    {
        internal sealed override Type ModuleType => typeof(TModule);
    }
}