using UnityEngine;

namespace VFX
{
    [System.Serializable]
    public struct ParticleEffectInfo
    {
        [Tooltip("Type of particle effect to play")]
        [SerializeField] private ParticleType _particleType;
        [Tooltip("Scale multiplier applied to the particle effect")]
        [SerializeField] private float _scale;
        [Tooltip("Position where the particle will be spawned (local or world depending on IsOffset)")]
        [SerializeField] private Vector3 _position;
        [Tooltip("If true the particle position is treated as an offset from the unit's transform")]
        [SerializeField] private bool _isOffset;

        public readonly ParticleType Type => _particleType;
        public readonly float Scale => _scale;
        public readonly Vector3 Position => _position;
        public readonly bool IsOffset => _isOffset;
    }
}
