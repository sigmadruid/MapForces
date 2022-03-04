using System;
using UnityEngine;

namespace Game.Scripts.Map.Generation
{
    public class MapGenerationConfig : ScriptableObject
    {
        public GroundGenerationConfig GroundConfig;
    }

    [Serializable]
    public class GroundGenerationConfig
    {
        public int MaxHighLandNum;

        public int MaxHighLandRange;
        public float SpreadRatio;

        public float MaxHeight;
        public float MinHeight;
    }
}