using System;

namespace Game.Scripts.Map
{
    [Serializable]
    public class MapBlockDefinition
    {
        public int ID;
        
        public string Prefab;

        public float MinHeight;
        public float MaxHeight;
    }
}