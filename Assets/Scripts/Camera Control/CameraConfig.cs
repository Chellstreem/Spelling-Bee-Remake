using UnityEngine;

namespace CameraControl
{
    [CreateAssetMenu(fileName = "Camera Config", menuName = "Scriptable Objects/Camera/Camera Config")]
    public class CameraConfig : ScriptableObject
    {
        [Tooltip("Default camera state used when the scene starts or when camera resets")]
        [SerializeField] private CameraState _defaultCameraState;
        public CameraState DefaultState => _defaultCameraState;
    }
}
