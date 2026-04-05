using Units;
using UnityEngine;

namespace Spawn
{
    public class Spawner
    {
        private readonly UnitPool pool;

        public Spawner(UnitPool unitPool) => pool = unitPool;

        public GameObject SpawnObject(UnitType type)
        {
            var pool = this.pool.GetPool(type);
            GameObject obj = pool.Pool.Dequeue();
            obj.transform.position = pool.Info.GetSpawnPosition();
            obj.SetActive(true);

            return obj;
        }
    }
}
