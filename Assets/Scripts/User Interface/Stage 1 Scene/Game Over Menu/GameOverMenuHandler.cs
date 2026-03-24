using DG.Tweening;
using GameStates;
using UnityEngine;
using Zenject;

public class GameOverMenuHandler : IInitializable
{
    private readonly GameStateController _stateController;
    private readonly ScaleEffect scaler;
    private readonly RectTransform gameoverMenu;

    public GameOverMenuHandler(GameStateController stateController, ScaleEffect scaler,
        [Inject(Id = UiObjectType.GameoverMenu)] RectTransform gameoverMenu)
    {
        _stateController = stateController;
        this.scaler = scaler;
        this.gameoverMenu = gameoverMenu;
    }

    public void Initialize() => SubscribeToEvents();

    private void OnStateChanged()
    {
        switch (_stateController.CurrentState.StateType)
        {
            case GameStateType.Victory:
                scaler.ActivateWithScale(gameoverMenu, 1f, 0, Ease.Linear);
                break;
            case GameStateType.Loss:
                scaler.ActivateWithScale(gameoverMenu, 1f, 0, Ease.Linear);
                break;
        }
    }

    private void SubscribeToEvents() => _stateController.OnStateChanged += OnStateChanged;
    private void UnsubscribeFromEvents() => _stateController.OnStateChanged -= OnStateChanged;
}
