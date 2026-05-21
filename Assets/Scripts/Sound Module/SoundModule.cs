using Installers;
using UnityEngine;
using GameModules;

namespace SoundModule
{
    [CreateAssetMenu(fileName = "Sound Module", menuName = "Game/Sound/Sound Module")]
    public class SoundModule : GameModule
    {
        [SerializeField] private SoundConfig _soundConfig;

        public override void Install(SceneInstaller installer, GameConfig config)
        {
            AudioSourcePool audioSourcePool = new(_soundConfig, installer.Camera);

            installer.DiContainer.Bind<AudioSourcePool>()
                .FromInstance(audioSourcePool)
                .AsSingle()
                .NonLazy();

            installer.DiContainer.Bind<SoundController>()
                .FromInstance(new SoundController(audioSourcePool, _soundConfig))
                .AsSingle()
                .NonLazy();
        }

        public override void Dispose(SceneInstaller installer)
        {
            var controller = installer.DiContainer.Resolve<SoundController>();
            controller.Dispose();
        }
    }
}