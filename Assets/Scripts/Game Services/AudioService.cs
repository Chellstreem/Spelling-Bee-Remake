using Installers;
using Sound;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Audio Service", menuName = "Scriptable Objects/Services/Audio Service")]
    public class AudioService : GameModule
    {
        public override void Install(GameContext gameContext, MainStageInstaller installer, GameConfig config)
        {
            AudioSourcePool audioSourcePool = new(config.SoundConfig, installer.Camera);
            gameContext.Register(audioSourcePool);

            installer.DiContainer.Bind<AudioSourcePool>()
                .FromInstance(audioSourcePool)
                .AsSingle()
                .NonLazy();

            SoundController soundController = new(audioSourcePool, config.SoundConfig);
        }
    }
}