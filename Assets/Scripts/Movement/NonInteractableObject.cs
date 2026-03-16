using UnityEngine;

namespace Movement
{
    public class NonInteractableObject : MovableUnit
    {
        private void OnEnable()
        {
            StartMoving();

            if (_stateController != null)
                _stateController.OnStateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            StopMoving();

            if (_stateController != null)
                _stateController.OnStateChanged -= OnStateChanged;
        }
    }
}
