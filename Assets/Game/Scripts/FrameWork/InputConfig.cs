using UnityEngine;

namespace Game.Scripts.FrameWork
{
    [CreateAssetMenu(fileName ="input_config", menuName ="Input Config")]

    public class InputConfig : ScriptableObject
    {
        public float DragStartThreshold = 5f;

        public float DragOffsetBase = 1000f;

    }
}