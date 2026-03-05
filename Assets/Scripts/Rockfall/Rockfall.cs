using System.Collections;
using UnityEngine;
using Zenject;

public class Rockfall : IRockDropper
{
    private readonly CoroutineRunner coroutineRunner;
    private readonly IRockGetter rockGetter;
    private readonly Transform playerTransform;
    private readonly float spawnInterval;
    private readonly float spawnRange;
    private readonly float xPosition = 7f;
    private readonly float yPosition = 15f;

    private Coroutine coroutine;

    public Rockfall
        (CoroutineRunner coroutineRunner,
        IRockGetter rockGetter,
        [Inject(Id = InstantiatedObjectType.Player)] Transform playerTransform,
        RockConfig config)
    {
        this.coroutineRunner = coroutineRunner;
        this.rockGetter = rockGetter;
        this.playerTransform = playerTransform;
        spawnInterval = config.SpawnInterval;
        spawnRange = config.SpawnRange;
        xPosition = config.XPositon;
        yPosition = config.YPosition;
    }

    public void StartRockFall()
    {
        if (coroutine == null) coroutine = coroutineRunner.StartCor(RockFallingCoroutine());
    }

    public void StopRockFall()
    {
        if (coroutine != null)
        {
            coroutineRunner.StopCor(coroutine);
            coroutine = null;
        }           
    }

    private IEnumerator RockFallingCoroutine()
    {                
        float zPosition = playerTransform.position.z;

        while (true)
        {
            float randomOffsetZ = Random.Range(-spawnRange, spawnRange);
            Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition + randomOffsetZ);        
            GameObject rock = rockGetter.GetRock();
            rock.transform.position = spawnPosition;
            rock.SetActive(true);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
