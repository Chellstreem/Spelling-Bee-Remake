using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Spawn
{
    public class ObjectPool
    {
        private readonly Dictionary<SpawnableType, SpawnableObject> poolDictionary = new();
        private readonly Dictionary<GameObject, SpawnableObject> returnMap = new();
        private Transform _poolHolderTransform;

        public ObjectPool(SpawnConfig spawnConfig) => InitializePool(spawnConfig);

        public void InitializePool(SpawnConfig spawnConfig)
        {
            _poolHolderTransform = new GameObject("Spawnable Object Pool").GetComponent<Transform>();

            foreach (var spawnableObject in spawnConfig.SpawnableObjects)
            {
                SpawnableType type = spawnableObject.Type;

                if (!poolDictionary.ContainsKey(type))
                    poolDictionary[type] = spawnableObject;

                for (int i = 0; i < spawnableObject.Amount; i++)
                {
                    GameObject obj = Object.Instantiate(spawnableObject.Prefab, _poolHolderTransform);
                    obj.SetActive(false);

                    poolDictionary[type].Pool.Enqueue(obj);
                    returnMap[obj] = spawnableObject;
                }
            }
        }

        public SpawnableObject GetSpawnableObject(SpawnableType type)
        {
            if (!poolDictionary.ContainsKey(type))
            {
                Debug.Log($"Пул с именем группы {type} не найден!");
                return null;
            }

            return poolDictionary[type];
        }

        public void ReturnObject(GameObject gameObject)
        {
            if (!returnMap.TryGetValue(gameObject, out var spawnObject))
            {
                Debug.LogError($"Попытка вернуть объект {gameObject.name}, не принадлежащий пулу!");
                return;
            }

            gameObject.SetActive(false);
            gameObject.transform.position = _poolHolderTransform.position;
            poolDictionary[spawnObject.Type].Pool.Enqueue(gameObject);
        }
    }
}