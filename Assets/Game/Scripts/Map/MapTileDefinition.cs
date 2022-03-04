using System;

namespace Game.Scripts.Map
{
    [Serializable]
    public class MapTileDefinition
    {
        public int ID;

        public string Prefab;

        public int SizeX = 1;
        public int SizeY = 1;
    }
}