using System;
using UnityEngine;

namespace Game.Scripts.FrameWork
{
    public class SpawnRequest
    {
        public bool IsDone;

        public bool Error;

        public bool Cancel;

        public Action<GameObject> OnComplete;

        public GameObject Result;

    }
}