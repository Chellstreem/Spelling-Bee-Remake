using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Loss State", menuName = "Game States/Loss State")]
    public class LossState : GameStateDefinition
    {
        public override GameStateType StateType => GameStateType.Loss;

        public override void Enter(GameState state)
        {
            Debug.Log("Entering Loss State");
        }

        public override void Exit(GameState state) { }
    }
}
