using UnityEngine;

public class RotationEffect : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    [SerializeField] private float _speed = 120f;
    [Tooltip("Axis around which the object will rotate")]
    [SerializeField] private Vector3 _rotationAxis = Vector3.up;

    private void Update() => transform.Rotate(_rotationAxis, _speed * Time.deltaTime);
}
