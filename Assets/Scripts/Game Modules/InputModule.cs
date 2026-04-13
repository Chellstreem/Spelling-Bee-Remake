using InputControl;
using Installers;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Input Module", menuName = "Scriptable Objects/Services/Input Module")]
    public class InputModule : GameModule
    {
        public override void Install(SceneInstaller installer, GameConfig config)
        {
            IInput input = new DesktopInput();
            input.Disable();

            installer.DiContainer.Bind<IInput>()
                .FromInstance(input)
                .AsSingle()
                .NonLazy();

            installer.DiContainer
                .Bind<InputController>()
                .AsSingle()
                .NonLazy();

            installer.DiContainer
                .Bind<CursorController>()
                .AsSingle()
                .NonLazy();
        }

        public override void Dispose(SceneInstaller installer)
        {
            var inputController = installer.DiContainer.Resolve<InputController>();
            inputController.Dispose();

            var cursorController = installer.DiContainer.Resolve<CursorController>();
            cursorController.Dispose();
        }
    }
}