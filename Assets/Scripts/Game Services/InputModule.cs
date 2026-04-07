using GameStates;
using InputControl;
using Installers;
using UnityEngine;
using UserInterface;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Input Module", menuName = "Scriptable Objects/Services/Input Module")]
    public class InputModule : GameModule
    {
        public override void Install(GameServices services, SceneInstaller installer, GameConfig config)
        {
            CursorController controller = new(services.Get<GameStateController>());

            IInput input = new DesktopInput();
            services.Register(input);
            input.Disable();

            installer.DiContainer.Bind<IInput>()
                .FromInstance(input)
                .AsSingle()
                .NonLazy();
        }
    }
}