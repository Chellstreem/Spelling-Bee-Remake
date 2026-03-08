using System.Collections;
using GameStates;
using UnityEngine;
using Zenject;

namespace MovableObjects
{
    public abstract class MovableObject : MonoBehaviour
    {
        protected GameStateController _stateController;
        protected GameSpeedController _speedController;
        protected ISpawnableObjectReturner _objectReturner;
        protected float _thresholdZ;
        protected Vector3 _moveDirection;
        private Coroutine _moveCoroutine;

        [Inject]
        public virtual void Construct(GameStateController stateController, GameSpeedController speedController,
         ISpawnableObjectReturner objectReturner, GameConfig gameConfig)
        {
            _stateController = stateController;
            _speedController = speedController;
            _objectReturner = objectReturner;
            _thresholdZ = gameConfig.ThresholdZ;
            _moveDirection = gameConfig.MoveDirection;
        }

        public void StartMoving() => _moveCoroutine ??= StartCoroutine(MoveCoroutine());

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
                    _objectReturner.ReturnObject(gameObject);
                    yield break;
                }

                transform.position = newPosition;
                yield return null;
            }
        }
    }
}
