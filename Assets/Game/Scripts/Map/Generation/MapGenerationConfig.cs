using System;
using UnityEngine;

namespace Game.Scripts.Map.Generation
{
    [CreateAssetMenu(fileName ="generation_config", menuName ="Generation Config")]
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