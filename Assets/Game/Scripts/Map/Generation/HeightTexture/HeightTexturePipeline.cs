namespace Game.Scripts.Map.Generation.HeightTexture
{
    public class HeightTexturePipeline : MapGenerationPipeline
    {
        protected override MapGenerationStep[] StepList
        {
            get
            {
                return new MapGenerationStep[]
                {
                    new HeightTextureStep(), 
                    new BlockGenerationStep(), 
                    // new TileGenerateStep(), 
                };
            }
        }

        protected override void OnInitialize()
        {
            
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnProcess()
        {
            
        }
    }
}