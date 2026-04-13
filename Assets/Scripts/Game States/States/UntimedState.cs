using Sound;
using UnityEngine;
using VFX;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Untimed State", menuName = "Game States/Untimed State")]
    public class UntimedState : SpawnStateDefinition
    {
        [Tooltip("Sound unit to play while this untimed state is active")]
        [SerializeField] private SoundUnit _stateSound;
        [Tooltip("Whether the state sound should loop while active")]
        [SerializeField] private bool _loopSound = true;
        [Tooltip("Optional particle effect to play for this state")]
        [SerializeField] private ParticleEffectInfo _stateEffect;

        public override void Enter(GameState state)
        {
            var spawnState = state as SpawnState;
            StartSpawning(spawnState);

            PlayStateSound(_stateSound, _loopSound, state);
            PlayVisualEffect(_stateEffect, state);
        }

        public override void Exit(GameState state)
        {
            var spawnState = state as SpawnState;
            StopSpawning(spawnState);
            StopSound(state);
        }
    }
}
