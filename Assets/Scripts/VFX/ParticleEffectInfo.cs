using UnityEngine;

namespace VFX
{
    [System.Serializable]
    public struct ParticleEffectInfo
    {
        [SerializeField] private ParticleType _particleType;
        [SerializeField] private float _scale;
        [SerializeField] private Vector3 _position;
        [SerializeField] private bool _isOffset;

        public readonly ParticleType Type => _particleType;
        public readonly float Scale => _scale;
        public readonly Vector3 Position => _position;
        public readonly bool IsOffset => _isOffset;
    }
}
