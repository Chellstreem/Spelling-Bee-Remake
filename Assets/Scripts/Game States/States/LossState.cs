using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Loss State", menuName = "Game States/Loss State")]
    public class LossState : GameState
    {
        public override GameStateType StateType => GameStateType.Loss;

        public override void Initialize(GameStateController stateController, CoroutineRunner runner) { }

        public override void Enter()
        {
            Debug.Log("Entering Loss State");
        }

        public override void Exit() { }
    }
}
