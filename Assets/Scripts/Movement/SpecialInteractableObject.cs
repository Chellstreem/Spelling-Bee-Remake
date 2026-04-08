using UnityEngine;

namespace Movement
{
    public class SpecialInteractableObject : InteractableUnit
    {
        [Tooltip("How the speed changes compared to other objects.")]
        [SerializeField] private float _speedDelta = 0;

        protected override void OnStateChanged()
        {
            if (_stateController.CurrentState.Definition.KillUnits)
            {
                _pool.ReturnObject(gameObject);
                return;
            }
        }

        protected override void Move()
        {
            Vector3 newPosition = transform.position;
            float speed = _speedController.CurrentSpeed + _speedDelta;

            newPosition += _config.MoveDirection * (speed * Time.deltaTime);

            if (newPosition.z <= _config.ReturnThreshold)
            {
                StopMoving();
                _pool.ReturnObject(gameObject);
                return;
            }

            transform.position = newPosition;
        }
    }
}
