using UnityEngine;
using Zenject;

namespace Spawn
{
    [CreateAssetMenu(fileName = "Random Spawn Positioner", menuName = "Scriptable Objects/Spawn/Random Spawn Positioner")]
    public class RandomSpawnPositioner : SpawnPositioner
    {
        [Header("Spawn Range")]
        [Tooltip("Minimum x coordinate for random spawn range")]
        [SerializeField] private float _minX;
        [Tooltip("Maximum x coordinate for random spawn range")]
        [SerializeField] private float _maxX;
        [Tooltip("Y coordinate used for spawned objects")]
        [SerializeField] private float _yPosition;
        private Vector3 _originalSpawnPosition;

        [Inject]
        public void Construct(GameConfig config) => _originalSpawnPosition = config.SpawnConfig.SpawnPosition;

        public override Vector3 GetPosition(SpawnableObjectInfo spawnableObject)
        {
            float x = Random.Range(_minX, _maxX);
            return new Vector3(x, _yPosition, _originalSpawnPosition.z);
        }
    }
}