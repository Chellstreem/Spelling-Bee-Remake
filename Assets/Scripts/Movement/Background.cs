using UnityEngine;
using System.Collections;

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

        protected override void Move()
        {
            Vector3 newPosition = transform.position;
            newPosition += _config.MoveDirection * (_speedController.CurrentSpeed * Time.deltaTime);

            if (newPosition.z < _config.ReturnThreshold)
                newPosition = _startPosition;

            transform.position = newPosition;
        }

        private void OnDisable() => _stateController.OnStateChanged -= OnStateChanged;
    }
}
