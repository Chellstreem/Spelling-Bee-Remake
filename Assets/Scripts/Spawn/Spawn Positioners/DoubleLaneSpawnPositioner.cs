using UnityEngine;
using Zenject;

namespace Spawn
{
    [CreateAssetMenu(fileName = "Double Lane Positioner", menuName = "Scriptable Objects/Spawn/Double Lane Positioner")]
    public class DoubleLaneSpawnPositioner : SpawnPositioner
    {
        [Tooltip("X coordinate used for both lanes when spawning")]
        [SerializeField] private float _xPosition = 3f;
        [Tooltip("Y coordinate for the top lane spawn position")]
        [SerializeField] private float _topYPosition = 0f;
        [Tooltip("Y coordinate for the bottom lane spawn position")]
        [SerializeField] private float _bottomYPosition = 0f;
        private Vector3 _originalSpawnPosition;

        [Inject]
        public void Construct(GameConfig config) => _originalSpawnPosition = config.SpawnConfig.SpawnPosition;

        public override Vector3 GetPosition(SpawnableObjectInfo spawnableObject)
        {
            float yPosition = Random.value < 0.5f ? _topYPosition : _bottomYPosition;
            return new Vector3(_xPosition, yPosition, _originalSpawnPosition.z);
        }
    }
}