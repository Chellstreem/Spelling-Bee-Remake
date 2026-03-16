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
        protected float _thresholdZ;
        protected Vector3 _moveDirection;
        private Coroutine _moveCoroutine;

        public bool IsMoving { get; private set; }
        public event Action OnMovementChanged;

        [Inject]
        public virtual void Construct(GameStateController stateController, GameSpeedController speedController,
         ObjectPool pool, GameConfig gameConfig)
        {
            _stateController = stateController;
            _speedController = speedController;
            _pool = pool;
            _thresholdZ = gameConfig.ReturnThreshold;
            _moveDirection = gameConfig.MoveDirection;
        }

        public virtual void StartMoving()
        {
            if (_moveCoroutine == null)
            {
                _moveCoroutine = StartCoroutine(MoveCoroutine());
                IsMoving = true;
                OnMovementChanged?.Invoke();
            }
        }

        public virtual void StopMoving()
        {
            if (_moveCoroutine == null)
                return;

            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
            IsMoving = false;

            OnMovementChanged?.Invoke();
        }

        protected virtual void OnStateChanged()
        {
            if (!_stateController.CurrentState.AllowMoving)
                StopMoving();
            else
                StartMoving();
        }

        protected virtual IEnumerator MoveCoroutine()
        {
            while (true)
            {
                Vector3 newPosition = transform.position;
                newPosition += _moveDirection * (_speedController.CurrentSpeed * Time.deltaTime);

                if (newPosition.z <= _thresholdZ)
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
