using Sound;
using UnityEngine;

namespace Units
{
    public class HostileAnimal : InteractableUnit, IDamageable
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private SoundUnit _attackSound;
        [SerializeField] private SoundUnit _deathSound;

        public override InteractableType Type => InteractableType.HostileAnimal;
        public bool IsAlive { get; private set; } = true;

        void IDamageable.Damage(int damage)
        {
            IsAlive = false;
            InvokeDeath();
        }

        protected override void HandleCollision(InteractableUnit other)
        {
            if (other.Type == InteractableType.Letter)
                _objectPool.ReturnObject(other.gameObject);

            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                if (!damageable.IsAlive)
                    return;

                InvokeAttack();
                damageable.Damage(_damage);
                _attackSound.PlayOneShot();
            }
        }
    }
}
