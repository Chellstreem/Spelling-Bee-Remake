using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

namespace Movement
{
    public class Background : MovableUnit
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

        private void OnDisable() => _stateController.OnStateChanged -= OnStateChanged;
    }
}
