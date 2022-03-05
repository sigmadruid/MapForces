using UnityEngine;

namespace Game.Scripts.Map.Generation.HeightTexture
{
    /// <summary>
    /// 根据生成的高度图，采样各个block位置的高度。
    /// 根据高度，确定各个block的类型和朝向。
    /// </summary>
    public class BlockGenerationStep : MapGenerationStep
    {
        private BlockGeneration[,] _blockMap;
        protected override void OnStart()
        {
            int blockXNum = _mapConfig.SizeX / _mapConfig.BlockSizeX;
            int blockYNum = _mapConfig.SizeY / _mapConfig.BlockSizeY;
            _blockMap = new BlockGeneration[blockXNum, blockYNum];
        }

        protected override void OnEnd()
        {
            _context.BlockMap = _blockMap;
        }

        protected override void OnProcess()
        {
            int blockXNum = _mapConfig.SizeX / _mapConfig.BlockSizeX;
            int blockYNum = _mapConfig.SizeY / _mapConfig.BlockSizeY;
            for (int i = 0; i < blockXNum; ++i)
            {
                for (int j = 0; j < blockYNum; ++j)
                {
                    // float u = i * 1f * _mapConfig.BlockSizeX / _mapConfig.SizeX;
                    // float v = j * 1f * _mapConfig.BlockSizeY / _mapConfig.SizeY;
                    float height = _context.HeightTexture.Sample(i, j);
                    int id = GetBlockIDByHeight(height);
                    short rot = (short)Random.Range(0, 4);
                    _blockMap[i, j] = new BlockGeneration()
                    {
                        ID = id,
                        Height = height,
                        Rotation = rot,
                    };
                }
            }
        }

        private int GetBlockIDByHeight(float height)
        {
            foreach (MapBlockDefinition def in _mapConfig.BlockDefinitions)
            {
                if (def.MinHeight <= height && height < def.MaxHeight)
                {
                    return def.ID;
                }
            }
            return 1;
        }
    }
}