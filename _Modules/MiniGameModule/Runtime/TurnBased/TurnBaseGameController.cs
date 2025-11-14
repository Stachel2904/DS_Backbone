namespace DivineSkies.Modules.Game.TurnBased
{
    public abstract class TurnBaseGameController<TModule, TProcessor, TVisualization> : GameController<TModule, TProcessor, TVisualization> where TModule : Core.ModuleBase where TProcessor : ITurnBaseGameProcessor where TVisualization : IGameVisualization
    {
        public void NextTurn()
        {
            Processor.OnNextTurn();
        }
    }
}