using UnityEngine;

namespace Game.Scripts.Map
{
    [CreateAssetMenu(fileName ="camera_config", menuName ="Camera Config")]
    public class MapCameraConfig : ScriptableObject
    {
        public Vector3 PlaneNormal = Vector3.up;

        public Vector3 InitPosition = new Vector3(50, 10, 50);
        public float XAngle = 45f;

        public float TranslateSpeed = 10;
        public float ScrollSpeed = 10;
    }
}