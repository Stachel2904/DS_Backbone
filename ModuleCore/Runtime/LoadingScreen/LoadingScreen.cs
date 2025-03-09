namespace DivineSkies.Modules.Core
{
    /// <summary>
    /// Use this class to print loading messages and show loadingprogress
    /// </summary>
    public class LoadingScreen : ModuleBase<LoadingScreen>
    {
        private bool _isLoading;

        internal void ProgressLoading(int index, int moduleCount, ModuleBase loadingModule, float progress)
        {
            if (!_isLoading)
            {
                _isLoading = true;
                ShowLoadingScreen();
            }

            OnLoadingProgressed((index + progress) / moduleCount, loadingModule.LoadingMessageKey);
        }

        /// <summary>
        /// This will be called when a new loadingprocess has started
        /// </summary>
        protected virtual void ShowLoadingScreen()
        {

        }


        protected virtual void OnLoadingProgressed(float progress, string messageKey)
        {

        }

        public override void OnSceneFullyLoaded()
        {
            base.OnSceneFullyLoaded();
            OnFinishedLoading();
            _isLoading = false;
        }

        /// <summary>
        /// This will be called when the current loadingProcess has finished
        /// </summary>
        protected virtual void OnFinishedLoading()
        {

        }
    }
}
