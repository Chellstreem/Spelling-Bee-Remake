using Installers;
using VFX;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Visual Effect Module", menuName = "Scriptable Objects/Services/Visual Effect Module")]
    public class VisualEffectModule : GameModule
    {
        public override void Install(GameServices gameServices, SceneInstaller installer, GameConfig config)
        {
            VisualEffectServices services = new();

            ParticlePool pool = new(config.ParticleConfig);
            ParticlePlayer particlePlayer = new(pool, gameServices.Get<CoroutineRunner>());
            services.RegisterService(particlePlayer);

            ObjectScaler scaler = new();
            services.RegisterService(scaler);

            var container = installer.DiContainer;

            container.Bind<ParticlePlayer>()
                .FromInstance(particlePlayer)
                .AsSingle()
                .NonLazy();

            container.Bind<VisualEffectServices>()
                .FromInstance(services)
                .AsSingle()
                .NonLazy();
        }
    }
}