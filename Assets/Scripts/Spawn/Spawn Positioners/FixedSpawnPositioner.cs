using UnityEngine;
using Zenject;

namespace Spawn
{
    [CreateAssetMenu(fileName = "Fixed Spawn Positioner", menuName = "Scriptable Objects/Spawn/Fixed Spawn Positioner")]
    public class FixedSpawnPositioner : SpawnPositioner
    {
        [SerializeField] private float _xPosition = 3f;
        [SerializeField] private float _yPosition = 0f;
        private Vector3 _originalSpawnPosition;

        [Inject]
        public void Construct(GameConfig config) => _originalSpawnPosition = config.SpawnConfig.SpawnPosition;

        public override Vector3 GetPosition(SpawnableObject spawnableObject)
        {
            return new Vector3(_xPosition, _yPosition, _originalSpawnPosition.z);
        }
    }
}