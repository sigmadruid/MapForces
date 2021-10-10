// #define USE_ASSET_BUNDLE

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.FrameWork
{
    public class ResourceManager
    {
        private Dictionary<string, LoadRequest> _requestDict = new Dictionary<string, LoadRequest>();
        
        private Dictionary<string, GameObject> _prefabDict = new Dictionary<string, GameObject>();
        public void Initialize()
        {
        }

        public void Dispose()
        {
            _requestDict.Clear();
            _prefabDict.Clear();
        }

        public void Tick(float deltaTime)
        {
            
        }

        public T Load<T>(string path) where T : UnityEngine.Object
        {
#if !USE_ASSET_BUNDLE
            return AssetDatabase.LoadAssetAtPath<T>(path);
#else
            //load from ab;
            return null;
#endif
        }
        
        public LoadRequest LoadAsync(string name, Action<GameObject> callback)
        {
            GameObject prefab = null;
#if !USE_ASSET_BUNDLE
            prefab = AssetDatabase.LoadAssetAtPath<GameObject>(name);
            callback?.Invoke(prefab);
            return null;
#else
            if (_prefabDict.TryGetValue(name, out prefab))
            {
                callback?.Invoke(prefab);
                return null;
            }

            if (_requestDict.TryGetValue(name, out LoadRequest request))
            {
                request.OnComplete += callback;
                return request;
            }

            request = new LoadRequest();
            request.OnComplete += callback;
            return request;
#endif
        }
    }
}