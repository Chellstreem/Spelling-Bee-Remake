public class CursorHandler : IEventSubscriber<OnMovingStateEnter>, IEventSubscriber<OnMovingStateExit>
{
    private readonly EventManager eventManager;
    private readonly CursorActivator cursorActivator;

    public CursorHandler(EventManager eventManager, CursorActivator cursorActivator)
    {
        this.eventManager = eventManager;
        this.cursorActivator = cursorActivator;   
        
        SubscribeToEvents();
    }

    public void OnEvent(OnMovingStateEnter eventData)
    {
        cursorActivator.DeactivateCursor();
    }

    public void OnEvent(OnMovingStateExit eventData)
    {
        cursorActivator.ActivateCursor();
    }

    private void SubscribeToEvents()
    {
        eventManager.Subscribe<OnMovingStateEnter>(this);
        eventManager.Subscribe<OnMovingStateExit>(this);
    }
}
