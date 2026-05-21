using Installers;
using UnityEngine;
using GameModules;

namespace VFXModule
{
    [CreateAssetMenu(fileName = "VFX Module", menuName = "Game/VFX/VFX Module")]
    public class VisualEffectModule : GameModule
    {
        [SerializeField] private ParticleConfig _particleConfig;

        public override void Install(SceneInstaller installer, GameConfig config)
        {
            ParticlePool pool = new(_particleConfig);

            var container = installer.DiContainer;

            container.Bind<ParticlePlayer>()
                .AsSingle()
                .WithArguments(pool)
                .NonLazy();

            container.Bind<ObjectScaler>()
                .AsSingle()
                .NonLazy();
        }
    }
}