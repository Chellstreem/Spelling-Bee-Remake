using System;
using UnityEngine;

namespace Movement
{
    public class InteractableObject : MovableUnit
    {
        private void OnEnable()
        {
            if (_stateController.CurrentState != null && _stateController.CurrentState.AllowMoving)
                StartMoving();

            _stateController.OnStateChanged += OnStateChanged;
        }

        protected override void OnStateChanged()
        {
            base.OnStateChanged();

            if (_stateController.CurrentState.KillInteractableObject)
            {
                _pool.ReturnObject(gameObject);
                return;
            }
        }

        private void OnDisable()
        {
            StopMoving();
            _stateController.OnStateChanged -= OnStateChanged;
        }
    }
}
