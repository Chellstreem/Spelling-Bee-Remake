using Installers;
using UnityEngine;
using WordControl;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Word Control Module", menuName = "Scriptable Objects/Services/Word Control Module")]
    public class WordControlModule : GameModule
    {
        public override void Install(GameServices services, SceneInstaller installer, GameConfig config)
        {
            WordController wordController = new(config.WordControlConfig);

            installer.DiContainer.Bind<WordController>()
                 .FromInstance(wordController)
                 .AsSingle()
                 .NonLazy();
        }
    }
}