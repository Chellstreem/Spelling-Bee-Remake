using System.Collections;
using UnityEngine;

public class ScreenObjectHandler
{
    private readonly EventManager eventManager;
    private readonly CoroutineRunner coroutineRunner;
    private readonly IScreenObjectMover screenObjectMover;
    private readonly float interval;

    private Coroutine coroutine;


    public ScreenObjectHandler(EventManager eventManager, CoroutineRunner coroutineRunner,
        IScreenObjectMover screenObjectMover, ScreenObjectConfig config)
    {
        this.eventManager = eventManager;
        this.coroutineRunner = coroutineRunner;
        this.screenObjectMover = screenObjectMover;
        interval = config.TimeInterval;

        SubscribeToEvents();
    }

    private IEnumerator ScreenObjectCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (Random.value > 0.5)
                screenObjectMover.ShowScreenObject();
        }
    }

    private void StartCoroutine()
    {
        if (coroutine == null)
            coroutine = coroutineRunner.StartCoroutine(ScreenObjectCoroutine());
    }

    private void StopCoroutine()
    {
        if (coroutine != null)
        {
            coroutineRunner.Stop(coroutine);
            coroutine = null;
        }
    }

    private void SubscribeToEvents()
    {

    }
}
