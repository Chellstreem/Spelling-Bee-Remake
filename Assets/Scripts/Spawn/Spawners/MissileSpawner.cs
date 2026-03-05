using System.Collections;
using UnityEngine;

namespace Spawn
{
    public class MissileSpawner : Spawner, ISpawner
    {
        private readonly ISpawnableObjectProvider objectProvider;        
        private readonly Vector3 spawnPosition;
        private readonly float delayBeforeLaunching;

        protected override SpawnableObjectType ObjectType { get; set; } = SpawnableObjectType.Missile;

        public MissileSpawner(ISpawnableObjectProvider objectProvider, CoroutineRunner coroutineRunner,
            SpawnConfig spawnConfig, GameConfig gameConfig)
        {
            this.objectProvider = objectProvider;
            runner = coroutineRunner;
            spawnPosition = spawnConfig.SpawnPosition;
            spawnFrequency = gameConfig.MissileSpawnFrequency;
            delayBeforeLaunching = gameConfig.DelayBeforeLaunching;
        }         

        protected override IEnumerator SpawnObjects()
        {
            yield return new WaitForSeconds(delayBeforeLaunching);
            while (true)
            {
                SpawnableObject spawnObject = GetObject(ObjectType);
                GameObject obj = spawnObject.GameObject;
                spawnObject.CachedTransform.position = GetPosition(spawnObject);
                obj.SetActive(true);
                yield return new WaitForSeconds(spawnFrequency);
            }
        }

        protected override Vector3 GetPosition(SpawnableObject spawnObject)
        {
            float z = spawnPosition.z;
            float x = spawnObject.MinPosX;
            float y = (Random.value < 0.5f ? spawnObject.MinPosY : spawnObject.MaxPosY);

            return new Vector3(x, y, z);
        }

        protected override SpawnableObject GetObject(SpawnableObjectType objType)
        {
            return objectProvider.GetObject(objType);
        }
    }
}
