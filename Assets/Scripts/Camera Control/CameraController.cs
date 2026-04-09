using GameStates;
using UnityEngine;

namespace CameraControl
{
    public class CameraController
    {
        private readonly CameraConfig config;
        private readonly Transform cameraTransfrom;
        private readonly CameraMover cameraMover;
        private readonly GameStateController stateController;

        public CameraController(GameConfig config, Camera camera, CameraMover cameraMover, GameStateController stateController)
        {
            this.config = config.CameraConfig;
            cameraTransfrom = camera.transform;
            this.cameraMover = cameraMover;
            this.stateController = stateController;

            stateController.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            CameraState state = stateController.CurrentState.Definition.CameraState;
            cameraMover.SetState(cameraTransfrom, state != null ? state : config.DefaultState);
        }
    }
}
