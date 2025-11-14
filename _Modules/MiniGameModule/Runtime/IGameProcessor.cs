namespace DivineSkies.Modules.Game
{
    public interface IGameProcessor
    {
        void SetController(IGameController controller);
        void OnStart();
        void OnEnd(GameEndReason reason);
    }
}
