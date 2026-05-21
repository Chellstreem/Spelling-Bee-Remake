using Installers;
using UnityEngine;
using WordControlModule;
using GameModules;

namespace WordControlModule
{
    [CreateAssetMenu(fileName = "Word Control Module", menuName = "Game/Word Control/Word Control Module")]
    public class WordControlModule : GameModule
    {
        [SerializeField] private WordControlConfig _wordControlConfig;

        public override void Install(SceneInstaller installer, GameConfig config)
        {
            WordController wordController = new(_wordControlConfig);

            installer.DiContainer.Bind<WordController>()
                 .FromInstance(wordController)
                 .AsSingle()
                 .NonLazy();
        }
    }
}