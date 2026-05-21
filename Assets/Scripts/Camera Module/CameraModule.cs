using CameraControl;
using Installers;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Camera Module", menuName = "Scriptable Objects/Services/Camera Module")]
    public class CameraModule : GameModule
    {
        [Tooltip("Default camera state used when the scene starts or when camera resets")]
        [SerializeField] private CameraState _defaultCameraState;

        public CameraState DefaultState => _defaultCameraState;

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
                .WithArguments(this)
                .NonLazy();
        }
    }
}