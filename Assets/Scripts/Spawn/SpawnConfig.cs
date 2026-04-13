using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Config", menuName = "Scriptable Objects/Spawn/Spawn Config")]
public class SpawnConfig : ScriptableObject
{
    [Header("Spawnable Objects")]
    [Tooltip("Definitions of objects/units that can be spawned by the spawner")]
    [SerializeField] private SpawnableObjectInfo[] _spawnableObjects;

    [Header("Other")]
    [Tooltip("World position used as the base spawn origin for spawned objects")]
    [SerializeField] private Vector3 _spawnPosition;

    public SpawnableObjectInfo[] SpawnableObjects => _spawnableObjects;
    public Vector3 SpawnPosition => _spawnPosition;
}