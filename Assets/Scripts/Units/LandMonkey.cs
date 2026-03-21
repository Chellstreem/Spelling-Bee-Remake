using UnityEngine;

namespace Units
{
    public class LandMonkey : InteractableUnit
    {
        [SerializeField] private int _damage = 1;

        public override UnitType Type => UnitType.LandMonkey;

        protected override void HandleCollision(InteractableUnit other)
        {
            InvokeAttack();

            if (other.Type == UnitType.Letter)
                _objectPool.ReturnObject(other.gameObject);

            if (other.TryGetComponent<IDamageable>(out var damageable))
                damageable.Damage(_damage);
        }
    }
}
