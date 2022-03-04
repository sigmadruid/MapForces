using Game.Scripts.Map.Generation.HeightTexture;

namespace Game.Scripts.Map.Generation
{
    public abstract class MapGenerationPipeline
    {
        protected abstract MapGenerationStep[] StepList { get; }

        private MapConfig _mapConfig;
        private MapGenerationConfig _generationConfig;
        private MapGenerationContext _context;

        private int _index;
        private MapGenerationStep _currentStep;

        public bool IsFinished
        {
            get;
            private set;
        }

        public void Initialize(MapConfig mapConfig, MapGenerationConfig generationConfig, MapGenerationContext context)
        {
            _mapConfig = mapConfig;
            _generationConfig = generationConfig;
            _context = context;
            
            OnInitialize();
        }
        
        public void Start()
        {
            IsFinished = false;

            _currentStep = StepList[0];
            _currentStep.Start(_mapConfig, _generationConfig, _context);
            _index = 0;
            
            OnStart();
        }

        public void Process()
        {
            if (IsFinished) return;
            
            _currentStep.Process();
            _currentStep.End();
            _index++;

            if (_index < StepList.Length)
            {
                _currentStep = StepList[_index];
                _currentStep.Start(_mapConfig, _generationConfig, _context);
            }
            else
            {
                IsFinished = true;
            }
            
            OnProcess();
        }

        protected abstract void OnInitialize();
        protected abstract void OnStart();
        protected abstract void OnProcess();
    }
}