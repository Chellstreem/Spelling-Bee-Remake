using GameModules;
using GameStateModule;
using UnityEngine;

namespace CameraModule
{
    public class CameraController
    {
        private readonly CameraConfig config;
        private readonly GameStateController stateController;
        private readonly Transform cameraTransfrom;
        private readonly CameraMover cameraMover;

        public CameraController(CameraConfig config, Camera camera, CameraMover cameraMover, GameStateController stateController)
        {
            this.config = config;
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

        public void Dispose() => stateController.OnStateChanged -= OnStateChanged;
    }
}
