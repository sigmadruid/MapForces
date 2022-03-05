using System;
using Random = UnityEngine.Random;

namespace Game.Scripts.Map.Generation.Legacy
{
    public class MapGenerator
    {
        public void Initialize()
        {
            Random.InitState(DateTime.Now.Millisecond);
        }

        public void Generate(MapData mapData)
        {
           //for test. Random 1000 tiles on map.
           for (int i = 0; i < 5000; ++i)
           {
               int x = Random.Range(0, mapData.Config.SizeX);
               int y = Random.Range(0, mapData.Config.SizeY);
               int index = Random.Range(0, mapData.Config.DefinitionList.Length);
               MapTileDefinition definition = mapData.Config.DefinitionList[index];
               MapTileData tileData = new MapTileData();
               tileData.Initialize(x, y, definition);
               mapData.AddTileData(tileData);
           }
        }

    }
}