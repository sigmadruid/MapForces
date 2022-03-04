namespace Game.Scripts.Map.Generation
{
    public abstract class MapGenerationStep
    {
        protected MapConfig _mapConfig;
        protected MapGenerationConfig _generationConfig;
        protected MapGenerationContext _context;
        
        public void Start(MapConfig mapConfig, MapGenerationConfig generationConfig, MapGenerationContext context)
        {
            _mapConfig = mapConfig;
            _generationConfig = generationConfig;
            _context = context;

            OnStart();
        }

        public void End()
        {
            OnEnd();
        }

        public void Process()
        {
            OnProcess();
        }

        protected abstract void OnStart();

        protected abstract void OnEnd();

        protected abstract void OnProcess();
    }
}