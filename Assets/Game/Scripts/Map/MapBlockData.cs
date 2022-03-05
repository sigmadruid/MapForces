using System;
using System.Collections.Generic;
using Game.Scripts.FrameWork;
using Game.Scripts.Map.Generation;
using UnityEngine;

namespace Game.Scripts.Map
{
    public class MapBlockData
    {
        public int Index { get; private set; }

        public int ID
        {
            get { return _generation.ID; }
        }

        public float Rotation
        {
            get { return _generation.Rotation * 90f; }
        }

        public float Height
        {
            get { return _generation.Height; }
        }

        private RectInt _blockRect;
        
        private List<MapTileData> _tileDatas = new List<MapTileData>();

        private BlockGeneration _generation;

        public void Initialize(int index, BlockGeneration generation)
        {
            Index = index;
            
            _generation = generation;
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