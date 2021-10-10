using System;
using UnityEngine;
using Game.Scripts.Map;

namespace Game.Scripts.FrameWork
{
    public enum SpawnState
    {
        Loading,
        Spawning,
        Ready,
    }
    
    public class SpawnPool
    {
        private const int MAX_SPAWN_COUNT = 3;
        private readonly Vector3 RECYCLE_POSITION = new Vector3(-10000, -10000, -10000);

        private string _prefabName;
        
        private GameObject _prefab;
        
        private LoadRequest _loadRequest;
        
        private BetterList<GameObject> _instanceList = new BetterList<GameObject>();
        private BetterList<SpawnRequest> _requestList = new BetterList<SpawnRequest>();

        private float _lifeTime;
        
        public void Initialize(string prefabName)
        {
            _prefabName = prefabName;
        }

        public void Dispose()
        {
            if (_loadRequest != null)
            {
                _loadRequest.Cancel = true;
                _loadRequest = null;
            }
            _instanceList.Release();
            _requestList.Release();
            _prefab = null;
        }

        public void Tick(float deltaTime)
        {
            if (_prefab)
            {
                int count = MAX_SPAWN_COUNT;
                while (_requestList.size > 0)
                {
                    if (count-- <= 0) break;
                    SpawnRequest request = _requestList.Pop();
                    DoSpawn(request);
                }
            }
        }

        public void SpawnAsync(Action<GameObject> callback)
        {
            if (!_prefab)
            {
                _loadRequest = BaseContext.Resource.LoadAsync(_prefabName, OnPrefabLoaded);
            }
            
            SpawnRequest request = new SpawnRequest()
            {
                Cancel = false,
                Error = false,
                IsDone = false,
                OnComplete = callback,
                Result = null,
            };
            _requestList.Add(request);
        }
        
        private void OnPrefabLoaded(GameObject prefab)
        {
            _prefab = prefab;
            _loadRequest = null;
        }
        
        private void DoSpawn(SpawnRequest request)
        {
            GameObject instance;
            if (_instanceList.size > 0)
            {
                instance = _instanceList.Pop();
            }
            else
            {
                instance = GameObject.Instantiate(_prefab);
            }

            bool error = !instance;
            if (error)
                Logger.ErrorFormat("load {0} failed", _prefabName);

            request.Result = instance;
            request.IsDone = true;
            request.Error = error;
            request.OnComplete.Invoke(instance);
        }

        public void Despawn(GameObject instance)
        {
            _instanceList.Add(instance);
            instance.transform.position = RECYCLE_POSITION;
        }

    }
}