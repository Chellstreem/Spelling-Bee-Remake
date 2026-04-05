using System.Collections;
using UnityEngine;

namespace VFX
{
    public class ParticlePlayer : IVisualEffectService
    {
        private readonly ParticlePool particlePool;
        private readonly CoroutineRunner runner;

        public ParticlePlayer(ParticlePool particlePool, CoroutineRunner coroutineRunner)
        {
            this.particlePool = particlePool;
            runner = coroutineRunner;
        }

        public ParticleSystem Play(ParticleType type, Vector3 position, float scale = 1f)
        {
            ParticleSystem particle = particlePool.GetParticle(type);
            particle.transform.position = position;
            particle.transform.localScale = Vector3.one * scale;
            particle.gameObject.SetActive(true);
            particle.Play();

            runner.Run(ReturnToPoolAfterPlay(type, particle));
            return particle;
        }

        private IEnumerator ReturnToPoolAfterPlay(ParticleType particleType, ParticleSystem particle)
        {
            yield return new WaitWhile(() => particle.IsAlive(true));

            particle.Stop();
            particle.gameObject.SetActive(false);
            particlePool.ReturnParticle(particleType, particle);
        }
    }
}
