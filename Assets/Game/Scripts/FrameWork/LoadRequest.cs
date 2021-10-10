using System;
using UnityEngine;

namespace Game.Scripts.FrameWork
{
    public class LoadRequest
    {
        public bool IsDone { get; private set; }
        
        public bool Error { get; private set; }
        
        public bool Cancel { get; set; }

        public event Action<GameObject> OnComplete;
        
        public GameObject Result { get; private set; }

        public void SetResult(GameObject result)
        {
            Result = result;
        }
    }
}