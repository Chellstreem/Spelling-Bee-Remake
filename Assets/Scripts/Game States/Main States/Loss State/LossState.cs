using UnityEngine;

namespace GameStates
{
    public class LossState : GameState
    {
        private readonly EventManager eventManager;

        public LossState(EventManager eventManager)
        {
            this.eventManager = eventManager;
        }

        public override void Enter()
        {
            eventManager.Publish(new OnLossStateEnter());
            Debug.Log("Entering Loss State");
        }

        public override void Exit() { }
    }
}
