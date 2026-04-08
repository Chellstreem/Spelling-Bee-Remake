using Sound;
using UnityEngine;
using VFX;
using Zenject;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Untimed State", menuName = "Game States/Untimed State")]
    public class IntimedState : SpawnStateDefinition
    {
        [SerializeField] private SoundUnit _stateSound;
        [SerializeField] private bool _loopSound = true;
        [SerializeField] private ParticleEffectInfo _stateEffect;

        public override void Enter(GameState state)
        {
            SpawnState spawnState = state as SpawnState;
            spawnState.StartSpawning();

            state.PlaySound(_stateSound, _loopSound);
            PlayVisualEffect(_stateEffect, state);
        }

        public override void Exit(GameState state)
        {
            SpawnState spawnState = state as SpawnState;
            spawnState.StopSpawning();

            state.StopSound();
        }
    }
}
