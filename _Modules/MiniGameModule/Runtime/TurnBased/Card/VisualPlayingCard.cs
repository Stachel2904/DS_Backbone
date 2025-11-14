using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DivineSkies.Modules.UI;

namespace DivineSkies.Modules.Game.TurnBased.Card
{
    public abstract class VisualPlayingCard : UiItemBase
    {
        [SerializeField] private TextMeshProUGUI _txtHeadline, _txtEffect;
        [SerializeField] private Button _btnSelect;

        public CardBase CardReference => _cardReference;

        private CardBase _cardReference;


        protected virtual void Start()
        {
            _btnSelect.onClick.RemoveAllListeners();
            _btnSelect.onClick.AddListener(OnPlay);
        }

        public virtual void Setup(CardBase card)
        {
            _cardReference = card;

            _txtHeadline.text = _cardReference.Name;
            _txtEffect.text = _cardReference.GetCardText();
        }

        /// <summary>
        /// call <see cref="CardGameController{TModule, TProcessor, TVisualization, TCard}.PlayCard(TCard)"/> to play card
        /// </summary>
        protected abstract void OnPlay();

        public void Refresh() => Setup(_cardReference);
    }
}
