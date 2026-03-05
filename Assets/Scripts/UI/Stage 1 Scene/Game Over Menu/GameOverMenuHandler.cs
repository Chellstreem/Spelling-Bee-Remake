using DG.Tweening;
using UnityEngine;
using Zenject;

public class GameOverMenuHandler : IInitializable, IEventSubscriber<OnVictoryStateEnter>,
    IEventSubscriber<OnLossStateEnter>
{
    private readonly EventManager eventManager;
    private readonly IScaler scaler;
    private readonly RectTransform gameoverMenu;
    
    public GameOverMenuHandler(EventManager eventManager, IScaler scaler,
        [Inject(Id = UiObjectType.GameoverMenu)] RectTransform gameoverMenu)
    {
        this.eventManager = eventManager;
        this.scaler = scaler;
        this.gameoverMenu = gameoverMenu;
    }

    public void Initialize()
    {
        SubscribeToEvents();
    }

    public void OnEvent(OnVictoryStateEnter eventData)
    {
        scaler.ActivateWithScale(gameoverMenu, 1f, 0, Ease.Linear);
    }

    public void OnEvent(OnLossStateEnter eventData)
    {
        scaler.ActivateWithScale(gameoverMenu, 1f, 0, Ease.Linear);
    }

    private void SubscribeToEvents()
    {
        eventManager.Subscribe<OnVictoryStateEnter>(this);
        eventManager.Subscribe<OnLossStateEnter>(this);
    }

    private void UnsubscribeFromEvents()
    {
        eventManager.Unsubscribe<OnVictoryStateEnter>(this);
        eventManager.Unsubscribe<OnLossStateEnter>(this);
    }
}
