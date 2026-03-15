public class CursorHandler
{
    private readonly EventManager eventManager;
    private readonly CursorActivator cursorActivator;

    public CursorHandler(EventManager eventManager, CursorActivator cursorActivator)
    {
        this.eventManager = eventManager;
        this.cursorActivator = cursorActivator;

        SubscribeToEvents();
    }

    public void OnEvent()
    {
        cursorActivator.DeactivateCursor();
    }



    private void SubscribeToEvents()
    {

    }
}
