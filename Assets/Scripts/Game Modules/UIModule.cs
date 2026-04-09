using GameStates;
using Installers;
using UnityEngine;
using UserInterface;

namespace GameModules
{
    [CreateAssetMenu(fileName = "UI Module", menuName = "Scriptable Objects/Services/UI Module")]
    public class UIModule : GameModule
    {
        public override void Install(SceneInstaller installer, GameConfig config)
        {
            installer.DiContainer.Bind<UIBarController>()
                .AsSingle()
                .NonLazy();
        }
    }
}