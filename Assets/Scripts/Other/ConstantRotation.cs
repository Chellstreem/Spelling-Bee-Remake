using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private float _speed = 120f;
    [SerializeField] private Vector3 _rotationAxis = Vector3.up;

    private void Update() => transform.Rotate(_rotationAxis, _speed * Time.deltaTime);
}
