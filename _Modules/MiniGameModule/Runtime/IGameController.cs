namespace DivineSkies.Modules.Game
{
    public interface IGameController
    {
        IGameProcessor Processor { get; }
        IGameVisualization Visualization { get; }
    }
}
