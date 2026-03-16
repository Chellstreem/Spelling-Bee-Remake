using UnityEngine;

namespace Units
{
    public class LandMonkey : InteractableUnit
    {
        [SerializeField] private int _damage = 1;

        public override SpawnableType Type => SpawnableType.LandMonkey;

        public void Kill() => InvokeDeath();

        protected override void HandleCollision(InteractableUnit other)
        {
            if (other.Type == SpawnableType.Letter)
                _pool.ReturnObject(other.gameObject);

            if (other.TryGetComponent<IDamageable>(out var damageable))
                damageable.Damage(_damage);
        }
    }
}
