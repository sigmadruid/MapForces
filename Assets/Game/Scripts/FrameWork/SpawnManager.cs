using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.FrameWork
{
    public class SpawnManager
    {
        private Dictionary<string, SpawnPool> _poolDict = new Dictionary<string, SpawnPool>();
        
        public void Initialize()
        {
        }

        public void Dispose()
        {
            using (var i = _poolDict.Values.GetEnumerator())
            {
                while (i.MoveNext())
                {
                    i.Current?.Dispose();
                }
            }
            _poolDict.Clear();
        }
        
        public void Tick(float deltaTime)
        {
            using (var i = _poolDict.Values.GetEnumerator())
            {
                while (i.MoveNext())
                {
                    i.Current?.Tick(deltaTime);
                }
            }
        }
        
        public void SpawnAsync(string name, Action<GameObject> callback)
        {
            SpawnPool pool;
            if (!_poolDict.TryGetValue(name, out pool))
            {
                pool = new SpawnPool();
                pool.Initialize(name);
                _poolDict[name] = pool;
            }

            pool.SpawnAsync(callback);
        }

        public void Despawn(string name, GameObject instance)
        {
            SpawnPool pool;
            if (_poolDict.TryGetValue(name, out pool))
            {
                pool.Despawn(instance);
            }
        }
    }
}