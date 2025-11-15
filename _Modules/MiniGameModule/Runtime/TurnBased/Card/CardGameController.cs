using DivineSkies.Tools.Extensions;

namespace DivineSkies.Modules.Game.TurnBased.Card
{
    public abstract class CardGameController<TModule, TProcessor, TVisualization, TCard> : TurnBaseGameController<TModule, TProcessor, TVisualization> where TModule : Core.ModuleBase where TProcessor : ITurnBaseGameProcessor where TVisualization : ICardGameVisualization where TCard : CardBase
    {
        protected override bool AutoStart => false;

        protected CardDeck<TCard> _drawDeck;
        protected CardDeck<TCard> _handCards;
        protected CardDeck<TCard> _discardDeck;

        public override void Initialize()
        {
            base.Initialize();
            _discardDeck = new CardDeck<TCard>();
            _handCards = new CardDeck<TCard>();
            _drawDeck = new CardDeck<TCard>(GetDeckCards(), _discardDeck);
            _drawDeck.Shuffle();
        }

        public override void OnSceneFullyLoaded()
        {
            base.OnSceneFullyLoaded();

            Visualization.DiscardDeck.SetDeck(_discardDeck);
            Visualization.HandCards.SetDeck(_handCards);
            Visualization.DrawDeck.SetDeck(_drawDeck);

            StartGame();
        }

        protected abstract TCard[] GetDeckCards();

        public void DrawCards(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                TCard card = _drawDeck.DrawTopCard();
                _handCards.Add(card);
                card.OnDraw();
            }
            Visualization.HandCards.Refresh();
            Visualization.DrawDeck.Refresh();
        }

        public void PlayCard(TCard card)
        {
            card.OnPlay();
        }

        public void DiscardHandCard(TCard card)
        {
            card.OnDiscard();

            _discardDeck.Add(card);
            Visualization.DiscardDeck.Refresh();

            _handCards.Remove(card);
            Visualization.HandCards.Refresh();
        }
    }
}