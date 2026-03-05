using UnityEngine;

public class ScreenObject
{    
    public GameObject GameObj { get; }
    public Vector3 StartLocalPosition { get; }
    public Vector3 TargetLocalPosition { get; }
    public Vector3 TargetLocalRotation { get; }

    public ScreenObject(GameObject gameObj, Vector3 startLocalPosition, Vector3 targetLocalPosition, Vector3 targetLocalRotation)
    {
        GameObj = gameObj;
        StartLocalPosition = startLocalPosition;
        TargetLocalPosition = targetLocalPosition;
        TargetLocalRotation = targetLocalRotation;
    }
}
