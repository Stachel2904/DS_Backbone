using DivineSkies.Modules;
using DivineSkies.Modules.Core;

namespace DivineSkies.MirrorModule
{
    public abstract class NetworkModuleBase<TNetworkBehaviour, TModule> : ModuleBase<TModule> where TModule : ModuleBase where TNetworkBehaviour : ModuleNetworkBehaviour<TModule>
    {
        public TNetworkBehaviour NetworkBehaviour { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            if (gameObject.TryGetComponent(out TNetworkBehaviour comp))
            {
                NetworkBehaviour = comp;
            }
            else
            {
                NetworkBehaviour = gameObject.AddComponent<TNetworkBehaviour>();
            } 

            NetworkBehaviour.Initialize();
        }
    }
}
