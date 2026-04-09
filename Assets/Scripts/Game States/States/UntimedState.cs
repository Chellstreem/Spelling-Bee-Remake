using Sound;
using UnityEngine;
using VFX;
using Zenject;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Untimed State", menuName = "Game States/Untimed State")]
    public class UntimedState : GameStateDefinition
    {
        [SerializeField] private SoundUnit _stateSound;
        [SerializeField] private bool _loopSound = true;
        [SerializeField] private ParticleEffectInfo _stateEffect;

        public override void Enter(GameState state)
        {
            state.StartSpawning();

            state.PlaySound(_stateSound, _loopSound);
            PlayVisualEffect(_stateEffect, state);
        }

        public override void Exit(GameState state)
        {
            state.StopSpawning();
            state.StopSound();
        }
    }
}
