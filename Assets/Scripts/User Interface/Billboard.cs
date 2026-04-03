using UnityEngine;
using Zenject;

public abstract class Billboard : MonoBehaviour
{
    protected Camera _camera;
    protected Transform targetCameraTransform;
    [SerializeField, Range(-180, 180)] protected float horizontalAngleOffset = 0f;

    [Inject]
    public virtual void Construct(Camera activeCamera)
    {
        _camera = activeCamera;
    }

    protected void UpdateTargetCamera()
    {
        targetCameraTransform = _camera.transform;
    }

    private void LateUpdate()
    {
        Vector3 directionToCamera = targetCameraTransform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
        lookRotation *= Quaternion.Euler(0, horizontalAngleOffset, 0);
        transform.rotation = lookRotation;
    }
}
