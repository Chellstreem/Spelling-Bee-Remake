using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Victory State", menuName = "Game States/Victory State")]
    public class VictoryState : GameStateDefinition
    {
        public override GameStateType StateType => GameStateType.Victory;

        public override void Enter(GameState state) => Debug.Log("Entering Victory State");

        public override void Exit(GameState state) { }
    }
}
