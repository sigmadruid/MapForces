using Game.Scripts.FrameWork;
using UnityEngine;

namespace Game.Scripts.Map
{
    public class MapGround
    {
        public void Initialize(MapConfig mapConfig)
        {
            int blockNumX = mapConfig.SizeX / mapConfig.BlockSizeX;
            int blockNumY = mapConfig.SizeY / mapConfig.BlockSizeY;
            for (int i = 0; i < blockNumX; ++i)
            {
                for (int j = 0; j < blockNumY; ++j)
                {
                    int index = Random.Range(0, mapConfig.BlockDefinitions.Length);
                    MapBlockDefinition definition = mapConfig.BlockDefinitions[index];
                    Vector3 groundPosition = new Vector3(mapConfig.BlockSizeX * (i + 0.5f), 0, mapConfig.BlockSizeY * (j + 0.5f));
                    BaseContext.Spawn.SpawnAsync(definition.Prefab, (go) =>
                    {
                        go.transform.SetParent(MapFacade.Instance.GroundRoot);
                        go.transform.localPosition = groundPosition;
                    });
                }
            }
        }
    }
}