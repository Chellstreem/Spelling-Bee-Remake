using System.Collections;
using UnityEngine;

namespace Movement
{
    public class SpecialInteractableObject : InteractableObject
    {
        [Tooltip("How the speed changes compared to other objects.")]
        [SerializeField] private float _speedDelta = 0;

        protected override void OnStateChanged()
        {
            base.OnStateChanged();

            if (_stateController.CurrentState.KillInteractableObject)
            {
                _pool.ReturnObject(gameObject);
                return;
            }
        }

        protected override IEnumerator MoveCoroutine()
        {
            while (true)
            {
                Vector3 newPosition = transform.position;
                float speed = _speedController.CurrentSpeed + _speedDelta;

                newPosition += _moveDirection * (speed * Time.deltaTime);

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