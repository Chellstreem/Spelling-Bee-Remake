using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VFXModule
{
    public class ParticlePlayer
    {
        private readonly ParticlePool particlePool;

        public ParticlePlayer(ParticlePool particlePool) => this.particlePool = particlePool;

        public ParticleSystem Play(ParticleType type, Vector3 position, float scale = 1f)
        {
            ParticleSystem particle = particlePool.GetParticle(type);

            particle.transform.position = position;
            particle.transform.localScale = Vector3.one * scale;

            particle.gameObject.SetActive(true);
            particle.Play();

            ReturnToPoolAfterPlay(type, particle).Forget();

            return particle;
        }

        private async UniTaskVoid ReturnToPoolAfterPlay(ParticleType particleType, ParticleSystem particle)
        {
            await UniTask.WaitUntil(() => !particle.IsAlive(true));

            particle.Stop();
            particle.gameObject.SetActive(false);

            particlePool.ReturnParticle(particleType, particle);
        }
    }
}