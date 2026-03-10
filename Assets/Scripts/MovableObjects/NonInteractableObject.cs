using UnityEngine;

namespace MovableObjects
{
    public class NonInteractableObject : MovableObject
    {
        private void OnEnable()
        {
            StartMoving();

            if (_stateController != null)
                _stateController.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            if (!_stateController.CurrentState.AllowMoving)
            {
                StopMoving();
                return;
            }
        }

        private void OnDisable()
        {
            StopMoving();

            if (_stateController != null)
                _stateController.OnStateChanged -= OnStateChanged;
        }
    }
}
