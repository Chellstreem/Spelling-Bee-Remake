using System;
using GameStates;
using InputControl;
using Movement;
using UnityEngine;
using Zenject;

namespace Units
{
    public class Player : InteractableUnit, IDamageable
    {
        private Health _health;
        private IInput _input;
        private ObjectMover _objectMover;
        private GameConfig _gameConfig;

        public override UnitType Type => UnitType.Player;

        [Inject]
        public void Construct(IInput input, GameConfig config, CoroutineRunner runner)
        {
            _input = input;
            _objectMover = new ObjectMover(runner);
            _gameConfig = config;
        }

        private void Awake() => _health = new(_gameConfig.PlayerMaxLives, _gameConfig.PlayerStartLives);

        private void OnEnable()
        {
            transform.position = _gameConfig.PlayerLowerPosition;
            _health?.Refresh();
            SubscribeToEvents();
        }

        void IDamageable.Damage(int count)
        {
            _health.Damage(count);

            if (_health.CurrentHealth <= 0)
                InvokeDeath();
        }

        protected override void HandleCollision(InteractableUnit other) => InvokeCollision();

        private void OnMoveUp()
        {
            Vector3 position = _gameConfig.PlayerUpperPosition;
            _objectMover.MoveTo(transform, position, _gameConfig.PlayerSpeed, _gameConfig.PlayerPositionTolerance);
        }

        private void OnMoveDown()
        {
            Vector3 position = _gameConfig.PlayerLowerPosition;
            _objectMover.MoveTo(transform, position, _gameConfig.PlayerSpeed, _gameConfig.PlayerPositionTolerance);
        }

        private void SubscribeToEvents()
        {
            _input.OnMoveUp += OnMoveUp;
            _input.OnMoveDown += OnMoveDown;
        }

        private void UnsubscribeFromEvents()
        {
            _input.OnMoveUp -= OnMoveUp;
            _input.OnMoveDown -= OnMoveDown;
        }

        private void OnDisable() => UnsubscribeFromEvents();
    }
}