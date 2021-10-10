using UnityEngine;

namespace Game.Scripts.Map
{
    public class MapManager
    {
        private MapData _data = new MapData();
        private MapGenerator _generator = new MapGenerator();
        private MapView _view = new MapView();
        private MapCamera _camera = new MapCamera();

        public void Initialize(MapConfig mapConfig, MapCameraConfig cameraConfig)
        {
            MapMath.Initialize(mapConfig);

            _data.Initialize(mapConfig);
            _generator.Initialize();
            _view.Initialize(_data);
            _camera.Initialize(cameraConfig, mapConfig, MapFacade.Instance.MainCamera);

            _camera.OnDrag += OnCameraDragHandler;
            
            _generator.Generate(_data);
        }

        public void Dispose()
        {
            _camera.OnDrag -= OnCameraDragHandler;

            _camera.Dispose();
            _view.Dispose();
            _data.Dispose();
        }
        
        public void Tick(float deltaTime)
        {
            _view.Tick(deltaTime);
            _camera.Tick(deltaTime);
        }

        public void Start()
        {
            OnCameraDragHandler();
        }

        private void OnCameraDragHandler()
        {
            Rect projectRect = _camera.GetProjectRect();
            Vector2 projectMin = projectRect.min;
            Vector2 projectMax = projectRect.max;
            Vector2Int min = MapMath.CalculateTilePosition(new Vector3(projectMin.x, 0, projectMin.y));
            Vector2Int max = MapMath.CalculateTilePosition(new Vector3(projectMax.x, 0, projectMax.y));
            RectInt tileRect = new RectInt(min, new Vector2Int(max.x - min.x, max.y - min.y));
            _view.Cull(tileRect);
        }
    }
}