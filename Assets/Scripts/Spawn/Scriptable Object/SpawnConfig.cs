using UnityEngine;

[CreateAssetMenu(fileName = "SpawnConfig", menuName = "Scriptable Objects/Spawn/SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
    [Header("Array of Spawnable Objects")]
    [SerializeField] private SpawnableObjectConfig[] spawnableObjects;

    [Header("Other")]    
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float decorativeSpawnFrequency = 1.5f;
    [SerializeField] private float monkeySpawnFrequencyMin = 3f;
    [SerializeField] private float monkeySpawnFrequencyMax = 6f;
    [SerializeField] private float desiredDistance = 10f;    
    
    public SpawnableObjectConfig[] SpawnableObjects => spawnableObjects;    
    public Vector3 SpawnPosition => spawnPosition;
    public float DecorativeSpawnFrequency => decorativeSpawnFrequency;
    public float MonkeySpawnFrequencyMin => monkeySpawnFrequencyMin;
    public float MonkeySpawnFrequencyMax => monkeySpawnFrequencyMax;
    public float InteractableSpawnFrequency => desiredDistance / GameSpeed.Speed;
}