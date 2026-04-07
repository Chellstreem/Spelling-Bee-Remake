using Installers;
using Sound;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Audio Module", menuName = "Scriptable Objects/Services/Audio Module")]
    public class AudioModule : GameModule
    {
        public override void Install(GameServices services, SceneInstaller installer, GameConfig config)
        {
            AudioSourcePool audioSourcePool = new(config.SoundConfig, installer.Camera);
            services.Register(audioSourcePool);

            installer.DiContainer.Bind<AudioSourcePool>()
                .FromInstance(audioSourcePool)
                .AsSingle()
                .NonLazy();

            SoundController soundController = new(audioSourcePool, config.SoundConfig);
        }
    }
}