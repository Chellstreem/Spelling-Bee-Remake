using Sound;
using UnityEngine;
using Zenject;

namespace Units
{
    public class Player : InteractableUnit, IDamageable, IHealth
    {
        [SerializeField] private SoundUnit _attackSound;
        [SerializeField] private SoundUnit _deathSound;
        [SerializeField] private SoundUnit _damageSound;

        private GameConfig _gameConfig;
        public Health Health { get; private set; }
        public bool IsAlive { get; private set; } = true;

        public override InteractableType Type => InteractableType.Player;

        [Inject]
        public void Construct(GameConfig config) => _gameConfig = config;

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

        void IDamageable.Damage(int count)
        {
            Health.Damage(count);
            _damageSound.PlayOneShot();

            if (Health.CurrentHealth <= 0)
            {
                IsAlive = false;
                InvokeDeath();
                _deathSound.PlayOneShot();
            }
        }

        protected override void InvokeDeath()
        {
            base.InvokeDeath();

            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;

            if (_collider is BoxCollider boxCollider)
                boxCollider.center = _gameConfig.PlayerDeathColliderCenter;
        }

        protected override void HandleCollision(InteractableUnit other)
        {
            InvokeAttack();
        }
    }
}