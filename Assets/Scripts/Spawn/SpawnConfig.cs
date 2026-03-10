using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Config", menuName = "Scriptable Objects/Spawn/Spawn Config")]
public class SpawnConfig : ScriptableObject
{
    [Header("Spawnable Objects")]
    [SerializeField] private SpawnableObject[] _spawnableObjects;

    [Header("Other")]
    [SerializeField] private Vector3 _spawnPosition;

    public SpawnableObject[] SpawnableObjects => _spawnableObjects;
    public Vector3 SpawnPosition => _spawnPosition;
}