using UnityEngine;
using Zenject;

namespace Spawn
{
    [CreateAssetMenu(fileName = "Random Spawn Positioner", menuName = "ScriptableObjects/Spawn/Random Spawn Positioner")]
    public class RandomSpawnPositioner : SpawnPositioner
    {
        [Header("Spawn Range")]
        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;
        private Vector3 _originalSpawnPosition;

        public override SpawnPositionType Type => SpawnPositionType.Random;

        [Inject]
        public void Construct(GameConfig config) => _originalSpawnPosition = config.SpawnConfig.SpawnPosition;

        public override Vector3 GetPosition(SpawnableObject spawnableObject)
        {
            float x = Random.Range(_minX, _maxX);
            float y = Random.Range(_minY, _maxY);
            float z = _originalSpawnPosition.z;

            return new Vector3(x, y, z);
        }
    }
}