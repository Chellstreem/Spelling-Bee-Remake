using UnityEngine;

namespace Spawn
{
    public enum SpawnPositionType
    {
        Random, Fixed
    }

    public abstract class SpawnPositioner : ScriptableObject
    {
        public abstract SpawnPositionType Type { get; }
        public abstract Vector3 GetPosition(SpawnableObject spawnableObject);
    }
}