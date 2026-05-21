using CameraModule;
using Installers;
using UnityEngine;
using GameModules;

namespace CameraModule
{
    [CreateAssetMenu(fileName = "Camera Module", menuName = "Game/Camera/Camera Module")]
    public class CameraModule : GameModule
    {
        [SerializeField] private CameraConfig _cameraConfig;

        public override void Install(SceneInstaller installer, GameConfig config)
        {
            Camera camera = installer.Camera;

            installer.DiContainer.Bind<Camera>()
               .FromInstance(camera)
               .AsSingle()
               .NonLazy();

            var cameraMover = installer.DiContainer.Bind<CameraMover>()
                .AsSingle()
                .NonLazy();

            installer.DiContainer.Bind<CameraController>()
                .AsSingle()
                .WithArguments(_cameraConfig)
                .NonLazy();
        }
    }
}