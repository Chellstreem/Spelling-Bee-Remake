using Sound;
using UnityEngine;

namespace Units
{
    public class HostileAnimal : InteractableUnit, IDamageable
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private SoundUnit _attackSound;
        [SerializeField] private SoundUnit _deathSound;

        public override InteractableType InteractableType => InteractableType.HostileAnimal;
        public bool IsDead { get; private set; } = true;

        public void OnEnable() => IsDead = false;

        void IDamageable.Damage(int damage)
        {
            IsDead = true;
            InvokeDeath();
            _deathSound.PlayOneShot();
        }

        public override void InvokeCharacterSound()
        {
            if (IsDead)
                return;

            base.InvokeCharacterSound();
        }

        protected override void HandleCollision(InteractableUnit other)
        {
            if (IsDead)
                return;

            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                if (damageable.IsDead)
                    return;

                InvokeAttack();
                damageable.Damage(_damage);

                _attackSound.PlayOneShot();
            }
        }
    }
}
