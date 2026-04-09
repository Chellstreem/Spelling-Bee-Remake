using Installers;
using Sound;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Audio Module", menuName = "Scriptable Objects/Services/Audio Module")]
    public class AudioModule : GameModule
    {
        public override void Install(SceneInstaller installer, GameConfig config)
        {
            AudioSourcePool audioSourcePool = new(config.SoundConfig, installer.Camera);

            installer.DiContainer.Bind<AudioSourcePool>()
                .FromInstance(audioSourcePool)
                .AsSingle()
                .NonLazy();

            installer.DiContainer.Bind<SoundController>()
                .FromInstance(new SoundController(audioSourcePool, config.SoundConfig))
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