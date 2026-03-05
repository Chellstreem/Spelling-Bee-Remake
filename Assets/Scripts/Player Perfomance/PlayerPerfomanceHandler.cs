using System;

namespace PlayerPerfomance
{
    public class PlayerPerfomanceHandler : IDisposable, IEventSubscriber<OnAllWordsCompleted>, IDeathInvoker
    {
        private readonly EventManager eventManager;
        private readonly IHealth playerHealth;

        public PlayerPerfomanceHandler(EventManager eventManager, IHealth playerHealth)
        {
            this.eventManager = eventManager;
            this.playerHealth = playerHealth;

            SubscribeToEvents();
        }

        private void OnLifeChanged(HealthChangeType changeType)
        {
            if (playerHealth.LivesRemaining <= 0)
                InvokeDeath();
        }

        public void OnEvent(OnAllWordsCompleted eventData)
        {
            eventManager.Publish(new OnVictory());
        }

        public void InvokeDeath() => eventManager.Publish(new OnDeath());

        public void Dispose() => UnsubscribeFromEvents();

        private void SubscribeToEvents()
        {
            playerHealth.OnHealthChanged += OnLifeChanged;
            eventManager.Subscribe<OnAllWordsCompleted>(this);
        }

        private void UnsubscribeFromEvents()
        {
            playerHealth.OnHealthChanged -= OnLifeChanged;
            eventManager.Unsubscribe<OnAllWordsCompleted>(this);
        }
    }
}
