using System;
using UnityEngine;

namespace MovableObjects
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
                //particlePlayer.PlayParticle(ParticleType.Poof, transform.position);
                _pool.ReturnObject(gameObject);
            }
        }

        private void SubscribeToEvents()
        {
            //_eventManager.Subscribe<OnWordCompleted>(this);
            _stateController.OnStateChanged += OnStateChanged;
        }

        private void UnsubscribeFromEvents()
        {
            //_eventManager.Unsubscribe<OnWordCompleted>(this);
            _stateController.OnStateChanged -= OnStateChanged;
        }

        private void OnDisable()
        {
            StopMoving();
            UnsubscribeFromEvents();
        }
    }
}
