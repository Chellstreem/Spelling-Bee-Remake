using System.Collections;
using GameStates;
using UnityEngine;
using Zenject;

namespace MovableObjects
{
    public abstract class MovableObject : MonoBehaviour
    {
        protected GameStateController _stateController;
        protected ISpawnableObjectReturner _objectReturner;
        protected float _thresholdZ;
        private Coroutine _moveCoroutine;

        [Inject]
        public virtual void Construct(GameStateController stateController, ISpawnableObjectReturner objectReturner, GameConfig gameConfig)
        {
            _stateController = stateController;
            _objectReturner = objectReturner;
            _thresholdZ = gameConfig.ThresholdZ;
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
                newPosition += Vector3.back * (GameSpeed.Speed * Time.deltaTime);

                if (newPosition.z <= _thresholdZ)
                {
                    StopMoving();
                    _objectReturner.ReturnObject(gameObject);
                }

                transform.position = newPosition;
                yield return null;
            }
        }
    }
}
