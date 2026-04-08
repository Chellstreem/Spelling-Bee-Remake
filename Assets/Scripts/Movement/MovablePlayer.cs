using InputControl;
using Zenject;
using UnityEngine;
using Units;

namespace Movement
{
    [RequireComponent(typeof(Player))]
    public class MovablePlayer : MovableUnit
    {
        private IInput _input;
        private ObjectMover _objectMover;
        private Player _player;

        [Inject]
        public void Construct(IInput input, CoroutineRunner runner)
        {
            _input = input;
            _objectMover = new ObjectMover(runner);
        }

        private void OnEnable()
        {
            if (_stateController.CurrentState != null)
                OnStateChanged();

            _input.OnMoveUp += OnMoveUp;
            _input.OnMoveDown += OnMoveDown;
            _stateController.OnStateChanged += OnStateChanged;

            _player = gameObject.GetComponent<Player>();
        }

        protected override void OnStateChanged()
        {
            base.OnStateChanged();

            if (_stateController.CurrentState.Definition.StateType == GameStates.GameStateType.Victory)
                OnMoveDown();
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

        protected override void Move() { }

        private void OnMoveUp()
        {
            if (!_player.StatusController.CurrentStatus.Definition.CanMove)
                return;

            Vector3 position = _config.PlayerUpperPosition;
            _objectMover.MoveTo(transform, position, _config.PlayerSpeed, _config.PlayerPositionTolerance);
        }

        private void OnMoveDown()
        {
            if (!_player.StatusController.CurrentStatus.Definition.CanMove)
                return;

            Vector3 position = _config.PlayerLowerPosition;
            _objectMover.MoveTo(transform, position, _config.PlayerSpeed, _config.PlayerPositionTolerance);
        }

        private void OnDisable()
        {
            _input.OnMoveUp -= OnMoveUp;
            _input.OnMoveDown -= OnMoveDown;
            _stateController.OnStateChanged -= OnStateChanged;
        }
    }
}