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
        protected override void OnStart()
        {
            _heightMap = new float[_mapConfig.SizeX, _mapConfig.SizeY];
            for (int i = 0; i < _mapConfig.SizeX; ++i)
            {
                for (int j = 0; j < _mapConfig.SizeY; ++j)
                {
                    _heightMap[i, j] = 0f;
                }
            }
            _markMap = new int[_mapConfig.SizeX, _mapConfig.SizeY];
            for (int i = 0; i < _mapConfig.SizeX; ++i)
            {
                for (int j = 0; j < _mapConfig.SizeY; ++j)
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
                int x = Random.Range(0, _mapConfig.SizeX);
                int y = Random.Range(0, _mapConfig.SizeY);
                int range = Random.Range(1, groundConfig.MaxHighLandRange);
                float height = Random.Range(groundConfig.MinHeight, groundConfig.MaxHeight);
                ProcessHighLand(i, x, y, range, height);
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
                if (0 <= i && i < _mapConfig.SizeX)
                {
                    for (int j = y - 1; j <= y + 1; ++j)
                    {
                        if (0 <= j && j < _mapConfig.SizeY)
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