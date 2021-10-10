using UnityEngine;

namespace Game.Scripts.Map
{
    public class MapTileData
    {
        public MapTileDefinition Definition { get; private set; }

        public Vector2Int BasePosition;
        public Vector2Int Min;
        public Vector2Int Max;

        public void Initialize(int baseX, int baseY, MapTileDefinition definition)
        {
            BasePosition = new Vector2Int(baseX, baseY);
            Definition = definition;
            Min = new Vector2Int(baseX, baseY);
            Max = new Vector2Int(baseX + definition.SizeX, baseY + definition.SizeY);
        }

        public void Dispose()
        {
        }

        public bool Intersect(MapTileData tileData)
        {
            return tileData.Min.x < Max.x && tileData.Max.x > Min.x && tileData.Min.y < Max.y && tileData.Max.y > Min.y;
        }
        
        public bool Intersect(RectInt rect)
        {
            return rect.xMin < Max.x && rect.xMax > Min.x && rect.yMin < Max.y && rect.yMax > Min.y;
        }

    }
}