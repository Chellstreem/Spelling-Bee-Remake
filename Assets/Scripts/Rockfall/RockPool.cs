using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RockPool : IRockGetter, IRockReturner, IInitializable
{
    private readonly RockCollection collection;
    private readonly DiContainer container;
    private readonly int amount;

    private List<GameObject> rockList;
    private Transform poolHolderTransform;    

    public RockPool(RockConfig config, DiContainer container)
    {
        collection = config.RockCollection;
        this.container = container;
        amount = config.RockAmount;        
    }

    public void Initialize() => InitializePool();    

    public void InitializePool()
    {
        rockList = new List<GameObject>();
        poolHolderTransform = new GameObject("Rock Pool").GetComponent<Transform>();

        foreach (GameObject rockPrefab in collection.Rocks)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject newRock = container.InstantiatePrefab(rockPrefab, poolHolderTransform);
                newRock.SetActive(false);
                rockList.Add(newRock);
            }
        }
    }

    public GameObject GetRock()
    {
        if (rockList.Count > 0)
        {
            int randomIndex = Random.Range(0, rockList.Count);
            GameObject rock = rockList[randomIndex];
            rockList[randomIndex] = rockList[rockList.Count - 1];
            rockList.RemoveAt(rockList.Count - 1);

            return rock;
        }
        else
        {
            Debug.LogWarning($"Пул камней пуст!");
            return null;
        }
    }

    public void ReturnRock(GameObject gameObject)
    {
        gameObject.SetActive(false);
        rockList.Add(gameObject);
    }

}
