using UnityEngine;

namespace Spawn
{
    public enum SpawnPositionType
    { Random, Fixed }

    public abstract class SpawnPositioner : ScriptableObject
    {
        public abstract Vector3 GetPosition(SpawnableObjectInfo spawnableObject);
    }
}