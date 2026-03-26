using UnityEngine;

namespace VFX
{
    [System.Serializable]
    public struct PoolParticleInfo
    {
        [SerializeField] private ParticleType _particleType;
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private int _poolAmount;

        public readonly ParticleType Type => _particleType;
        public readonly ParticleSystem Prefab => _particlePrefab;
        public readonly int PoolAmount => _poolAmount;
    }

    [CreateAssetMenu(fileName = "Particle Config", menuName = "Scriptable Objects/VFX/Particle Config")]
    public class ParticleConfig : ScriptableObject
    {
        [SerializeField] private ParticleChannel _channel;
        [SerializeField] private PoolParticleInfo[] _particles;

        public ParticleChannel Channel => _channel;
        public PoolParticleInfo[] Particles => _particles;
    }
}