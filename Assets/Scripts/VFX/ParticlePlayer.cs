using System.Collections;
using UnityEngine;

namespace VFX
{
    public class ParticlePlayer
    {
        private readonly ParticlePool particlePool;
        private readonly ParticleChannel channel;
        private readonly CoroutineRunner coroutineRunner;

        public ParticlePlayer(ParticlePool particlePool, ParticleChannel channel, CoroutineRunner coroutineRunner)
        {
            this.particlePool = particlePool;
            this.channel = channel;
            this.coroutineRunner = coroutineRunner;

            channel.OnParticleInvoked += OnParticleEffect;
        }

        public ParticleSystem Play(ParticleType particleType, Vector3 position, float scale = 1f)
        {
            ParticleSystem particle = particlePool.GetParticle(particleType);
            particle.transform.position = position;
            particle.transform.localScale = Vector3.one * scale;
            particle.gameObject.SetActive(true);
            particle.Play();

            coroutineRunner.StartCoroutine(ReturnToPoolAfterPlay(particleType, particle));
            return particle;
        }

        private void OnParticleEffect(ParticleEffect effect, Vector3 position)
        {
            Play(effect.ParticleType, position, effect.Scale);
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
