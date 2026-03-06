using GameStates;

public interface ISubstateInitializer<TSubtateType>
{
    public void InitializeSubstates();
    public GameState GetSubstate(TSubtateType state);
}
