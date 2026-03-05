using System.Collections;
using UnityEngine;

namespace Spawn
{
    public class InteractableObjectSpawner : Spawner, ISpawner
    {
        private readonly ISpawnableObjectProvider objectProvider;
        private readonly SpawnConfig config;
        private readonly Vector3 spawnPosition;

        protected override SpawnableObjectType ObjectType { get; set; } = SpawnableObjectType.BasicInteractable;

        public InteractableObjectSpawner(ISpawnableObjectProvider objectProvider, CoroutineRunner coroutineRunner, SpawnConfig config)
        {
            this.objectProvider = objectProvider;
            runner = coroutineRunner;
            spawnFrequency = config.InteractableSpawnFrequency;
            this.config = config;

            spawnPosition = config.SpawnPosition;
        }

        public override void StartSpawning()
        {
            if (spawnFrequency != config.InteractableSpawnFrequency) spawnFrequency = config.InteractableSpawnFrequency;
            base.StartSpawning();            
        }       

        protected override Vector3 GetPosition(SpawnableObject spawnObject)
        {
            float z = spawnPosition.z;
            float x = Random.Range(spawnObject.MinPosX, spawnObject.MaxPosX);
            float y = (Random.value < 0.5f ? spawnObject.MinPosY : spawnObject.MaxPosY);
            return new Vector3(x, y, z);
        }

        protected override SpawnableObject GetObject(SpawnableObjectType objType)
        {
            return objectProvider.GetObject(objType);
        }
    }
}
