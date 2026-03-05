using UnityEngine;

[CreateAssetMenu(fileName = "ScreenObjectConfig", menuName = "Scriptable Objects/Screen Objects/Screen ObjectConfig")]
public class ScreenObjectConfig : ScriptableObject
{
    [SerializeField] private ScreenObjectData[] screenObjects;
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private float pauseDuration = 0.5f;
    [SerializeField] private float timeInterval = 7f;
    
    public ScreenObjectData[] ScreenObjects => screenObjects;
    public float MoveDuration => moveDuration;
    public float PauseDuration => pauseDuration;
    public float TimeInterval => timeInterval;
}
