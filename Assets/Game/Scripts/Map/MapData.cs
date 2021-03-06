using Game.Scripts.FrameWork;
using Game.Scripts.Map.Generation;
using UnityEngine;

namespace Game.Scripts.Map
{
    public class MapData
    {
        public MapConfig Config { get; private set; }

        private MapBlockData[,] _blockDataMatrix;

        public void Initialize(MapConfig config, MapGenerationResult generationResult)
        {
            Config = config;
            _blockDataMatrix = new MapBlockData[config.SizeX, config.SizeY];
            int index = 0;
            int blockNumX = Config.SizeX / Config.BlockSizeX;
            int blockNumY = Config.SizeY / Config.BlockSizeY;
            for (int i = 0; i < blockNumX; ++i)
            {
                for (int j = 0; j < blockNumY; ++j)
                {
                    BlockGeneration generation = generationResult.BlockGenerations[i, j];
                    MapBlockData blockData = new MapBlockData();
                    blockData.Initialize(index++, generation);
                    _blockDataMatrix[i, j] = blockData;
                }
            }
        }

        public void Dispose()
        {
            int blockNumX = Config.SizeX / Config.BlockSizeX;
            int blockNumY = Config.SizeY / Config.BlockSizeY;
            for (int i = 0; i < blockNumX; ++i)
            {
                for (int j = 0; j < blockNumY; ++j)
                {
                    MapBlockData blockData = _blockDataMatrix[i, j]; 
                    blockData.Dispose();
                }
            }
            _blockDataMatrix = null;
        }

        public MapBlockData GetBlockDataAt(int x, int y)
        {
            if (0 <= x && x < _blockDataMatrix.GetLength(0) && 0 <= y && y < _blockDataMatrix.GetLength(1))
                return _blockDataMatrix[x, y];
            return null;
        }
        
        public void AddTileData(MapTileData tileData)
        {
            Vector2Int blockPosition = MapMath.CalculateBlockPosition(tileData.BasePosition);
            MapBlockData blockData = _blockDataMatrix[blockPosition.x, blockPosition.y];
            if (!blockData.CheckIntersect(tileData))
                blockData.AddTileData(tileData);
        }

        public void Cull(RectInt viewPort, BetterList<MapTileData> resultList)
        {
            Vector2Int min = viewPort.min;
            Vector2Int max = viewPort.max;
            Vector2Int blockMin = MapMath.CalculateBlockPosition(min);
            Vector2Int blockMax = MapMath.CalculateBlockPosition(max);
            // Debug.LogFormat("min:{0},max:{1}", min, max);
            for (int i = blockMin.x; i <= blockMax.x; ++i)
            {
                for (int j = blockMin.y; j <= blockMax.y; ++j)
                {
                    MapBlockData blockData = _blockDataMatrix[i, j];
                    blockData.Cull(viewPort, resultList);
                }
            }
        }

    }
}