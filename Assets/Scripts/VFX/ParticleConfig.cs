using UnityEngine;

namespace VFX
{
    [System.Serializable]
    public struct PoolParticleInfo
    {
        [Tooltip("Type identifier for this pooled particle effect")]
        [SerializeField] private ParticleType _particleType;
        [Tooltip("Prefab used to spawn particle instances for this type")]
        [SerializeField] private ParticleSystem _particlePrefab;
        [Tooltip("Initial amount of pooled instances to pre-allocate")]
        [SerializeField] private int _poolAmount;

        public readonly ParticleType Type => _particleType;
        public readonly ParticleSystem Prefab => _particlePrefab;
        public readonly int PoolAmount => _poolAmount;
    }

    [CreateAssetMenu(fileName = "Particle Config", menuName = "Scriptable Objects/VFX/Particle Config")]
    public class ParticleConfig : ScriptableObject
    {
        [Tooltip("Pool configuration entries for particle effects")]
        [SerializeField] private PoolParticleInfo[] _particles;
        public PoolParticleInfo[] Particles => _particles;
    }
}