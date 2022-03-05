using UnityEngine;

namespace Game.Scripts.Map.Generation.HeightTexture
{
    /// <summary>
    /// 生成一个二维高度数组。
    /// 随机点生成高度，并按ratio和range向周围扩散。
    /// </summary>
    public class HeightTextureStep : MapGenerationStep
    {
        private float[,] _heightMap;
        private int[,] _markMap;

        private int blockNumX;
        private int blockNumY;
        protected override void OnStart()
        {
            blockNumX = _mapConfig.SizeX / _mapConfig.BlockSizeX;
            blockNumY = _mapConfig.SizeY / _mapConfig.BlockSizeY;
            _heightMap = new float[blockNumX, blockNumY];
            for (int i = 0; i < blockNumX; ++i)
            {
                for (int j = 0; j < blockNumY; ++j)
                {
                    _heightMap[i, j] = 1f;
                }
            }
            _markMap = new int[blockNumX, blockNumY];
            for (int i = 0; i < blockNumX; ++i)
            {
                for (int j = 0; j < blockNumY; ++j)
                {
                    _markMap[i, j] = 0;
                }
            }
        }

        protected override void OnEnd()
        {
            _context.HeightTexture.Initialize(_heightMap);
        }

        protected override void OnProcess()
        {
            GroundGenerationConfig groundConfig = _generationConfig.GroundConfig;
            for (int i = 0; i < groundConfig.MaxHighLandNum; ++i)
            {
                int x = Random.Range(0, blockNumX);
                int y = Random.Range(0, blockNumY);
                int range = Random.Range(1, groundConfig.MaxHighLandRange);
                float height = Random.Range(groundConfig.MinHeight, groundConfig.MaxHeight);
                ProcessHighLand(i + 1, x, y, range, height);
            }
            
        }

        private void ProcessHighLand(int index, int x, int y, int range, float height)
        {
            if (range < 0) return;
            
            if (index > _markMap[x, y])
            {
                _heightMap[x, y] += height;
                _markMap[x, y] = index;
            }

            for (int i = x - 1; i <= x + 1; ++i)
            {
                if (0 <= i && i < blockNumX)
                {
                    for (int j = y - 1; j <= y + 1; ++j)
                    {
                        if (0 <= j && j < blockNumY)
                        {
                            if (Random.value <= _generationConfig.GroundConfig.SpreadRatio)
                                ProcessHighLand(index, i, j, range - 1, height);
                        }
                    }
                }
                
            }
        }
    }
}