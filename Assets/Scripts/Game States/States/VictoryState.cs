using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Victory State", menuName = "Game States/Victory State")]
    public class VictoryState : GameState
    {
        private EventManager _eventManager;

        public override GameStateType StateType => GameStateType.Victory;

        public override void Initialize(GameStateController stateController, EventManager eventManager, CoroutineRunner runner)
        {
            _eventManager = eventManager;
        }

        public override void Enter()
        {
            _eventManager.Publish(new OnVictoryStateEnter());
            Debug.Log("Entering Victory State");
        }

        public override void Exit() { }
    }
}
