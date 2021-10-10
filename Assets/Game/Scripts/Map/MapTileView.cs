using System;
using Game.Scripts.FrameWork;
using UnityEngine;
using Logger = Game.Scripts.FrameWork.Logger;

namespace Game.Scripts.Map
{
    public class MapTileView
    {
        private MapTileData _tileData;

        private GameObject _go;

        private bool _show;
        private bool _newShow;
        public MapTileView(MapTileData tileData)
        {
            _tileData = tileData;
        }
        public void Show()
        {
            if (_callback == null) _callback = OnLoaded;
            BaseContext.Spawn.SpawnAsync(_tileData.Definition.Prefab, _callback);
            Logger.LogFormat("show tile {0}", _tileData.BasePosition);
        }

        public void Hide()
        {
            BaseContext.Spawn.Despawn(_tileData.Definition.Prefab, _go);
            Logger.LogFormat("hide tile {0}", _tileData.BasePosition);
        }

        private Action<GameObject> _callback;

        private void OnLoaded(GameObject go)
        {
            if (!_go)
            {
                _go = go;
                _go.transform.SetParent(MapFacade.Instance.TileRoot);
            }

            _go.transform.localPosition = MapMath.CalculateWorldPosition(_tileData.BasePosition);
            _go.transform.localRotation = Quaternion.identity;
            _go.transform.localScale = Vector3.one;
        }
    }
}