using Sound;
using UnityEngine;

namespace Units
{
    public class Missile : InteractableUnit
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private SoundUnit _collisionSound;
        public override InteractableType InteractableType => InteractableType.Missile;

        protected override void HandleCollision(InteractableUnit other)
        {
            _collisionSound.PlayOneShot();

            if (other.TryGetComponent<IDamageable>(out var damageable))
                damageable.Damage(_damage);

            _objectPool.ReturnObject(gameObject);
        }
    }
}