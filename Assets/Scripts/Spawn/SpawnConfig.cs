using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Config", menuName = "Scriptable Objects/Spawn/Spawn Config")]
public class SpawnConfig : ScriptableObject
{
    [Header("Spawnable Objects")]
    [SerializeField] private SpawnableObject[] _spawnableObjects;

    [Header("Other")]
    [SerializeField] private Vector3 _spawnPosition;

    [SerializeField] private float _desiredDistance = 10f;

    public SpawnableObject[] SpawnableObjects => _spawnableObjects;
    public Vector3 SpawnPosition => _spawnPosition;
    public float InteractableSpawnFrequency => _desiredDistance;// / GameSpeedController.Speed;
}