using UnityEngine;

namespace GameStates
{
    public class VictoryState : GameState
    {
        private readonly IStateSwitcher stateSwitcher;
        private readonly EventManager eventManager;

        public VictoryState(IStateSwitcher stateSwitcher, EventManager eventManager)
        {
            this.stateSwitcher = stateSwitcher;
            this.eventManager = eventManager;
        }

        public override void Enter()
        {
            eventManager.Publish(new OnVictoryStateEnter());
            Debug.Log("Entering Victory State");
        }

        public override void Exit() { }
    }
}
