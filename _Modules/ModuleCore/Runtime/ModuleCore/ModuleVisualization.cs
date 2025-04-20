using UnityEngine;
using UnityEngine.UI;

namespace DivineSkies.Modules
{
    /// <summary>
    /// Use this class to display Data from <see cref="ISceneModule"/>
    /// </summary>
    public abstract class ModuleVisualization : MonoBehaviour
    {
        [SerializeField] protected Button _closeButton;

        internal void Initialize()
        {
            _closeButton?.onClick.AddListener(OnCloseClicked);
            OnInitialized();
        }

        /// <summary>
        /// Initialize your own Stuff here
        /// </summary>
        protected virtual void OnInitialized()
        {

        }

        /// <summary>
        /// Override this to instead load previous Scene when close button is clicked
        /// </summary>
        protected virtual void OnCloseClicked()
        {
        }
    }
}