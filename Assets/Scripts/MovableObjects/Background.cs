using UnityEngine;
using System.Collections;

namespace MovableObjects
{
    public class Background : MovableObject
    {
        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
            _stateController.OnStateChanged += OnStateChanged;
        }

        protected override IEnumerator MoveCoroutine()
        {
            while (true)
            {
                Vector3 newPosition = transform.position;
                newPosition += _moveDirection * (_speedController.CurrentSpeed * Time.deltaTime);

                if (newPosition.z < _thresholdZ)
                    newPosition = _startPosition;

                transform.position = newPosition;
                yield return null;
            }
        }

        private void OnStateChanged()
        {
            if (!_stateController.CurrentState.AllowMoving)
            {
                StopMoving();
                return;
            }

            StartMoving();
        }

        private void OnDisable() => _stateController.OnStateChanged -= OnStateChanged;
    }
}
