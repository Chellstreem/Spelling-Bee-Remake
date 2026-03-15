using Input;
using Movement;
using UnityEngine;
using Zenject;

namespace GameplayObjects
{
    public class Player : InteractableObject
    {
        private IInput _input;
        private ObjectMover _objectMover;
        private float _moveSpeed;
        private float _positionTolerance;
        private Vector3 _lowerPosition;
        private Vector3 _upperPosition;

        public override SpawnableType Type => SpawnableType.Player;

        [Inject]
        public void Construct(IInput input, GameConfig config, CoroutineRunner runner)
        {
            _input = input;
            _objectMover = new ObjectMover(runner);

            _moveSpeed = config.PlayerSpeed;
            _positionTolerance = config.PlayerPositionTolerance;
            _lowerPosition = config.PlayerLowerPosition;
            _upperPosition = config.PlayerUpperPosition;
        }

        private void OnEnable()
        {
            transform.position = _lowerPosition;
            SubscribeToEvents();
        }

        protected override void HandleCollision(InteractableObject other)
        {
            // flinch
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

        private void OnMoveUp() => _objectMover.MoveTo(transform, _upperPosition, _moveSpeed, _positionTolerance);
        private void OnMoveDown() => _objectMover.MoveTo(transform, _lowerPosition, _moveSpeed, _positionTolerance);
        private void OnDisable() => UnsubscribeFromEvents();
    }
}