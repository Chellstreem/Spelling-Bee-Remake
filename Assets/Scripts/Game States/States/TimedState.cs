using System.Collections;
using Sound;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Timed State", menuName = "Game States/Timed State")]
    public class TimedState : SpawnStateDefinition
    {
        [Tooltip("Duration of the timed state in seconds")]
        [SerializeField] private float _duration = 5f;
        [Tooltip("State to transition to when this timed state completes")]
        [SerializeField] private GameStateType _nextState = GameStateType.Interactive;
        [Tooltip("Optional sound to play while this state is active")]
        [SerializeField] private SoundUnit _stateSound;

        public override void Enter(GameState state)
        {
            state.StateCoroutine = state.Runner.Run(StateCoroutine(state));

            if (_stateSound != null)
                _stateSound.PlayOneShot();
        }

        public override void Exit(GameState state)
        {
            var spawnState = state as SpawnState;

            if (state.StateCoroutine != null)
            {
                StopSpawning(spawnState);
                state.Runner.Stop(state.StateCoroutine);
                state.StateCoroutine = null;
            }
        }

        private IEnumerator StateCoroutine(GameState state)
        {
            var spawnState = state as SpawnState;

            StartSpawning(spawnState);
            yield return new WaitForSeconds(_duration);
            StopSpawning(spawnState);
            state.StateController.SetState(_nextState);
        }
    }
}
