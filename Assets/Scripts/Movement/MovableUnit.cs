using System;
using System.Collections;
using GameStates;
using Spawn;
using UnityEngine;
using Zenject;

namespace Movement
{
    public abstract class MovableUnit : MonoBehaviour
    {
        protected GameStateController _stateController;
        protected GameSpeedController _speedController;
        protected UnitPool _pool;
        protected GameConfig _config;

        public bool IsMoving { get; protected set; }
        public event Action OnMovementChanged;

        [Inject]
        public virtual void Construct(GameStateController stateController, GameSpeedController speedController, UnitPool pool,
            GameConfig gameConfig)
        {
            _stateController = stateController;
            _speedController = speedController;
            _pool = pool;
            _config = gameConfig;
        }

        private void Update()
        {
            if (!IsMoving)
                return;

            Move();
        }

        protected virtual void Move()
        {
            Vector3 newPosition = transform.position;
            newPosition += _config.MoveDirection * (_speedController.CurrentSpeed * Time.deltaTime);

            if (newPosition.z <= _config.ReturnThreshold)
            {
                StopMoving();
                _pool.ReturnObject(gameObject);
                return;
            }

            transform.position = newPosition;
        }

        protected virtual void StartMoving()
        {
            if (IsMoving)
                return;

            IsMoving = true;
            InvokeMovementChanged();
        }

        protected virtual void StopMoving()
        {
            if (!IsMoving)
                return;

            IsMoving = false;
            InvokeMovementChanged();
        }

        protected virtual void OnStateChanged()
        {
            if (!_stateController.CurrentState.Definition.IsMovingState)
                StopMoving();
            else
                StartMoving();
        }

        protected void InvokeMovementChanged() => OnMovementChanged?.Invoke();
    }
}
