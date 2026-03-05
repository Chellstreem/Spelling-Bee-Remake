using System.Collections;
using UnityEngine;

namespace Spawn
{
    public class MonkeySpawner : Spawner, ISpawner
    {
        private readonly ISpawnableObjectProvider objectProvider;
        private readonly Vector3 spawnPosition;
        private readonly float minSpawnFrequency;
        private readonly float maxSpawnFrequency;

        protected override SpawnableObjectType ObjectType { get; set; } = SpawnableObjectType.CrazyMonkey;

        public MonkeySpawner(ISpawnableObjectProvider objectProvider, CoroutineRunner coroutineRunner, SpawnConfig config)
        {
            this.objectProvider = objectProvider;
            runner = coroutineRunner;
            minSpawnFrequency = config.MonkeySpawnFrequencyMin;
            maxSpawnFrequency = config.MonkeySpawnFrequencyMax;
            spawnPosition = config.SpawnPosition;
        }

        protected override IEnumerator SpawnObjects()
        {
            while (true)
            {
                spawnFrequency = Random.Range(minSpawnFrequency, maxSpawnFrequency);
                yield return new WaitForSeconds(spawnFrequency);
                if (Random.value > 0.6)
                {
                    SpawnableObject spawnObject = GetObject(ObjectType);
                    spawnObject.CachedTransform.position = GetPosition(spawnObject);
                    spawnObject.GameObject.SetActive(true);
                }
            }
        }

        protected override SpawnableObject GetObject(SpawnableObjectType objType)
        {
            return objectProvider.GetObject(objType);
        }

        protected override Vector3 GetPosition(SpawnableObject spawnObject)
        {
            float z = spawnPosition.z;
            float x = spawnObject.MinPosX;
            float y = spawnObject.MinPosY;

            return new Vector3(x, y, z);
        }
    }
}
