using UnityEngine;

namespace VFX
{
    [System.Serializable]
    public struct PoolParticleInfo
    {
        [SerializeField] private ParticleType _particleType;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private int _poolAmount;

        public readonly ParticleType ParticleType => _particleType;
        public readonly ParticleSystem Particle => _particle;
        public readonly int PoolAmount => _poolAmount;
    }

    public class VisualEffectConfig
    {
        [SerializeField] private PoolParticleInfo[] _particles;

        public PoolParticleInfo[] Particles => _particles;
    }
}