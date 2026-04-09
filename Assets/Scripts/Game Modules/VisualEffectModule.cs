using Installers;
using VFX;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Visual Effect Module", menuName = "Scriptable Objects/Services/Visual Effect Module")]
    public class VisualEffectModule : GameModule
    {
        public override void Install(SceneInstaller installer, GameConfig config)
        {
            ParticlePool pool = new(config.ParticleConfig);

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