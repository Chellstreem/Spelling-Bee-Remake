using HealthSystem;
using InputControl;
using Sound;
using UnityEngine;
using VFX;
using Zenject;

namespace Units
{
    public class Player : ComplexUnit, IHealth
    {
        [Space(15f)]
        [Tooltip("Sound played when the player takes damage")]
        [SerializeField] private SoundUnit _damageSound;
        [Tooltip("Particle effect played when the player dies")]
        [SerializeField] private ParticleEffectInfo _deathEffect;
        [Tooltip("Renderer used to toggle player visibility")]
        [SerializeField] private Renderer _renderer;
        private GameConfig _config;
        private IInput _input;

        public Health Health { get; private set; }
        public override ComplexUnitType ComplexUnitType => ComplexUnitType.Player;

        [Inject]
        public void Construct(GameConfig config, IInput input)
        {
            _config = config;
            _input = input;
        }

        protected override void Awake()
        {
            base.Awake();
            Health = new(_config.PlayerMaxLives, _config.PlayerStartLives);
        }

        private void OnEnable()
        {
            transform.position = _config.PlayerLowerPosition;
            Health?.Refresh();

            _input.OnGameOver += OnGameOver;
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

        public void SetVisible(bool isVisible) => _renderer.enabled = isVisible;

        protected override void InvokeDeath()
        {
            base.InvokeDeath();

            _collider.isTrigger = false;

            if (_collider is BoxCollider boxCollider)
                boxCollider.center = _config.PlayerDeathColliderCenter;

            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }

        private void OnGameOver()
        {
            if (!IsInteractable) return;
            InvokeDeath();
        }
    }
}