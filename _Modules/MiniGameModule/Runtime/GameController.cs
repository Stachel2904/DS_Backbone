namespace DivineSkies.Modules.Game
{
    public abstract class GameController<TModule, TProcessor, TVisualization> : ModuleBase<TModule>, IGameController where TModule : Core.ModuleBase where TProcessor : IGameProcessor where TVisualization : IGameVisualization
    {
        IGameProcessor IGameController.Processor => Processor;
        public TProcessor Processor { get; private set; }

        IGameVisualization IGameController.Visualization => Visualization;
        public TVisualization Visualization { get; private set; }

        protected virtual bool AutoStart => true;

        public override void OnSceneFullyLoaded()
        {
            base.OnSceneFullyLoaded();

            Processor = CreateProcessor();
            Processor.SetController(this);

            Visualization = CreateVisualization();
            Visualization.SetController(this);

            if (AutoStart)
            {
                StartGame();
            }
        }

        public override void BeforeUnregister()
        {
            Visualization = default;
            Processor = default;
            base.BeforeUnregister();
        }

        public abstract TProcessor CreateProcessor();
        public abstract TVisualization CreateVisualization();

        public void StartGame()
        {
            Processor.OnStart();
        }

        public void End(GameEndReason reason)
        {
            Processor.OnEnd(reason);
        }
    }
}
