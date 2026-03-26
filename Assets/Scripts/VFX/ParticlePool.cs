using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class ParticlePool
    {
        private readonly ParticleConfig config;
        private readonly Dictionary<ParticleType, Queue<ParticleSystem>> poolDictionary = new();
        private readonly Transform poolHolder;

        public ParticlePool(ParticleConfig config)
        {
            this.config = config;
            poolHolder = new GameObject("Particle Pool").transform;
            InitializePools();
        }

        public ParticleSystem GetParticle(ParticleType particleType)
        {
            if (!poolDictionary.ContainsKey(particleType) || poolDictionary[particleType].Count == 0)
            {
                Debug.LogWarning($"Партикл типа {particleType} не найден.");
                return null;
            }

            return poolDictionary[particleType].Dequeue();
        }

        public void ReturnParticle(ParticleType particleType, ParticleSystem particle)
        {
            particle.transform.SetParent(poolHolder);
            poolDictionary[particleType].Enqueue(particle);
        }

        private void InitializePools()
        {
            foreach (var particleInfo in config.Particles)
            {
                var particleType = particleInfo.Type;

                if (!poolDictionary.ContainsKey(particleType))
                    poolDictionary[particleType] = new Queue<ParticleSystem>();

                for (int i = 0; i < particleInfo.PoolAmount; i++)
                {
                    var particle = Object.Instantiate(particleInfo.Prefab, poolHolder.transform);
                    particle.gameObject.SetActive(false);

                    poolDictionary[particleType].Enqueue(particle);
                }
            }
        }
    }
}
