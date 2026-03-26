using Sound;
using UnityEngine;
using VFX;

namespace Units
{
    public class Missile : InteractableUnit
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private SoundUnit _collisionSound;

        [Header("VFX")]
        [SerializeField] private ParticleEffectInfo _explosionEffect;

        public override InteractableType InteractableType => InteractableType.Missile;

        protected override void HandleCollision(InteractableUnit other)
        {
            _collisionSound.PlayOneShot();

            if (other.TryGetComponent<IDamageable>(out var damageable))
                damageable.Damage(_damage);

            PlayEffect(_explosionEffect);
            _objectPool.ReturnObject(gameObject);
        }
    }
}