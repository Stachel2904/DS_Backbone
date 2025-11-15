using DivineSkies.Tools.Extensions;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DivineSkies.Modules.Game.TurnBased.Card
{
    public class CardDeckVisualization : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _amountTxt;

        protected IEnumerable<CardBase> _displayingDeck;

        public virtual void SetDeck(IEnumerable<CardBase> deck)
        {
            _displayingDeck = deck;
            Refresh();
        }

        public virtual void Refresh()
        {
            if(_amountTxt != null)
            {
                int amount = _displayingDeck.Count();
                _amountTxt.text = amount.ToColoredString(amount > 0);
            }
        }
    }
}