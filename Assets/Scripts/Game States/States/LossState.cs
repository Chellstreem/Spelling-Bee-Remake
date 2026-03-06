using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Loss State", menuName = "Game States/Loss State")]
    public class LossState : GameState
    {
        private EventManager _eventManager;

        public override void Initialize(GameStateController stateController, EventManager eventManager, CoroutineRunner runner)
        {
            _eventManager = eventManager;
        }

        public override GameStateType StateType => GameStateType.Loss;

        public override void Enter()
        {
            Debug.Log("Entering Loss State");
        }

        public override void Exit() { }
    }
}
