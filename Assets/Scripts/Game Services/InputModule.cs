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
        public override void Install(GameContext context, MainStageInstaller installer, GameConfig config)
        {
            var stateController = context.Get<GameStateController>();
            CursorController controller = new(stateController);

            IInput input = new DesktopInput();
            context.Register(input);

            installer.DiContainer.Bind<IInput>()
                .FromInstance(input)
                .AsSingle()
                .NonLazy();

            input.Disable();
        }
    }
}