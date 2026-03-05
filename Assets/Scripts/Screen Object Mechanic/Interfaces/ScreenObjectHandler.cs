using System.Collections;
using UnityEngine;

public class ScreenObjectHandler : IEventSubscriber<OnMovingStateEnter>, IEventSubscriber<OnMovingStateExit>
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

    public void OnEvent(OnMovingStateEnter eventData) => StartCoroutine();
    public void OnEvent(OnMovingStateExit eventData) => StopCoroutine();

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
            coroutine = coroutineRunner.StartCor(ScreenObjectCoroutine());
    }

    private void StopCoroutine()
    {
        if (coroutine != null)
        {
            coroutineRunner.StopCor(coroutine);
            coroutine = null;
        }
    }

    private void SubscribeToEvents()
    {
        eventManager.Subscribe<OnMovingStateEnter>(this);
        eventManager.Subscribe<OnMovingStateExit>(this);
    }
}
