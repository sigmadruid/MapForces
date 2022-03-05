using UnityEngine;

namespace Game.Scripts.Map.Generation.HeightTexture
{
    public class HeightTexture
    {
        private float[,] _heightMap;

        public void Initialize(float[,] heightMap)
        {
            _heightMap = heightMap;
        }

        public float Sample(float u, float v)
        {
            u = Mathf.Clamp01(u);
            v = Mathf.Clamp01(v);
            int i = Mathf.RoundToInt(u * _heightMap.GetLength(0));
            int j = Mathf.RoundToInt(v * _heightMap.GetLength(1));
            return _heightMap[i, j];
        }
        
        public float Sample(int i, int j)
        {
            return _heightMap[i, j];
        }
    }
}