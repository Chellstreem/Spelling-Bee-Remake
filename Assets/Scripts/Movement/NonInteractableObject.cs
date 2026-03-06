namespace Movement
{
    public class NonInteractableObject : MovableObject, IEventSubscriber<OnMovingStateExit>
    {
        public void OnEvent(OnMovingStateExit eventData) => StopMoving();

        private void OnEnable()
        {
            StartMoving();
            _eventManager.Subscribe<OnMovingStateExit>(this);
        }

        private void OnDisable()
        {
            StopMoving();
            _eventManager.Unsubscribe<OnMovingStateExit>(this);
        }

        private void OnDestroy()
        {
            _eventManager.Unsubscribe<OnMovingStateExit>(this);
        }
    }
}
