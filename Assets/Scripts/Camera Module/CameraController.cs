using GameModules;
using GameStates;
using UnityEngine;

namespace CameraControl
{
    public class CameraController
    {
        private readonly CameraModule _module;
        private readonly GameStateController stateController;
        private readonly Transform cameraTransfrom;
        private readonly CameraMover cameraMover;

        public CameraController(CameraModule cameraModule, Camera camera, CameraMover cameraMover, GameStateController stateController)
        {
            _module = cameraModule;
            cameraTransfrom = camera.transform;
            this.cameraMover = cameraMover;
            this.stateController = stateController;

            stateController.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            CameraState state = stateController.CurrentState.Definition.CameraState;
            cameraMover.SetState(cameraTransfrom, state != null ? state : _module.DefaultState);
        }

        public void Dispose() => stateController.OnStateChanged -= OnStateChanged;
    }
}
