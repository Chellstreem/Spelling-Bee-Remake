using GameStates;
using UnityEngine;

namespace CameraControl
{
    public class CameraController
    {
        private readonly Transform cameraTransfrom;
        private readonly CameraMover cameraMover;
        private readonly GameStateController stateController;

        public CameraController(Camera camera, CameraMover cameraMover, GameStateController stateController)
        {
            cameraTransfrom = camera.transform;
            this.cameraMover = cameraMover;
            this.stateController = stateController;

            SubscribeToEvents();
        }

        private void OnStateChanged()
        {
            switch (stateController.CurrentState.StateType)
            {
                case GameStateType.Victory:
                    cameraMover.SetState(cameraTransfrom, CameraStateType.Move);
                    break;
                case GameStateType.Loss:
                    cameraMover.SetState(cameraTransfrom, CameraStateType.Move);
                    break;
                case GameStateType.Countdown:
                    cameraMover.SetState(cameraTransfrom, CameraStateType.Move);
                    break;
                default:
                    cameraMover.SetState(cameraTransfrom, CameraStateType.Move);
                    break;
            }
        }

        private void SubscribeToEvents() => stateController.OnStateChanged += OnStateChanged;
        private void UnsubscribeFromEvents() => stateController.OnStateChanged -= OnStateChanged;
    }
}
