using System.Collections;
using UnityEngine;

namespace Spawn
{
    public class Spawner
    {
        private readonly ObjectPool objectPool;
        private readonly CoroutineRunner runner;
        private readonly Vector3 spawnPosition;
        private Coroutine _spawnCoroutine;

        public Spawner(ObjectPool objectPool, CoroutineRunner runner, GameConfig gameConfig)
        {
            this.objectPool = objectPool;
            this.runner = runner;
            spawnPosition = gameConfig.SpawnConfig.SpawnPosition;
        }

        public void Spawn(GameCharacterType type)
        {
            var spawnableObject = objectPool.GetSpawnableObject(type);
            GameObject obj = spawnableObject.Pool.Dequeue();
            obj.SetActive(true);

            obj.transform.position = spawnPosition;
        }


    }
}
