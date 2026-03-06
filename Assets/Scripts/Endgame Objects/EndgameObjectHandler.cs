using GameStates;

public class EndgameObjectHandler
{
    private readonly GameStateController _stateController;
    private readonly IEndgameObjectActivator _objectActivator;

    public EndgameObjectHandler(GameStateController stateController, IEndgameObjectActivator objectActivator)
    {
        _stateController = stateController;
        _objectActivator = objectActivator;

        SubscribeToEvents();
    }

    private void OnStateChanged()
    {
        switch (_stateController.CurrentState.StateType)
        {
            case GameStateType.Victory:
                _objectActivator.ActivateObject(EndgameObjectType.Victory);
                break;
            case GameStateType.Loss:
                _objectActivator.ActivateObject(EndgameObjectType.Loss);
                break;
        }
    }

    private void SubscribeToEvents()
    {
        _stateController.OnStateChanged += OnStateChanged;
    }
}
