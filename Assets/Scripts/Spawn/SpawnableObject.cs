using System.Collections.Generic;
using Spawn;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    [SerializeField] private GameCharacterType _type;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private SpawnPosition _spawnPosition;

    [Range(0, 20)]
    [SerializeField] private int _amount;

    public Queue<GameObject> Pool { get; } = new();

    public GameCharacterType Type => _type;
    public GameObject Prefab => _prefab;
    public int Amount => _amount;

    public void GetSpawnPosition() => _spawnPosition.GetPosition(this);
}
