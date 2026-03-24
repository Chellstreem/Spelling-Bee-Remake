using Sound;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Victory State", menuName = "Game States/Victory State")]
    public class VictoryState : GameStateDefinition
    {
        [SerializeField] private SoundUnit _stateSound;
        public override GameStateType StateType => GameStateType.Victory;

        public override void Enter(GameState state) => _stateSound.PlayOneShot();

        public override void Exit(GameState state) { }
    }
}
