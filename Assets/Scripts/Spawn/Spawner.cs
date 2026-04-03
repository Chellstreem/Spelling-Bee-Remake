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
            var pool = objectPool.GetPool(type);
            GameObject obj = pool.Pool.Dequeue();
            obj.transform.position = pool.Info.GetSpawnPosition();
            obj.SetActive(true);

            return obj;
        }
    }
}
