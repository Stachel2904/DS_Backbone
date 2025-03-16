namespace DivineSkies.Modules.Popups
{
    public class PopupLoadData<TPopup> : Internal.PopupLoadData where TPopup : PopupBase
    {
        public override void OpenPopup() => Popup.Create<TPopup>().Open();
    }
}

namespace DivineSkies.Modules.Popups.Internal
{
    public abstract class PopupLoadData : SceneLoadData<PopupController>
    {
        public abstract void OpenPopup();
    }
}