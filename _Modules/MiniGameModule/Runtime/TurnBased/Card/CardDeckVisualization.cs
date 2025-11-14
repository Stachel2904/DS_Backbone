using DivineSkies.Tools.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DivineSkies.Modules.Game.TurnBased.Card
{
    public class CardDeckVisualization : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _amountTxt;

        protected IList _displayingDeck;

        public virtual void SetDeck(IList deck)
        {
            _displayingDeck = deck;
            Refresh();
        }

        public virtual void Refresh()
        {
            if(_amountTxt != null)
            {
                int amount = _displayingDeck.Count;
                _amountTxt.text = amount.ToColoredString(amount > 0);
            }
        }
    }
}