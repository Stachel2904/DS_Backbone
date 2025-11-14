namespace DivineSkies.Modules.Game.TurnBased.Card
{
    public interface ICardGameVisualization : IGameVisualization
    {
        HandCardVisualization HandCards { get; }
        CardDeckVisualization DrawDeck { get; }
        CardDeckVisualization DiscardDeck { get; }
    }
}