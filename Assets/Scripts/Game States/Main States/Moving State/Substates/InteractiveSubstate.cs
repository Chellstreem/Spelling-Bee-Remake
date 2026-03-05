using UnityEngine;

namespace GameStates.Moving
{
    public class InteractiveSubstate : IGameState, IEventSubscriber<OnWordCompleted>
    {
        private readonly EventManager eventManager;
        private readonly ISubstateSwitcher<MovingStateSubstate> substateSwitcher;               

        public InteractiveSubstate(EventManager eventManager, ISubstateSwitcher<MovingStateSubstate> substateSwitcher)
        {
            this.eventManager = eventManager;
            this.substateSwitcher = substateSwitcher;                      
        }

        public void Enter()
        {
            Debug.Log("Entering Interactive State...");            
            eventManager.Publish(new OnInteractiveSubstateEnter());
            SubscribeToEvents();
        }

        public void Exit()
        {
            Debug.Log("Exiting Interactive State...");            
            eventManager.Publish(new OnInteractiveSubstateExit());
            UnsubscribeFromEvents();
        }

        public void OnEvent(OnWordCompleted eventData)
        {
            if (eventData.GameplayAction == GameplayActionType.Missiles)
            {
                substateSwitcher.SetSubstate(MovingStateSubstate.Missile);
            }
            else
            {
                substateSwitcher.SetSubstate(MovingStateSubstate.Safe);
            }            
        }        

        private void SubscribeToEvents()
        {
            eventManager.Subscribe<OnWordCompleted>(this);            
        }

        private void UnsubscribeFromEvents()
        {
            eventManager.Unsubscribe<OnWordCompleted>(this);            
        }
    }
}
