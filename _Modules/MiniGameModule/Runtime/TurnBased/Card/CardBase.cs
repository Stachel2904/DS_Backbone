using System.Collections.Generic;
using UnityEngine;

namespace DivineSkies.Modules.Game.TurnBased.Card
{
    public abstract class CardBase
    {
        private static long idCounter = long.MinValue;
        public static long CurrentID { private get => ++idCounter; set => idCounter = (long)Mathf.Max(idCounter, value); }
        public abstract string PlayText { get; }

        public long Id;
        public string Name;
        public int Cost;
        public string Description;
        protected List<ICardEffect> DrawEffects;
        protected List<ICardEffect> PlayEffects;
        protected List<ICardEffect> DiscardEffects;

        protected static T Instantiate<T>(T template) where T : CardBase, new()
        {
            T created = new();

            created.Id = CurrentID;

            created.Name = template.Name;
            created.Cost = template.Cost;
            created.Description = template.Description;
            created.DrawEffects = new List<ICardEffect>(template.DrawEffects);
            created.PlayEffects = new List<ICardEffect>(template.PlayEffects);
            created.DiscardEffects = new List<ICardEffect>(template.DiscardEffects);

            return created;
        }

        public abstract string GetCardText();
        public void OnDraw()
        {
            foreach (ICardEffect effect in DrawEffects)
            {
                if (effect.Evaluate())
                {
                    effect.Execute();
                }
            }
        }

        public void OnPlay()
        {
            foreach (ICardEffect effect in PlayEffects)
            {
                if (effect.Evaluate())
                {
                    effect.Execute();
                }
            }
        }

        public void OnDiscard()
        {
            foreach (ICardEffect effect in DiscardEffects)
            {
                if (effect.Evaluate())
                {
                    effect.Execute();
                }
            }
        }
    }
}
