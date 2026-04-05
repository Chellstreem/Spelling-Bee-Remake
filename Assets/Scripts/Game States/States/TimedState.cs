using System.Collections;
using Sound;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Timed State", menuName = "Game States/Timed State")]
    public class TimedState : SpawnStateDefinition
    {
        [SerializeField] private float _duration = 5f;
        [SerializeField] private GameStateType _nextState = GameStateType.Interactive;
        [SerializeField] private SoundUnit _stateSound;

        public override void Enter(GameState state)
        {
            base.Enter(state);

            SpawnState spawnState = state as SpawnState;
            state.StateCoroutine = state.Context.Get<CoroutineRunner>().Run(StateCoroutine(spawnState));

            if (_stateSound != null)
                _stateSound.PlayOneShot();
        }

        public override void Exit(GameState state)
        {
            SpawnState spawnState = state as SpawnState;

            if (state.StateCoroutine != null)
            {
                spawnState.StopSpawning();
                state.Context.Get<CoroutineRunner>().Stop(state.StateCoroutine);
                state.StateCoroutine = null;
            }
        }

        private IEnumerator StateCoroutine(SpawnState spawnState)
        {
            spawnState.StartSpawning();
            yield return new WaitForSeconds(_duration);
            spawnState.StopSpawning();
            spawnState.Context.Get<GameStateController>().SetState(_nextState);
        }
    }
}
