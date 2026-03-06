using System.Collections;
using UnityEngine;
using Zenject;

namespace Movement
{
    public abstract class MovableObject : MonoBehaviour
    {
        protected EventManager _eventManager;
        protected ISpawnableObjectReturner _objectReturner;
        protected float _thresholdZ;

        private Coroutine moveCoroutine;

        [Inject]
        public virtual void Construct(EventManager eventManager, ISpawnableObjectReturner objectReturner, GameConfig gameConfig)
        {
            _eventManager = eventManager;
            _objectReturner = objectReturner;
            _thresholdZ = gameConfig.ThresholdZ;
        }

        protected void StartMoving() => moveCoroutine ??= StartCoroutine(MoveCoroutine());

        protected void StopMoving()
        {
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);

            moveCoroutine = null;
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
                    ReturnToOriginalState();
                }

                transform.position = newPosition;
                yield return null;
            }
        }

        protected virtual void ReturnToOriginalState() => _objectReturner.ReturnObject(gameObject);
    }
}
