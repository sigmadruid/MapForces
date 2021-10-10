using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Editor
{
    public class AssetBundleBuilder
    {
        private static Dictionary<string, string> _asset2Bundles = new Dictionary<string, string>();
        
        [MenuItem("Tools/MarkAllBundles")]
        public static void MarkAllBundles()
        {
            _asset2Bundles.Clear();
            
            string[] allResFiles = Directory.GetFiles(AssetBundleConfig.RESOURCE_BASE_PATH, "*.*", SearchOption.AllDirectories);
            foreach (string path in allResFiles)
            {
                if (path.EndsWith(".meta")) continue;
                AssetImporter ai = AssetImporter.GetAtPath(path);
                if (ai != null)
                {
                    string bundleName = GetBundleName(path);
                    ai.assetBundleName = bundleName;
                    ai.SaveAndReimport();

                    string name = Path.GetFileNameWithoutExtension(path);
                    _asset2Bundles.Add(name, bundleName);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (string asset in _asset2Bundles.Keys)
            {
                sb.Append(asset);
                sb.Append(',');
                sb.Append(_asset2Bundles[asset]);
                sb.Append(",\n");
            }
            File.WriteAllText(AssetBundleConfig.MARK_RESULT_PATH, sb.ToString());
        }

        private static string GetBundleName(string path)
        {
            path = Path.GetDirectoryName(path);
            int startIndex = path.IndexOf(AssetBundleConfig.RESOURCE_FOLDER_NAME) + AssetBundleConfig.RESOURCE_FOLDER_NAME.Length;
            path = path.Substring(startIndex + 1);
            path = path.Replace('/', '+');
            path = path.Replace('\\', '+');
            return path.ToLower();
        }

        public static void BuildAllBundles()
        {
            // BuildPipeline.BuildAssetBundles();
        }
    }
}

