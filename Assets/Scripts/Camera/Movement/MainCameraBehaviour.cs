using GameStates;

namespace Cameras
{
    public class MainCameraBehaviour
    {
        private readonly GameStateController _stateController;
        private readonly EventManager eventManager;
        private readonly ISingleCameraMover cameraMover;

        public MainCameraBehaviour(GameStateController stateController, EventManager eventManager, ISingleCameraMover cameraMover)
        {
            _stateController = stateController;
            this.eventManager = eventManager;
            this.cameraMover = cameraMover;

            SubscribeToEvents();
        }

        private void OnStateChanged()
        {
            switch (_stateController.CurrentState.StateType)
            {
                case GameStateType.Victory:
                    cameraMover.ChangeStateSmoothly(CameraStateType.Victory, 1.5f);
                    break;
                case GameStateType.Loss:
                    cameraMover.ChangeStateSmoothly(CameraStateType.Loss, 1.5f);
                    break;
                case GameStateType.Countdown:
                    cameraMover.ChangeState(CameraStateType.Start);
                    break;
                default:
                    cameraMover.ChangeStateSmoothly(CameraStateType.Move, 1f);
                    break;
            }
        }

        private void SubscribeToEvents() => _stateController.OnStateChanged += OnStateChanged;
        private void UnsubscribeFromEvents() => _stateController.OnStateChanged -= OnStateChanged;
    }
}
