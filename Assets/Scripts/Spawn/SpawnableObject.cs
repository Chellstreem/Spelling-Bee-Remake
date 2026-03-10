using System.Collections.Generic;
using Spawn;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    [SerializeField] private SpawnableType _type;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private SpawnPositioner _spawnPositioner;

    [Range(0, 20)]
    [SerializeField] private int _amount;

    public Queue<GameObject> Pool { get; } = new();

    public SpawnableType Type => _type;
    public GameObject Prefab => _prefab;
    public int Amount => _amount;

    public Vector3 GetSpawnPosition() => _spawnPositioner.GetPosition(this);
}
