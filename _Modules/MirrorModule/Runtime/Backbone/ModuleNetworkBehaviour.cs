using DivineSkies.Modules;
using DivineSkies.Modules.Core;
using Mirror;

namespace DivineSkies.MirrorModule
{
    public class ModuleNetworkBehaviour<TNetworkModule> : NetworkBehaviour where TNetworkModule : ModuleBase
    {
        public TNetworkModule ParentModule { get; private set; }

        public NetworkIdentity Identity { get; private set; }

        internal void Initialize()
        {
            ParentModule = ModuleController.Get<TNetworkModule>();

            if (ParentModule.gameObject.TryGetComponent(out NetworkIdentity comp))
            {
                Identity = comp;
            }
            else
            {
                Identity = ParentModule.gameObject.AddComponent<NetworkIdentity>();
            }
        }
    }
}
