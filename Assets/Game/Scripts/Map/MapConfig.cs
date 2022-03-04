using UnityEngine;

namespace Game.Scripts.Map
{
    [CreateAssetMenu(fileName ="map_config", menuName ="Map Config")]
    public class MapConfig : ScriptableObject
    {
        public Vector3 StartPosition;
        
        public float UnitSize;
        public int SizeX;
        public int SizeY;
        public float Margin;

        public int BlockSizeX;
        public int BlockSizeY;

        public MapTileDefinition[] DefinitionList;
        public MapBlockDefinition[] BlockDefinitions;
    }
}