using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Map.Generation
{
    public class MapGenerationResult
    {
        public BlockGeneration[,] BlockGenerations;

        public Dictionary<Vector2Int, List<TileGeneration>> TileGenerations;
    }
}