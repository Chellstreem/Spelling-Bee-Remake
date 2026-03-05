using UnityEngine;

[CreateAssetMenu(fileName = "ScreenObjectData", menuName = "Scriptable Objects/Screen Objects/Screen Object Data")]
public class ScreenObjectData : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3 startLocalPosition;
    [SerializeField] private Vector3 targetLocalPosition;
    [SerializeField] private Vector3 targetLocalRotation;
    
    public GameObject Prefab => prefab;
    public Vector3 StartLocalPosition => startLocalPosition;
    public Vector3 TargetLocalPosition => targetLocalPosition;
    public Vector3 TargetLocalRotation => targetLocalRotation;
}
