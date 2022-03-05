using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.FrameWork;

namespace Game.Scripts.Map
{
    public class MapView
    {
        private MapData _mapData;
        
        private Dictionary<MapTileData, MapTileView> _currentViewDict = new Dictionary<MapTileData, MapTileView>();
        
        private BetterList<MapTileData> _culledResult = new BetterList<MapTileData>();
        private BetterList<MapTileData> _prevCulledResult = new BetterList<MapTileData>();
        
        private HashSet<MapTileData> _showSet = new HashSet<MapTileData>();
        private HashSet<MapTileData> _hideSet = new HashSet<MapTileData>();

        private MapGround _ground;

        public void Initialize(MapData mapData)
        {
            _mapData = mapData;
            
            _ground = new MapGround();
            _ground.Initialize(mapData.Config, _mapData);
        }

        public void Dispose()
        {
            _culledResult.Release();
            _prevCulledResult.Release();
        }

        public void Start()
        {
            
        }
        
        public void Tick(float deltaTime)
        {
        }

        public void Cull(RectInt viewPort)
        {
            _prevCulledResult.Clear();
            for (int i = 0; i < _culledResult.size; ++i)
            {
                _prevCulledResult.Add(_culledResult[i]);
            }
            _culledResult.Clear();
            
            _mapData.Cull(viewPort, _culledResult);

            GetDifferentSet(_culledResult, _prevCulledResult, _showSet, _hideSet);

            using(var i = _hideSet.GetEnumerator())
            {
                while (i.MoveNext())
                {
                    MapTileData tileData = i.Current;
                    Hide(tileData);
                }
            }
            using(var i = _showSet.GetEnumerator())
            {
                while (i.MoveNext())
                {
                    MapTileData tileData = i.Current;
                    Show(tileData);
                }
            }
        }

        private void GetDifferentSet(BetterList<MapTileData> newList, BetterList<MapTileData> oldList, HashSet<MapTileData> showList, HashSet<MapTileData> hideList)
        {
            showList.Clear();
            for (int i = 0; i < newList.size; ++i)
            {
                bool find = false;
                MapTileData tileData = newList[i];
                Vector2Int newPosition = tileData.BasePosition;
                for (int j = 0; j < oldList.size; ++j)
                {
                    if (newPosition == oldList[j].BasePosition)
                    {
                        find = true;
                        break;
                    }
                }

                if (!find) showList.Add(tileData);
            }
            
            hideList.Clear();
            for (int i = 0; i < oldList.size; ++i)
            {
                bool find = false;
                MapTileData tileData = oldList[i];
                Vector2Int oldPosition = tileData.BasePosition;
                for (int j = 0; j < newList.size; ++j)
                {
                    if (oldPosition == newList[j].BasePosition)
                    {
                        find = true;
                        break;
                    }
                }

                if (!find) hideList.Add(tileData);
            }
        }
        
        private void Hide(MapTileData tileData)
        {
            MapTileView view = null;
            if (_currentViewDict.TryGetValue(tileData, out view))
            {
                view.Hide();
                MapTileViewFactory.Release(view);
                _currentViewDict.Remove(tileData);
            }
        }

        private void Show(MapTileData tileData)
        {
            MapTileView view = null;
            if (!_currentViewDict.TryGetValue(tileData, out view))
            {
                view = MapTileViewFactory.Create(tileData);
                _currentViewDict[tileData] = view;
            }
            else
            {
                view = _currentViewDict[tileData];
            }
            view.Show();
        }
        
    }
}