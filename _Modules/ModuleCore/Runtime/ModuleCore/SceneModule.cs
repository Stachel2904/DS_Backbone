namespace DivineSkies.Modules
{
    /// <summary>
    /// Derive from this interface if your Module plans to display Data
    /// </summary>
    public interface ISceneModule
    {
        /// <summary>
        /// A getter to your created Visualization. Will be Initialized automatically
        /// </summary>
        public ModuleVisualization Visualization { get; }
    }
}