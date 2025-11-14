using DivineSkies.Tools.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DivineSkies.Modules.Game.TurnBased.Card
{
    public class CardDeck<TCard> : List<TCard> where TCard : CardBase
    {
        private CardDeck<TCard> _backUpDeck;

        public CardDeck() { }
        public CardDeck(TCard[] startCards)
        {
            AddRange(startCards);
        }
        public CardDeck(TCard[] startCards, CardDeck<TCard> backUpDeckReference)
        {
            AddRange(startCards);
            _backUpDeck = backUpDeckReference;
        }

        public virtual TCard DrawTopCard()
        {
            if (Count == 0)
            {
                if (_backUpDeck != null)
                {
                    _backUpDeck.Shuffle();
                    AddRange(_backUpDeck);
                    _backUpDeck.Clear();
                }
                else
                {
                    this.PrintError("You tried to draw from an empty deck.");
                    return default;
                }
            }

            TCard result = this.First();
            RemoveAt(0);

            return result;
        }

        public virtual TCard[] DrawTopCards(int amount)
        {
            TCard[] result = new TCard[amount];

            for (int i = 0; i < amount; i++)
                result[i] = DrawTopCard();

            return result;
        }
    }
}
