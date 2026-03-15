using System;
using UnityEngine;

namespace Movement
{
    public class InteractableObject : MovableObject
    {
        private void OnEnable()
        {
            if (_stateController.CurrentState != null && _stateController.CurrentState.AllowMoving)
                StartMoving();

            SubscribeToEvents();
        }

        private void OnStateChanged()
        {
            if (!_stateController.CurrentState.AllowMoving)
            {
                StopMoving();
                return;
            }

            if (_stateController.CurrentState.StateType == GameStates.GameStateType.Missile)
            {
                _pool.ReturnObject(gameObject);
            }
        }

        private void SubscribeToEvents()
        {
            _stateController.OnStateChanged += OnStateChanged;
        }

        private void UnsubscribeFromEvents()
        {
            _stateController.OnStateChanged -= OnStateChanged;
        }

        private void OnDisable()
        {
            StopMoving();
            UnsubscribeFromEvents();
        }
    }
}
