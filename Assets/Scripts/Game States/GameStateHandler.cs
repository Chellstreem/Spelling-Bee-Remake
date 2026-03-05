using Zenject;

public class GameStateHandler : IInitializable
{
    private readonly IStateInitializer stateInitializer;
    private readonly IStateSwitcher stateSwitcher;

    public GameStateHandler(IStateInitializer stateInitializer, IStateSwitcher stateSwitcher)
    {
        this.stateInitializer = stateInitializer;
        this.stateSwitcher = stateSwitcher;        
    }

    public void Initialize()
    {
        stateInitializer.InitializeStates();
        stateSwitcher.SetState(GameStates.GameState.Countdown);
    }    
}
