using CameraControl;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraState", menuName = "Scriptable Objects/Camera/CameraState")]
public class CameraState : ScriptableObject
{
    [SerializeField] private CameraStateType _state;
    [SerializeField] private Vector3 _cameraPosition;
    [SerializeField] private Vector3 _cameraRotation;
    [SerializeField] private float _transitionDuration = 1.5f;

    public CameraStateType State => _state;
    public Vector3 CameraPosition => _cameraPosition;
    public Vector3 CameraRotation => _cameraRotation;
    public float TransitionDuration => _transitionDuration;
}
