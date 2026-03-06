using UnityEngine;

namespace MovableObjects
{
    public abstract class InteractableObject : MovableObject, IEventSubscriber<OnWordCompleted>
    {
        public abstract InteractableObjectType Type { get; }

        private void OnEnable() => SubscribeToEvents();

        protected abstract void OnCollision(InteractableObjectType type);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<InteractableObject>(out var interactable))
                OnCollision(Type);
        }

        public void OnEvent(OnWordCompleted eventData)
        {
            //particlePlayer.PlayParticle(ParticleType.Poof, transform.position);
            _objectReturner.ReturnObject(gameObject);
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
            _eventManager.Subscribe<OnWordCompleted>(this);
            _stateController.OnStateChanged += OnStateChanged;
        }

        private void UnsubscribeFromEvents()
        {
            _eventManager.Unsubscribe<OnWordCompleted>(this);
            _stateController.OnStateChanged -= OnStateChanged;
        }

        private void OnDisable()
        {
            StopMoving();
            UnsubscribeFromEvents();
        }
    }
}
