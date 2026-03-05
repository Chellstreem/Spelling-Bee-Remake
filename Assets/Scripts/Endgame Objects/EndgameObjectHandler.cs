using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameObjectHandler : IEventSubscriber<OnLossStateEnter>, IEventSubscriber<OnVictoryStateEnter>
{
    private readonly EventManager eventManager;
    private readonly IEndgameObjectActivator objectActivator;

    public EndgameObjectHandler(EventManager eventManager, IEndgameObjectActivator objectActivator)
    {
        this.eventManager = eventManager;
        this.objectActivator = objectActivator;

        SubscribeToEvents();
    }

    public void OnEvent(OnLossStateEnter eventData)
    {
        objectActivator.ActivateObject(EndgameObjectType.Loss);
    }

    public void OnEvent(OnVictoryStateEnter eventData)
    {
        objectActivator.ActivateObject(EndgameObjectType.Victory);
    }

    private void SubscribeToEvents()
    {
        eventManager.Subscribe<OnLossStateEnter>(this);
        eventManager.Subscribe<OnVictoryStateEnter>(this);
    }
}
