using CameraControl;
using GameStates;
using Installers;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Camera Module", menuName = "Scriptable Objects/Services/Camera Module")]
    public class CameraModule : GameModule
    {
        public override void Install(GameServices services, Installer installer, GameConfig config)
        {
            Camera camera = installer.Camera;

            installer.DiContainer.Bind<Camera>()
               .FromInstance(camera)
               .AsSingle()
               .NonLazy();

            CameraMover cameraMover = new(services.Get<CoroutineRunner>());
            var stateController = services.Get<GameStateController>();

            CameraController cameraController = new(config.CameraConfig, camera, cameraMover, stateController);
        }
    }
}