using GameModules;
using Installers;
using UnityEngine;
using UIModule;

namespace UIModule
{
    [CreateAssetMenu(fileName = "UI Module", menuName = "Game/UI/UI Module")]
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