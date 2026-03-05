using UnityEngine;

[CreateAssetMenu(fileName = "Endgame Object Spawn Config", menuName = "Scriptable Objects/Endgame Objects/Endgame Object Spawn Config")]
public class EndgameObjectSpawnConfig : ScriptableObject
{
    [SerializeField] private EndgameObjectType objectType;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3 spawnPosition;    

    public EndgameObjectType ObjectType => objectType;
    public GameObject Prefab => prefab;
    public Vector3 SpawnPosition => spawnPosition;       
}
