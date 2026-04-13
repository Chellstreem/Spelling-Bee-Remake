using UnityEngine;

[CreateAssetMenu(fileName = "Camera State", menuName = "Scriptable Objects/Camera/Camera State")]
public class CameraState : ScriptableObject
{
    [Tooltip("Local position of the camera in this camera state")]
    [SerializeField] private Vector3 _cameraPosition;
    [Tooltip("Local rotation (Euler angles) for this camera state")]
    [SerializeField] private Vector3 _cameraRotation;
    [Tooltip("Duration of transition to this state in seconds")]
    [SerializeField] private float _transitionDuration = 1.5f;

    public Vector3 CameraPosition => _cameraPosition;
    public Vector3 CameraRotation => _cameraRotation;
    public float TransitionDuration => _transitionDuration;
}
