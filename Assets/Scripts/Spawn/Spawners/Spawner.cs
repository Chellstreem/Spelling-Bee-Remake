using System.Collections;
using UnityEngine;

namespace Spawn
{
    public abstract class Spawner
    {
        protected CoroutineRunner runner;
        protected float spawnFrequency;

        protected abstract SpawnableObjectType ObjectType { get; set; }
        private Coroutine spawnCoroutine;

        public virtual void StartSpawning()
        {
            StopSpawning();
            spawnCoroutine = runner.StartCor(SpawnObjects());
        }

        public virtual void StopSpawning()
        {
            if (spawnCoroutine != null)
            {
                runner.StopCor(spawnCoroutine);
                spawnCoroutine = null;
            }
        }

        protected virtual IEnumerator SpawnObjects()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnFrequency);
                SpawnableObject spawnObject = GetObject(ObjectType);                
                spawnObject.CachedTransform.position = GetPosition(spawnObject);
                spawnObject.GameObject.SetActive(true);
            }
        }

        protected abstract Vector3 GetPosition(SpawnableObject spawnObject);

        protected abstract SpawnableObject GetObject(SpawnableObjectType objType);
    }
}
