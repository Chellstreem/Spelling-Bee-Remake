using Sound;
using UnityEngine;
using Zenject;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Interactive State", menuName = "Game States/Interactive State")]
    public class InteractiveState : SpawnStateDefinition
    {
        [SerializeField] private SoundUnit _stateSound;

        public override GameStateType StateType => GameStateType.Interactive;

        public override void Enter(GameState state)
        {
            Debug.Log("Entering Interactive State...");

            SpawnState spawnState = state as SpawnState;
            spawnState.StartSpawning();

            _stateSound.Play(state.AudioSource, true);
        }

        public override void Exit(GameState state)
        {
            Debug.Log("Exiting Interactive State...");

            SpawnState spawnState = state as SpawnState;
            spawnState.StopSpawning();

            _stateSound.Stop(state.AudioSource);
        }
    }
}
