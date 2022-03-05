using Game.Scripts.FrameWork;
using UnityEngine;

namespace Game.Scripts.Map
{
    public class MapGround
    {
        public void Initialize(MapConfig mapConfig, MapData mapData)
        {
            int blockNumX = mapConfig.SizeX / mapConfig.BlockSizeX;
            int blockNumY = mapConfig.SizeY / mapConfig.BlockSizeY;
            for (int i = 0; i < blockNumX; ++i)
            {
                for (int j = 0; j < blockNumY; ++j)
                {
                    MapBlockData blockData = mapData.GetBlockDataAt(i, j);
                    MapBlockDefinition definition = mapConfig.BlockDefinitions[blockData.ID - 1];
                    Vector3 blockPosition = new Vector3(mapConfig.BlockSizeX * (i + 0.5f), 0, mapConfig.BlockSizeY * (j + 0.5f));
                    Vector3 blockRotation = Vector3.up * blockData.Rotation;
                    Vector3 blockScale = new Vector3(1, blockData.Height, 1);
                    BaseContext.Spawn.SpawnAsync(definition.Prefab, (go) =>
                    {
                        go.transform.SetParent(MapFacade.Instance.GroundRoot);
                        go.transform.localPosition = blockPosition;
                        go.transform.localEulerAngles = blockRotation;
                        go.transform.localScale = blockScale;
                    });
                }
            }
        }
    }
}