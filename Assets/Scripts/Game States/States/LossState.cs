using Sound;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Loss State", menuName = "Game States/Loss State")]
    public class LossState : GameStateDefinition
    {
        [SerializeField] private SoundUnit _stateSound;
        public override GameStateType StateType => GameStateType.Loss;

        public override void Enter(GameState state)
        {
            _stateSound.PlayOneShot();
        }

        public override void Exit(GameState state) { }
    }
}
