using Unity.VisualScripting;
using UnityEngine;

namespace Movement
{
    public class NonInteractableObject : MovableUnit
    {
        private void OnEnable()
        {
            if (_stateController.CurrentState == null)
                return;

            OnStateChanged();
            _stateController.OnStateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            if (_stateController.CurrentState == null)
                return;

            StopMoving();
            _stateController.OnStateChanged -= OnStateChanged;
        }
    }
}
