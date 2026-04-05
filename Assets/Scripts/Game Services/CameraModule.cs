using CameraControl;
using GameStates;
using Installers;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Camera Module", menuName = "Scriptable Objects/Services/Camera Module")]
    public class CameraModule : GameModule
    {
        public override void Install(GameContext context, MainStageInstaller installer, GameConfig config)
        {
            Camera camera = installer.Camera;

            installer.DiContainer.Bind<Camera>()
               .FromInstance(camera)
               .AsSingle()
               .NonLazy();

            CameraMover cameraMover = new(context.Get<CoroutineRunner>());
            var stateController = context.Get<GameStateController>();

            CameraController cameraController = new(config.CameraConfig, camera, cameraMover, stateController);
        }
    }
}