namespace DivineSkies.Modules.Game.TurnBased
{
    public interface ITurnBaseGameProcessor : IGameProcessor
    {
        void OnNextTurn();
    }
}
