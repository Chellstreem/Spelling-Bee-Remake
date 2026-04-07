using UnityEngine;

[CreateAssetMenu(fileName = "Camera State", menuName = "Scriptable Objects/Camera/Camera State")]
public class CameraState : ScriptableObject
{
    [SerializeField] private Vector3 _cameraPosition;
    [SerializeField] private Vector3 _cameraRotation;
    [SerializeField] private float _transitionDuration = 1.5f;

    public Vector3 CameraPosition => _cameraPosition;
    public Vector3 CameraRotation => _cameraRotation;
    public float TransitionDuration => _transitionDuration;
}
