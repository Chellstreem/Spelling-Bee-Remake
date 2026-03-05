using UnityEngine;

namespace GameStates
{
    public class MovingState : IGameState, IEventSubscriber<OnVictory>, IEventSubscriber<OnDeath>
    {
        private readonly EventManager eventManager;
        private readonly IStateSwitcher stateSwitcher;       
        private readonly IInputEnabler inputEnabler;

        public MovingState(EventManager eventManager, IStateSwitcher stateSwitcher, IInputEnabler inputEnabler)
        {
            this.stateSwitcher = stateSwitcher;            
            this.eventManager = eventManager;
            this.inputEnabler = inputEnabler;
        }

        public void Enter()
        {
            Debug.Log("Entering Moving State");
            eventManager.Publish(new OnMovingStateEnter());
            inputEnabler.Enable();
            SubscribeToEvents();            
        }

        public void Exit()
        {
            Debug.Log("Exiting Moving State");
            eventManager.Publish(new OnMovingStateExit());
            inputEnabler.Disable();
            UnsubscribeFromEvents();            
        }               

        public void OnEvent(OnVictory eventData) => stateSwitcher.SetState(GameState.Victory);        

        public void OnEvent(OnDeath eventData) => stateSwitcher.SetState(GameState.Loss);

        private void SubscribeToEvents()
        {
            eventManager.Subscribe<OnDeath>(this);
            eventManager.Subscribe<OnVictory>(this);            
        }

        private void UnsubscribeFromEvents()
        {
            eventManager.Unsubscribe<OnDeath>(this);
            eventManager.Unsubscribe<OnVictory>(this);            
        }
    }
}