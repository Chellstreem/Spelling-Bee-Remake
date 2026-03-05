using UnityEngine;

namespace GameStates
{
    public class LossState : IGameState
    {
        private readonly EventManager eventManager;

        public LossState(EventManager eventManager)
        {
            this.eventManager = eventManager;
        }       

        public void Enter()
        {
            eventManager.Publish(new OnLossStateEnter());
            Debug.Log("Entering Loss State");
        }

        public void Exit() { }
    }
}
