using System;
using UnityEngine;

namespace Game.Scripts.Map
{
    public class MapFacade : MonoBehaviour
    {
        public static MapFacade Instance;
        
        public Camera MainCamera;

        public Transform TileRoot;
        public Transform GroundRoot;

        public void Initialize()
        {
            Instance = this;
        }

        public void Dispose()
        {
            Instance = null;
        }
    }
}