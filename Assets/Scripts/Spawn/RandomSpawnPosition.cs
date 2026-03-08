using UnityEngine;

namespace Spawn
{
    public class RandomSpawnPosition : SpawnPosition
    {
        [Header("Spawn Range")]
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;
        public override SpawnPositionType Type => SpawnPositionType.Random;

        public override Vector3 GetPosition(SpawnableObject spawnableObject)
        {
            throw new System.NotImplementedException();
        }
    }
}