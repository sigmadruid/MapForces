using System;
using UnityEngine;
using Game.Scripts.FrameWork;
using Game.Scripts.Map;
using Game.Scripts.Map.Generation;

namespace Game.Scripts
{
    public class GameFacade : MonoBehaviour
    {
        public static GameFacade Instance;
        
        public MapManager MapManager { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            
            GetComponent<MapFacade>().Initialize();
            
            BaseContext.Initialize();

            MapConfig mapConfig = BaseContext.Resource.Load<MapConfig>("Assets/Game/ResourceData/Config/map_config.asset");
            MapCameraConfig cameraConfig = BaseContext.Resource.Load<MapCameraConfig>("Assets/Game/ResourceData/Config/camera_config.asset");
            MapGenerationConfig generationConfig = BaseContext.Resource.Load<MapGenerationConfig>("Assets/Game/ResourceData/Config/generation_config.asset");
            MapManager = new MapManager();
            MapManager.Initialize(mapConfig, cameraConfig, generationConfig);
        }

        private void Start()
        {
            MapManager.Start();
        }

        private void OnDestroy()
        {
            MapManager.Dispose();
            
            BaseContext.Dispose();

            GetComponent<MapFacade>().Dispose();

            Instance = null;
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            
            BaseContext.Tick(dt);
            
            MapManager.Tick(dt);
        }

    }
}