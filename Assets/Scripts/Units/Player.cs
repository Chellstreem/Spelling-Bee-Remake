using Sound;
using UnityEngine;
using VFX;
using Zenject;

namespace Units
{
    public class Player : ComplexUnit, IHealth
    {
        [Space(15f)]
        [SerializeField] private SoundUnit _damageSound;
        [SerializeField] private ParticleEffectInfo _deathEffect;
        [Inject] private GameConfig _gameConfig;

        public Health Health { get; private set; }

        public override ComplexUnitType ComplexUnitType => ComplexUnitType.Player;

        protected override void Awake()
        {
            base.Awake();
            Health = new(_gameConfig.PlayerMaxLives, _gameConfig.PlayerStartLives);
        }

        private void OnEnable()
        {
            transform.position = _gameConfig.PlayerLowerPosition;
            Health?.Refresh();
        }

        public override void HandleCollision(Unit other)
        {
            if (!IsInteractable)
                return;

            if (!StatusController.CurrentStatus.Definition.CanDealDamage)
                return;

            InvokeAttack();
            other.Damage(_damage);
        }

        public override void Damage(int count)
        {
            if (!StatusController.CurrentStatus.Definition.CanTakeDamage)
                return;

            Health.Damage(count);
            _damageSound.PlayOneShot();

            if (Health.CurrentHealth <= 0)
                InvokeDeath();
        }

        protected override void InvokeDeath()
        {
            base.InvokeDeath();

            _collider.isTrigger = false;

            if (_collider is BoxCollider boxCollider)
                boxCollider.center = _gameConfig.PlayerDeathColliderCenter;

            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }
    }
}