using Sound;
using UnityEngine;
using VFX;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Victory State", menuName = "Game States/Victory State")]
    public class VictoryState : GameStateDefinition
    {
        [SerializeField] private SoundUnit _stateSound;

        [Header("VFX")]
        [SerializeField] private ParticleEffectInfo _victoryEffect;

        public override GameStateType StateType => GameStateType.Victory;

        public override void Enter(GameState state)
        {
            _stateSound.PlayOneShot();
            //_victoryEffect.Invoke(_effectPosition);
        }

        public override void Exit(GameState state) { }
    }
}
