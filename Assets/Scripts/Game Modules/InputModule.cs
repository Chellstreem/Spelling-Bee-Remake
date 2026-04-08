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
            input.Disable();

            services.Register(input);

            InputController inputController = new(input, services.Get<GameStateController>());

            installer.DiContainer.Bind<IInput>()
                .FromInstance(input)
                .AsSingle()
                .NonLazy();
        }
    }
}