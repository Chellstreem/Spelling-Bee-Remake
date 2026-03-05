using UnityEngine;

namespace Spawn
{
    public class DecorativeObjectSpawner : Spawner, ISpawner
    {
        private readonly ISpawnableObjectProvider objectPool;        
        private readonly Vector3 spawnPosition;

        protected override SpawnableObjectType ObjectType { get; set; } = SpawnableObjectType.Decorative;

        public DecorativeObjectSpawner(ISpawnableObjectProvider objectPool, CoroutineRunner coroutineRunner,
            SpawnConfig spawnConfig)
        {
            spawnFrequency = spawnConfig.DecorativeSpawnFrequency;
            this.objectPool = objectPool;            
            runner = coroutineRunner;
            spawnPosition = spawnConfig.SpawnPosition;
        }        

        protected override Vector3 GetPosition(SpawnableObject spawnObject)
        {
            float z = spawnPosition.z;
            float x = Random.Range(spawnObject.MinPosX, spawnObject.MaxPosX);
            float y = Random.Range(spawnObject.MinPosY, spawnObject.MaxPosY);
            return new Vector3(x, y, z);
        }        

        protected override SpawnableObject GetObject(SpawnableObjectType objType)
        {
            return objectPool.GetObject(objType);
        }
    }
}

