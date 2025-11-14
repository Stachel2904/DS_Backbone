using System.Collections.Generic;
using UnityEngine;
using DivineSkies.Modules.ResourceManagement;
using System.Linq;

namespace DivineSkies.Modules.Game.TurnBased.Card
{
    public class HandCardVisualization : CardDeckVisualization
    {
        private readonly List<VisualPlayingCard> _cardVisualizations = new List<VisualPlayingCard>();

        public override void Refresh()
        {
            base.Refresh();

            VisualPlayingCard[] oldCards = _cardVisualizations.ToArray();
            foreach (VisualPlayingCard card in oldCards)
            {
                if (!_displayingDeck.Contains(card.CardReference))
                {
                    RemoveHandCardVisualization(card);
                }
            }

            foreach (CardBase card in _displayingDeck)
            {
                if(!_cardVisualizations.Any(v => v.CardReference == card))
                {
                    AddHandCardVisualization(card);
                }
            }

            RearrangeHandCards();
        }

        private void AddHandCardVisualization(CardBase card)
        {
            VisualPlayingCard createdCard = ResourceController.Main.LoadAndInstatiatePrefab<VisualPlayingCard>(transform);
            createdCard.Setup(card);
            _cardVisualizations.Add(createdCard);
        }

        private void RemoveHandCardVisualization(VisualPlayingCard cardToDestroy)
        {
            _cardVisualizations.Remove(cardToDestroy);
            Destroy(cardToDestroy.gameObject);
        }

        protected virtual void RearrangeHandCards()
        {
            int amount = _cardVisualizations.Count;
            int cardDistance = 120;
            float locationValue = -8 * amount + 10;
            float rotationValue = -3 * amount;
            for (int i = 0; i < amount; i++)
            {
                float centeredIndex = (amount == 1) ? 0 : i / (float)(amount - 1) * 2 - 1;
                _cardVisualizations[i].transform.localPosition = new Vector3(centeredIndex * cardDistance * amount / 2, locationValue * Mathf.Pow(centeredIndex, 2), 0);
                _cardVisualizations[i].transform.rotation = Quaternion.Euler(0, 0, rotationValue * centeredIndex);
                _cardVisualizations[i].transform.localScale = Vector3.one;
            }
        }
    }
}