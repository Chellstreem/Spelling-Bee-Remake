using System.Collections;
using UnityEngine;

namespace Spawn
{
    public class Spawner
    {
        private readonly ObjectPool objectPool;

        public Spawner(ObjectPool objectPool) => this.objectPool = objectPool;

        public GameObject SpawnObject(SpawnableType type)
        {
            var spawnableObject = objectPool.GetSpawnableObject(type);
            GameObject obj = spawnableObject.Pool.Dequeue();
            obj.SetActive(true);

            obj.transform.position = spawnableObject.GetSpawnPosition();
            return obj;
        }
    }
}
