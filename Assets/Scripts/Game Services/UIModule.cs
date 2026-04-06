using GameStates;
using Installers;
using UnityEngine;
using UserInterface;

namespace GameModules
{
    [CreateAssetMenu(fileName = "UI Module", menuName = "Scriptable Objects/Services/UI Module")]
    public class UIModule : GameModule
    {
        public override void Install(GameServices services, Installer installer, GameConfig config)
        {
            var stateController = services.Get<GameStateController>();
            UIBarController barController = new(stateController);

            installer.DiContainer.Bind<UIBarController>()
                .FromInstance(barController)
                .AsSingle()
                .NonLazy();
        }
    }
}