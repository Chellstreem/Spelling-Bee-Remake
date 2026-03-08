using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Victory State", menuName = "Game States/Victory State")]
    public class VictoryState : GameState
    {
        public override GameStateType StateType => GameStateType.Victory;

        public override void Initialize(GameStateController stateController, CoroutineRunner runner) { }

        public override void Enter() => Debug.Log("Entering Victory State");

        public override void Exit() { }
    }
}
