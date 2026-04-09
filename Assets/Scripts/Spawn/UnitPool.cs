using System.Collections.Generic;
using Units;
using UnityEngine;
using Zenject;

namespace Spawn
{
    public class UnitPool
    {
        private readonly DiContainer container;
        private readonly SpawnConfig config;
        private readonly Dictionary<UnitType, SpawnableObjectPool> poolDictionary = new();
        private readonly Dictionary<GameObject, SpawnableObjectPool> returnMap = new();
        private Transform _poolHolderTransform;

        public UnitPool(DiContainer container, SpawnConfig config)
        {
            this.config = config;
            this.container = container;
        }

        public void InitializePool()
        {
            _poolHolderTransform = new GameObject("Unit Pool").GetComponent<Transform>();

            foreach (var spawnableObject in config.SpawnableObjects)
            {
                UnitType type = spawnableObject.Type;

                if (!poolDictionary.ContainsKey(type))
                {
                    var pool = new SpawnableObjectPool(spawnableObject);
                    poolDictionary[type] = pool;
                }

                for (int i = 0; i < spawnableObject.PoolAmount; i++)
                {
                    GameObject obj = container.InstantiatePrefab(spawnableObject.Prefab, _poolHolderTransform);
                    obj.SetActive(false);

                    poolDictionary[type].Pool.Enqueue(obj);
                    returnMap[obj] = poolDictionary[type];
                }
            }
        }

        public SpawnableObjectPool GetPool(UnitType type)
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
            if (!returnMap.TryGetValue(gameObject, out var pool))
            {
                Debug.LogError($"Попытка вернуть объект {gameObject.name}, не принадлежащий пулу!");
                return;
            }

            gameObject.SetActive(false);
            gameObject.transform.position = _poolHolderTransform.position;
            poolDictionary[pool.Info.Type].Pool.Enqueue(gameObject);
        }
    }
}