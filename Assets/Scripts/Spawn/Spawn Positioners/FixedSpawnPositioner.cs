using UnityEngine;
using Zenject;

namespace Spawn
{
    [CreateAssetMenu(fileName = "Fixed Spawn Positioner", menuName = "Scriptable Objects/Spawn/Fixed Spawn Positioner")]
    public class FixedSpawnPositioner : SpawnPositioner
    {
        [Tooltip("Fixed x coordinate used for spawned objects")]
        [SerializeField] private float _xPosition = 3f;
        [Tooltip("Fixed y coordinate used for spawned objects")]
        [SerializeField] private float _yPosition = 0f;
        private Vector3 _originalSpawnPosition;

        [Inject]
        public void Construct(GameConfig config) => _originalSpawnPosition = config.SpawnConfig.SpawnPosition;

        public override Vector3 GetPosition(SpawnableObjectInfo spawnableObject)
        {
            return new Vector3(_xPosition, _yPosition, _originalSpawnPosition.z);
        }
    }
}