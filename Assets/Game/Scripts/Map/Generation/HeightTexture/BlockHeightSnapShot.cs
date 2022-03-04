using System;

namespace Game.Scripts.Map.Generation.HeightTexture
{
    [Serializable]
    public class BlockHeightSnapShot
    {
        public int Width;
        public int Height;
        public float[,] HeightMap;
    }
}