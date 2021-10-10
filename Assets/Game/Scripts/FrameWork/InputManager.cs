using System;
using UnityEngine;

namespace Game.Scripts.FrameWork
{
    public class InputManager
    {
        public event Action OnDrag;
        public event Action OnDragStart;
        public event Action OnDragEnd;
        public event Action OnTap;
        public event Action OnScroll;
        
        public bool IsDragging { get; private set; }
        public Vector3 DragOffset { get; private set; }
        
        private InputConfig _inputConfig;
        
        private Vector3 _dragOriginPosition;
        

        public void Initialize(InputConfig inputConfig)
        {
            _inputConfig = inputConfig;
        }

        public void Dispose()
        {
            OnTap = null;
            OnDrag = null;
            OnDragStart = null;
            OnDragEnd = null;
            OnScroll = null;
        }

        public void Tick(float deltaTime)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (IsDragging)
                {
                    IsDragging = false;
                    OnDragEnd?.Invoke();
                }
                else
                {
                    OnTap?.Invoke();
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                _dragOriginPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 offset = Input.mousePosition - _dragOriginPosition; 
                if (offset.magnitude > _inputConfig.DragStartThreshold)
                {
                    if (!IsDragging) OnDragStart?.Invoke();
                    IsDragging = true;
                    DragOffset = offset / _inputConfig.DragOffsetBase;
                    OnDrag?.Invoke();
                }
            }

            // if (IsDragging)
            // {
            // }
        }

    }
}