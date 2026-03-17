using System;
using GameStates;
using InputControl;
using Movement;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Units
{
    public class Player : InteractableUnit, IDamageable
    {
        private GameConfig _gameConfig;
        private Health _health;
        public override UnitType Type => UnitType.Player;

        [Inject]
        public void Construct(GameConfig config) => _gameConfig = config;

        protected override void Awake()
        {
            base.Awake();
            _health = new(_gameConfig.PlayerMaxLives, _gameConfig.PlayerStartLives);
        }

        private void OnEnable()
        {
            transform.position = _gameConfig.PlayerLowerPosition;
            _health?.Refresh();
        }

        void IDamageable.Damage(int count)
        {
            _health.Damage(count);

            if (_health.CurrentHealth <= 0)
                InvokeDeath();
        }

        protected override void InvokeDeath()
        {
            base.InvokeDeath();
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;

            if (_collider is BoxCollider boxCollider)
                boxCollider.center = _gameConfig.PlayerDeathColliderCenter;
        }

        protected override void HandleCollision(InteractableUnit other) => InvokeAttack();
    }
}