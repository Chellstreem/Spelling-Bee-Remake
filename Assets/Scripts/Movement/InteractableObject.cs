using Particles;
using Zenject;

namespace Movement
{
    public class InteractableObject : MovableObject, IEventSubscriber<OnWordCompleted>,
        IEventSubscriber<OnMovingStateExit>, IEventSubscriber<OnMissileStateEnter>
    {
        [Inject]
        private readonly IParticlePlayer particlePlayer;

        private void OnEnable()
        {
            SubscribeToEvents();
            StartMoving();
        }

        private void OnDisable()
        {
            StopMoving();
            UnsubscribeFromEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void OnEvent(OnWordCompleted eventData)
        {
            particlePlayer.PlayParticle(ParticleType.Poof, transform.position);
            ReturnToOriginalState();
        }

        public void OnEvent(OnMissileStateEnter eventData)
        {
            particlePlayer.PlayParticle(ParticleType.Poof, transform.position);
            ReturnToOriginalState();
        }

        public void OnEvent(OnMovingStateExit eventData) => StopMoving();

        private void SubscribeToEvents()
        {
            _eventManager.Subscribe<OnWordCompleted>(this);
            _eventManager.Subscribe<OnMovingStateExit>(this);
            _eventManager.Subscribe<OnMissileStateEnter>(this);
        }

        private void UnsubscribeFromEvents()
        {
            _eventManager.Unsubscribe<OnWordCompleted>(this);
            _eventManager.Unsubscribe<OnMovingStateExit>(this);
            _eventManager.Unsubscribe<OnMissileStateEnter>(this);
        }
    }
}
