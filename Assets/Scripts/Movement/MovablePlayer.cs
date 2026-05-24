using InputModule;
using Zenject;
using UnityEngine;
using Units;
using Cysharp.Threading.Tasks;

namespace Movement
{
    [RequireComponent(typeof(Player))]
    public class MovablePlayer : MovableUnit
    {
        private IInput _input;
        private ObjectMover _objectMover;
        private Player _player;

        [Inject]
        public void Construct(IInput input)
        {
            _input = input;
            _objectMover = new();
        }

        private void OnEnable()
        {
            if (_stateController.CurrentState != null)
                OnStateChanged();

            _input.OnKeyUp += OnMoveUp;
            _input.OnKeyDown += OnMoveDown;
            _stateController.OnStateChanged += OnStateChanged;

            _player = gameObject.GetComponent<Player>();
        }

        protected override void OnStateChanged()
        {
            base.OnStateChanged();

            if (_stateController.CurrentState.Definition.StateType == GameStateModule.GameStateType.Victory)
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
            _objectMover.MoveTo(transform, position, _config.PlayerSpeed, _config.PlayerPositionTolerance).Forget();
        }

        private void OnMoveDown()
        {
            if (!_player.StatusController.CurrentStatus.Definition.CanMove)
                return;

            Vector3 position = _config.PlayerLowerPosition;
            _objectMover.MoveTo(transform, position, _config.PlayerSpeed, _config.PlayerPositionTolerance).Forget();
        }

        private void OnDisable()
        {
            _input.OnKeyUp -= OnMoveUp;
            _input.OnKeyDown -= OnMoveDown;
            _stateController.OnStateChanged -= OnStateChanged;
        }
    }
}