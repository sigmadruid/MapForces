using System;
using Game.Scripts.FrameWork;
using UnityEngine;

namespace Game.Scripts.Map
{
    public class MapCamera
    {
        public event Action OnDrag;
        
        private Camera _rawCamera;

        private Plane _targetPlane;

        private MapCameraConfig _config;

        private MapConfig _mapConfig;
        
        public void Initialize(MapCameraConfig config, MapConfig mapConfig, Camera rawCamera)
        {
            _config = config;
            _mapConfig = mapConfig;
            _rawCamera = rawCamera;
            _targetPlane = new Plane(config.PlaneNormal, Vector3.zero);

            _rawCamera.transform.localPosition = config.InitPosition;
            _rawCamera.transform.localEulerAngles = new Vector3(config.XAngle, 0, 0);
            
            BaseContext.Input.OnDrag += OnDragHandler;
            BaseContext.Input.OnDragStart += OnDragStartHandler;
            BaseContext.Input.OnDragEnd += OnDragEndHandler;
            BaseContext.Input.OnScroll += OnScrollHandler;
        }

        public void Dispose()
        {
            BaseContext.Input.OnDrag -= OnDragHandler;
            BaseContext.Input.OnDragStart -= OnDragStartHandler;
            BaseContext.Input.OnDragEnd -= OnDragEndHandler;
            BaseContext.Input.OnScroll -= OnScrollHandler;
        }
        
        public void Tick(float deltaTime)
        {
            if (Input.GetMouseButtonUp(1))
            {
                Rect rect = GetProjectRect();
                GameObject tl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tl.transform.position = new Vector3(rect.min.x, 0, rect.max.y);
                GameObject tr = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tr.transform.position = new Vector3(rect.max.x, 0, rect.max.y);
                GameObject br = GameObject.CreatePrimitive(PrimitiveType.Cube);
                br.transform.position = new Vector3(rect.max.x, 0, rect.min.y);
                GameObject bl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                bl.transform.position = new Vector3(rect.min.x, 0, rect.min.y);
            }
        }

        private Vector3 _dragStartPosition;
        private void OnDragHandler()
        {
            Vector3 offset = BaseContext.Input.DragOffset * _config.TranslateSpeed;
            _rawCamera.transform.localPosition = _dragStartPosition - new Vector3(offset.x, 0, offset.y);
            Vector3 offBoundsOffset = ApplyBounds();
            _rawCamera.transform.localPosition += offBoundsOffset;
            OnDrag?.Invoke();
        }

        private void OnDragStartHandler()
        {
            _dragStartPosition = _rawCamera.transform.localPosition;
        }

        private void OnDragEndHandler()
        {
        }

        private void OnScrollHandler()
        {
            
        }

        private Vector3 ApplyBounds()
        {
            // Vector3 center = GetRaycastPoint(new Vector3(0.5f, 0.5f, 0));
            // Vector3 top = GetRaycastPoint(new Vector3(0.5f, 1f, 0));
            // Vector3 right = GetRaycastPoint(new Vector3(1f, 0.5f, 0));
            // float xMargin = right.x - center.x;
            // float zMargin = top.z - center.z;
            // Vector3 offset = Vector3.zero;
            // if (center.x <= xMargin) offset.x += xMargin - center.x;
            // if (center.z >= -zMargin) offset.z -= center.z - (-zMargin);
            // if (center.x >= 1000 - xMargin) offset.x -= (center.x - (1000 - xMargin));
            // if (center.z <= -(1000 - zMargin)) offset.z += (- (1000 - zMargin) - center.z);
            // return offset;

            Rect projectRect = GetProjectRect();
            Vector3 mapStartPosition = _mapConfig.StartPosition;
            float left = mapStartPosition.x + _mapConfig.Margin;
            float right = mapStartPosition.x + _mapConfig.SizeX * _mapConfig.UnitSize - _mapConfig.Margin;
            float bottom = mapStartPosition.z + _mapConfig.Margin;
            float top = mapStartPosition.z + _mapConfig.SizeY * _mapConfig.UnitSize - _mapConfig.Margin;
            Vector3 offset = Vector3.zero;
            if (projectRect.xMin < left) 
                offset.x += left - projectRect.xMin;
            else if (projectRect.xMax > right)
                offset.x -= projectRect.xMax - right;
            if (projectRect.yMin < bottom) 
                offset.z += bottom - projectRect.yMin;
            else if (projectRect.yMax > top)
                offset.z -= projectRect.yMax - top;
            return offset;
        }

        public Rect GetProjectRect()
        {
            Vector3 tl = GetRaycastPoint(new Vector3(0, 1, 0));
            Vector3 tr = GetRaycastPoint(new Vector3(1, 1, 0));
            Vector3 bl = GetRaycastPoint(new Vector3(0, 0, 0));
            return new Rect(new Vector2(tl.x, bl.z), new Vector2(tr.x - tl.x, tr.z - bl.z));
        }
        
        private Vector3 GetRaycastPoint(Vector3 corner)
        {
            Ray ray = _rawCamera.ViewportPointToRay(corner);
            _targetPlane.Raycast(ray, out float enter);
            return ray.GetPoint(enter);
        }
    }
}