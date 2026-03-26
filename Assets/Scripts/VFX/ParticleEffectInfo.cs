using UnityEngine;

namespace VFX
{
    [System.Serializable]
    public struct ParticleEffectInfo
    {
        [SerializeField] private ParticleType _particleType;
        [SerializeField] private float _scale;
        [SerializeField] private Vector3 _positionOffset;

        public readonly ParticleType Type => _particleType;
        public readonly float Scale => _scale;
        public readonly Vector3 Offset => _positionOffset;
    }
}
