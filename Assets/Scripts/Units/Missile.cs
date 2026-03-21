using Sound;
using UnityEngine;

namespace Units
{
    public class Missile : InteractableUnit
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private SoundUnit collisionSound;

        public override UnitType Type => UnitType.Missile;

        protected override void HandleCollision(InteractableUnit other)
        {
            _channel.RaiseEvent(collisionSound);

            if (other.TryGetComponent<IDamageable>(out var damageable))
                damageable.Damage(_damage);
        }
    }
}