using UnityEngine;

namespace CameraControl
{
    [CreateAssetMenu(fileName = "Camera Config", menuName = "Scriptable Objects/Camera/Camera Config")]
    public class CameraConfig : ScriptableObject
    {
        [SerializeField] private CameraState[] _states;
        [SerializeField] private float _cameraShakeIntensity;
        [SerializeField] private float _cameraShakeDuration;

        public CameraState[] States => _states;
        public float CameraShakeIntensity => _cameraShakeIntensity;
        public float CameraShakeDuration => _cameraShakeDuration;
    }
}
