using Units;
using UnityEngine;

namespace Spawn
{
    public class Spawner
    {
        private readonly ObjectPool objectPool;

        public Spawner(ObjectPool objectPool) => this.objectPool = objectPool;

        public GameObject SpawnObject(UnitType type)
        {
            var spawnableObject = objectPool.GetSpawnableObject(type);
            GameObject obj = spawnableObject.Pool.Dequeue();
            obj.transform.position = spawnableObject.GetSpawnPosition();
            obj.SetActive(true);

            return obj;
        }
    }
}
