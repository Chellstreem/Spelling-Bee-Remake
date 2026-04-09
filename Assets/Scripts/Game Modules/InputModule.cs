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

            installer.DiContainer.Instantiate<InputController>();
            installer.DiContainer.Instantiate<CursorController>();
        }
    }
}