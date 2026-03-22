using UnityEngine;
using Movement;
using System.Collections;

namespace Units
{
    public class Bird : MovableUnit
    {
        [Tooltip("How the speed changes compared to other objects.")]
        [SerializeField] private float _speedDelta = 0;

        private void OnEnable() => StartMoving();
        private void OnDisable() => StopMoving();

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