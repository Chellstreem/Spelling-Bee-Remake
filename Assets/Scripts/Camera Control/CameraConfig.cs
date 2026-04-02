using UnityEngine;

namespace CameraControl
{
    [CreateAssetMenu(fileName = "Camera Config", menuName = "Scriptable Objects/Camera/Camera Config")]
    public class CameraConfig : ScriptableObject
    {
        [SerializeField] private CameraState _defaultCameraState;
        public CameraState DefaultState => _defaultCameraState;
    }
}
