using GameStates;

public interface IStateInitializer
{
    public void InitializeStates();
    public GameState GetGameState(GameStateType gameState);
}