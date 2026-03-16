using System.Collections.Generic;
using Spawn;
using UnityEngine;
using Units;

[System.Serializable]
public class SpawnableObject
{
    [SerializeField] private UnitType _type;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private SpawnPositioner _spawnPositioner;

    [Tooltip("The number of instances to keep in the object pool.")]
    [SerializeField, Range(0, 20)] private int _poolAmount = 10;

    public Queue<GameObject> Pool { get; } = new();

    public UnitType Type => _type;
    public GameObject Prefab => _prefab;
    public int PoolAmount => _poolAmount;

    public Vector3 GetSpawnPosition() => _spawnPositioner.GetPosition(this);
}
