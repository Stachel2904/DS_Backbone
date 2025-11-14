namespace DivineSkies.Modules.Game.TurnBased.Card
{
    public interface ICardEffect
    {
        public bool Evaluate();
        public void Execute();
    }
}