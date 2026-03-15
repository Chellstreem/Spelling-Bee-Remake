using System.Collections;
using GameStates;
using Spawn;
using UnityEngine;
using Zenject;

namespace Movement
{
    public abstract class MovableObject : MonoBehaviour
    {
        protected GameStateController _stateController;
        protected GameSpeedController _speedController;
        protected ObjectPool _pool;
        protected float _thresholdZ;
        protected Vector3 _moveDirection;
        private Coroutine _moveCoroutine;

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

        public void StartMoving()
        {
            _moveCoroutine ??= StartCoroutine(MoveCoroutine());
        }

        public void StopMoving()
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = null;
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
