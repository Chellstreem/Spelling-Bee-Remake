using UnityEngine;

namespace GameStates
{
    public class VictoryState : IGameState
    {
        private readonly IStateSwitcher stateSwitcher;
        private readonly EventManager eventManager;

        public VictoryState(IStateSwitcher stateSwitcher, EventManager eventManager)
        {
            this.stateSwitcher = stateSwitcher;
            this.eventManager = eventManager;
        }

        public void Enter()
        {
            eventManager.Publish(new OnVictoryStateEnter());
            Debug.Log("Entering Victory State");
        }

        public void Exit() { }
    }
}
