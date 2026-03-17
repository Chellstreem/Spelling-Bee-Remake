using InputControl;
using Zenject;
using UnityEngine;

namespace Movement
{
    public class MovablePlayer : MovableUnit
    {
        private IInput _input;
        private ObjectMover _objectMover;
        private GameConfig _gameConfig;

        [Inject]
        public void Construct(IInput input, GameConfig config, CoroutineRunner runner)
        {
            _input = input;
            _objectMover = new ObjectMover(runner);
            _gameConfig = config;
        }

        private void OnEnable()
        {
            if (_stateController.CurrentState != null)
                OnStateChanged();

            _input.OnMoveUp += OnMoveUp;
            _input.OnMoveDown += OnMoveDown;
            _stateController.OnStateChanged += OnStateChanged;
        }

        protected override void StartMoving()
        {
            IsMoving = true;
            InvokeMovementChanged();
        }

        protected override void StopMoving()
        {
            IsMoving = false;
            InvokeMovementChanged();
        }

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

        private void OnDisable()
        {
            _input.OnMoveUp -= OnMoveUp;
            _input.OnMoveDown -= OnMoveDown;
            _stateController.OnStateChanged -= OnStateChanged;
        }
    }
}