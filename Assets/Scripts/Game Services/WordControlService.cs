using Installers;
using UnityEngine;
using WordControl;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Word Control Service", menuName = "Scriptable Objects/Services/Word Control Service")]
    public class WordControlService : GameModule
    {
        public override void Install(GameContext gameContext, MainStageInstaller installer, GameConfig config)
        {
            WordController wordController = new(config.WordControlConfig);
            gameContext.Register(wordController);

            installer.DiContainer.Bind<WordController>()
                 .FromInstance(wordController)
                 .AsSingle()
                 .NonLazy();
        }
    }
}