using System;
using UnityEngine;

namespace MovableObjects
{
    public class InteractableObject : MovableObject, IEventSubscriber<OnWordCompleted>
    {
        private void OnEnable() => SubscribeToEvents();

        public void OnEvent(OnWordCompleted eventData)
        {
            _objectReturner.ReturnObject(gameObject);
            //particlePlayer.PlayParticle(ParticleType.Poof, transform.position);
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
                _objectReturner.ReturnObject(gameObject);
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
