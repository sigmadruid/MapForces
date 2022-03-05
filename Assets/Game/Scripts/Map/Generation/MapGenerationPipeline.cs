using System;
using Game.Scripts.Map.Generation.HeightTexture;

namespace Game.Scripts.Map.Generation
{
    public abstract class MapGenerationPipeline
    {

        private MapConfig _mapConfig;
        private MapGenerationConfig _generationConfig;
        private MapGenerationContext _context = new MapGenerationContext();

        private int _index;
        private MapGenerationStep _currentStep;

        public bool IsFinished
        {
            get;
            private set;
        }

        public void Initialize(MapConfig mapConfig, MapGenerationConfig generationConfig)
        {
            _mapConfig = mapConfig;
            _generationConfig = generationConfig;
            
            OnInitialize();
        }

        public void Dispose()
        {
            
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

        public MapGenerationResult GetResult()
        {
            if (!IsFinished)
            {
                throw new Exception("Map Generation Pipeline is not finished yet!");
            }
            
            MapGenerationResult result = new MapGenerationResult();
            result.BlockGenerations = _context.BlockMap;
            return result;
        }
        
        protected abstract MapGenerationStep[] StepList { get; }

        protected abstract void OnInitialize();
        protected abstract void OnStart();
        protected abstract void OnProcess();
    }
}