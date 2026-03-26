using UnityEngine;

namespace VFX
{
    [CreateAssetMenu(fileName = "Particle Effect", menuName = "Scriptable Objects/VFX/Particle Effect")]
    public class ParticleEffect : ScriptableObject
    {
        [SerializeField] private ParticleChannel _channel;
        [SerializeField] private ParticleType _particleType;
        [SerializeField] private float _scale = 1f;

        public ParticleType ParticleType => _particleType;
        public float Scale => _scale;

        public void Invoke(Vector3 position) => _channel.Invoke(this, position);
    }
}
