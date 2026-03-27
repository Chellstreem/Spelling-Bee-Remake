using Sound;
using UnityEngine;
using VFX;

namespace Units
{
    public class Missile : InteractableUnit
    {
        [SerializeField] private SoundUnit _collisionSound;

        [Header("VFX")]
        [SerializeField] private ParticleEffectInfo _explosionEffect;

        public override InteractableType InteractableType => InteractableType.Missile;

        public override void HandleCollision(InteractableUnit other)
        {
            _collisionSound.PlayOneShot();

            if (other.TryGetComponent<IDamageable>(out var damageable))
                damageable.Damage(_damage);

            UseParticleEffect(_explosionEffect);
            _objectPool.ReturnObject(gameObject);
        }
    }
}