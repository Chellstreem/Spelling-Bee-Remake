using GameStates;
public interface IStateSwitcher
{
    public GameState CurrentState { get; }
    public void SetState(GameStateType state);
}
