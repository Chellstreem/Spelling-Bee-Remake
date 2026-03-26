using Sound;
using UnityEngine;
using VFX;
using Zenject;

namespace Units
{
    public class Player : InteractableUnit, IDamageable, IHealth
    {
        [SerializeField] private SoundUnit _attackSound;
        [SerializeField] private SoundUnit _deathSound;
        [SerializeField] private SoundUnit _damageSound;

        [Header("VFX")]
        [SerializeField] private ParticleEffectInfo _deathEffect;
        [SerializeField] private Vector3 _deathEffectOffset = new(0, 2f, 0);

        private GameConfig _gameConfig;
        public Health Health { get; private set; }
        public bool IsDead { get; private set; } = false;

        public override InteractableType InteractableType => InteractableType.Player;

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
                InvokeDeath();
        }

        protected override void InvokeDeath()
        {
            base.InvokeDeath();

            IsDead = true;
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;

            if (_collider is BoxCollider boxCollider)
                boxCollider.center = _gameConfig.PlayerDeathColliderCenter;

            _deathSound.PlayOneShot();
        }

        protected override void HandleCollision(InteractableUnit other)
        {
            if (IsDead)
                return;

            InvokeAttack();
        }
    }
}