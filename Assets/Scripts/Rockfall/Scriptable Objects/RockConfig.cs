using UnityEngine;

[CreateAssetMenu(fileName = "Rock Config", menuName = "Scriptable Objects/Rocks/Rock Config")]
public class RockConfig : ScriptableObject
{
    [SerializeField] private RockCollection rockCollection;
    [SerializeField] private int rockAmount = 10;
    [SerializeField] private float rockFallSpeed = 18;
    [SerializeField] private float spawnInterval = 0.2f;
    [SerializeField] private float spawnRange = 15f;
    [SerializeField] private float xPositon = 9f;
    [SerializeField] private float yPosition = 15f;    

    public RockCollection RockCollection => rockCollection;
    public int RockAmount => rockAmount;
    public float RockFallSpeed => rockFallSpeed;
    public float SpawnInterval => spawnInterval;
    public float SpawnRange => spawnRange;
    public float XPositon => xPositon;
    public float YPosition => yPosition;    
}
