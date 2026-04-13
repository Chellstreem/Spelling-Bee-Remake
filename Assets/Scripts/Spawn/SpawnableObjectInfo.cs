using Spawn;
using UnityEngine;
using Units;

[System.Serializable]
public class SpawnableObjectInfo
{
    [Tooltip("Type of unit represented by this spawnable object")]
    [SerializeField] private UnitType _type;
    [Tooltip("Prefab used when spawning this object")]
    [SerializeField] private GameObject _prefab;
    [Tooltip("Positioner that controls where instances of this object are spawned")]
    [SerializeField] private SpawnPositioner _spawnPositioner;

    [Tooltip("The number of instances to keep in the object pool.")]
    [SerializeField, Range(0, 20)] private int _poolAmount = 10;

    public UnitType Type => _type;
    public GameObject Prefab => _prefab;
    public int PoolAmount => _poolAmount;

    public Vector3 GetSpawnPosition() => _spawnPositioner.GetPosition(this);
}
