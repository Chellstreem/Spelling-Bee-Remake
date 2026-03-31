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
        protected ObjectPool _pool;
        protected GameConfig _config;
        private Coroutine _moveCoroutine;

        public bool IsMoving { get; protected set; }
        public event Action OnMovementChanged;

        [Inject]
        public virtual void Construct(GameStateController stateController, GameSpeedController speedController,
         ObjectPool pool, GameConfig gameConfig)
        {
            _stateController = stateController;
            _speedController = speedController;
            _pool = pool;
            _config = gameConfig;
        }

        protected virtual void StartMoving()
        {
            if (_moveCoroutine == null)
            {
                _moveCoroutine = StartCoroutine(MoveCoroutine());
                IsMoving = true;
                InvokeMovementChanged();
            }
        }

        protected virtual void StopMoving()
        {
            if (_moveCoroutine == null)
                return;

            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
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

        protected virtual IEnumerator MoveCoroutine()
        {
            while (true)
            {
                Vector3 newPosition = transform.position;
                newPosition += _config.MoveDirection * (_speedController.CurrentSpeed * Time.deltaTime);

                if (newPosition.z <= _config.ReturnThreshold)
                {
                    StopMoving();
                    _pool.ReturnObject(gameObject);
                    yield break;
                }

                transform.position = newPosition;
                yield return null;
            }
        }
    }
}
