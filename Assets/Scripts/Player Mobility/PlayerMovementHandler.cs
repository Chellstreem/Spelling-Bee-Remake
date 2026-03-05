using System;

namespace PlayerMobility
{
    public class PlayerMovementHandler : IDisposable, IEventSubscriber<OnVictory>
    {
        private readonly EventManager eventManager;
        private readonly IPlayerMover playerMover;
        private readonly IInput input;
        private readonly IDeathInvoker deathInvoker;               

        public PlayerMovementHandler(EventManager eventManager, IPlayerMover playerMover, IDeathInvoker deathInvoker,
            IInput input)
        {
            this.eventManager = eventManager;
            this.playerMover = playerMover;           
            this.input = input;
            this.deathInvoker = deathInvoker;

            SubscribeToEvents();            
        }

        public void OnEvent(OnVictory eventData) => playerMover.GoDown();        

        public void Dispose() => UnsubscribeFromEvents();        

        private void SubscribeToEvents()
        {
            input.ClickUp += playerMover.GoUp;
            input.ClickDown += playerMover.GoDown;
            input.ClickDeath += deathInvoker.InvokeDeath;
            eventManager.Subscribe<OnVictory>(this);           
        }

        private void UnsubscribeFromEvents()
        {
            input.ClickUp -= playerMover.GoUp;
            input.ClickDown -= playerMover.GoDown;
            input.ClickDeath -= deathInvoker.InvokeDeath;
            eventManager.Unsubscribe<OnVictory>(this);
        }
    }
}
