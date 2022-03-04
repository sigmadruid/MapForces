using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Map.Generation.HeightTexture.Editor
{
    public class BlockHeightSnapShotGenerator
    {
        private const string PREFAB_PATH = "Assets/Game/Block";
        private const string OUTPUT_PATH = "Assets/Game/ResourceData/BlockHeight";

        private const float HEIGHT = 100f;

        private static MapConfig _mapConfig;
        
        [MenuItem("Tools/Snapshot Block Prefabs")]
        public static void Execute()
        {
            _mapConfig = AssetDatabase.LoadAssetAtPath<MapConfig>("Assets/Game/ResourceData/Config/map_config.asset");
            if (_mapConfig == null)
            {
                Debug.LogError("can't load map config");
                return;
            }

            string[] files = Directory.GetFiles(PREFAB_PATH);
            for (int i = 0; i < files.Length; ++i)
            {
                string path = files[i];
                if (!path.EndsWith(".prefab")) continue;

                SnapShot(path);
            }
            
            AssetDatabase.Refresh();
        }

        private static void SnapShot(string path)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            GameObject go = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);

            BlockHeightSnapShot snapShot = new BlockHeightSnapShot()
            {
                Width = _mapConfig.BlockSizeX,
                Height = _mapConfig.BlockSizeY,
                HeightMap = new float[_mapConfig.BlockSizeX, _mapConfig.BlockSizeY],
            };
            
            float startX = -_mapConfig.BlockSizeX / 2f;
            float startY = -_mapConfig.BlockSizeY / 2f;
            
            Vector3 origin = go.transform.position + Vector3.up * HEIGHT;
           
            for (int i = 0; i < _mapConfig.BlockSizeX; ++i)
            {
                for (int j = 0; j < _mapConfig.BlockSizeY; ++j)
                {
                    float x = startX + i * _mapConfig.UnitSize;
                    float y = startY + j * _mapConfig.UnitSize;
                    Vector3 startPosition = origin + new Vector3(x, 0, y);
                    Ray ray = new Ray(startPosition, Vector3.down);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo))
                    {
                        snapShot.HeightMap[i, j] = hitInfo.point.y;
                        Debug.LogFormat($"i={i}, j={j}, value={hitInfo.point.y}");
                    }
                    else
                    {
                        snapShot.HeightMap[i, j] = 0;
                    }
                }
            }
            
            GameObject.DestroyImmediate(go);

            string outputName = Path.GetFileNameWithoutExtension(path) + ".bytes";
            string outputPath = Path.Combine(OUTPUT_PATH, outputName);
            using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, snapShot);
            }
        }
        
        [MenuItem("Tools/Snapshot Block Test")]
        public static void TestResult()
        {
            string[] files = Directory.GetFiles(OUTPUT_PATH);
            for (int i = 0; i < files.Length; ++i)
            {
                string path = files[i];
                if (!path.EndsWith(".bytes")) continue;
                
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    BlockHeightSnapShot snapShot = formatter.Deserialize(fs) as BlockHeightSnapShot;
                    if (snapShot != null)
                    {
                        Debug.LogFormat($"{snapShot.Width}, {snapShot.Height}");
                        Debug.LogFormat($"{snapShot.HeightMap[5, 5]}");
                    }
                }
            }
        }
    }
}