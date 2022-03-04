using Game.Scripts.Map.Generation.HeightTexture;

namespace Game.Scripts.Map.Generation
{
    public class MapGenerationContext
    {
        public HeightTexture.HeightTexture HeightTexture = new HeightTexture.HeightTexture();

        public BlockGeneration[,] BlockMap;
    }
}