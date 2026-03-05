using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Spawn
{
    public class EndgameObjectPool : IEndgameObjectGetter
    {
        private readonly EndgameObjectCollection specialObjects;
        private readonly DiContainer container;

        private Dictionary<EndgameObjectType, EndgameObject> poolDictionary; // Хранит гейм обжекты по типу объекта
        private Transform poolHolderTransform;

        public EndgameObjectPool(EndgameObjectCollection specialObjects, DiContainer container)
        {
            this.specialObjects = specialObjects;
            this.container = container;

            InitializePool();
        }

        public void InitializePool()
        {
            poolDictionary = new Dictionary<EndgameObjectType, EndgameObject>();
            poolHolderTransform = new GameObject("Endgame Object Pool").GetComponent<Transform>();

            foreach (EndgameObjectSpawnConfig config in specialObjects.SceneObjects)
            {
                EndgameObjectType objectType = config.ObjectType;

                if (!poolDictionary.ContainsKey(objectType))
                {
                    GameObject obj = container.InstantiatePrefab(config.Prefab, poolHolderTransform);
                    obj.SetActive(false);
                    EndgameObject endgameObject = new EndgameObject(obj, config.SpawnPosition);
                    poolDictionary.Add(objectType, endgameObject);
                }                   
            }
        }

        public EndgameObject GetObject(EndgameObjectType objectType)
        {
            if (!poolDictionary.ContainsKey(objectType))
            {
                Debug.Log($"Пул с именем группы {objectType} не найден!");
                return null;
            }

            return poolDictionary[objectType];
        }         
    }
}