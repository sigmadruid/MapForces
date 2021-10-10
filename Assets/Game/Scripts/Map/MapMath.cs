using UnityEngine;

namespace Game.Scripts.Map
{
    public static class MapMath
    {
        private static MapConfig _config;

        public static void Initialize(MapConfig config)
        {
            _config = config;
        }

        public static Vector3 CalculateWorldPosition(Vector2Int tilePosition)
        {
            return new Vector3(tilePosition.x * _config.UnitSize, 0, tilePosition.y * _config.UnitSize);
        }

        public static Vector2Int CalculateTilePosition(Vector3 position)
        {
            int x = Mathf.FloorToInt(position.x / _config.UnitSize);
            int y = Mathf.FloorToInt(position.z / _config.UnitSize);
            return new Vector2Int(x, y);
        }

        public static Vector2Int CalculateBlockPosition(Vector2Int tilePosition)
        {
            return new Vector2Int(tilePosition.x / _config.BlockSizeX, tilePosition.y / _config.BlockSizeY);
        }

        public static RectInt GetBlockRect(int index)
        {
            int x = index % _config.BlockSizeX;
            int y = index / _config.BlockSizeX;
            return new RectInt(x, y, _config.BlockSizeX, _config.BlockSizeY);
        }
    }
}