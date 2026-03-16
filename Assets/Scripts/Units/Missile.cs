using UnityEngine;

namespace Units
{
    public class Missile : InteractableUnit
    {
        [SerializeField] private int _damage = 1;

        public override SpawnableType Type => SpawnableType.Missile;

        protected override void HandleCollision(InteractableUnit other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
                damageable.Damage(_damage);
        }
    }
}