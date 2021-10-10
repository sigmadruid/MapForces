using System;
using System.Collections.Generic;
using Game.Scripts.FrameWork;
using UnityEngine;

namespace Game.Scripts.Map
{
    public class MapBlockData
    {
        public int Index { get; private set; }

        private RectInt _blockRect;
        
        private List<MapTileData> _tileDatas = new List<MapTileData>();

        public void Initialize(int index)
        {
            Index = index;
            _blockRect = MapMath.GetBlockRect(Index);
        }

        public void Dispose()
        {
            _tileDatas.Clear();
        }

        public void AddTileData(MapTileData tileData)
        {
            _tileDatas.Add(tileData);
        }

        public bool CheckIntersect(MapTileData other)
        {
            for (int i = 0; i < _tileDatas.Count; ++i)
            {
                MapTileData tileData = _tileDatas[i];
                if (tileData.Intersect(other))
                    return true;
            }

            return false;
        }

        public bool IsOverlaped(RectInt rect)
        {
            return rect.xMin >= _blockRect.xMin && rect.yMin >= _blockRect.yMin
                && rect.xMax <= _blockRect.xMax && rect.yMax <= _blockRect.yMax;
        }

        public void Cull(RectInt viewPort, BetterList<MapTileData> resultList)
        {
            bool isOverlaped = IsOverlaped(viewPort);
            for (int i = 0; i < _tileDatas.Count; ++i)
            {
                MapTileData tileData = _tileDatas[i];
                if (isOverlaped || tileData.Intersect(viewPort))
                {
                    resultList.Add(tileData);
                    if (tileData == null)
                    {
                        Debug.LogError("null tile data");
                    }
                }
            }
        }

        public void ForeachTile(Action<MapTileData> each)
        {
            if (each != null)
            {
                for (int i = 0; i < _tileDatas.Count; ++i)
                {
                    MapTileData tileData = _tileDatas[i];
                    each.Invoke(tileData);
                }
            }
        }
    }
}