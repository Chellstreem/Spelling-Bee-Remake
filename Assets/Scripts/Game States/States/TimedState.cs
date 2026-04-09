using System.Collections;
using Sound;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Timed State", menuName = "Game States/Timed State")]
    public class TimedState : GameStateDefinition
    {
        [SerializeField] private float _duration = 5f;
        [SerializeField] private GameStateType _nextState = GameStateType.Interactive;
        [SerializeField] private SoundUnit _stateSound;

        public override void Enter(GameState state)
        {
            state.StateCoroutine = state.Runner.Run(StateCoroutine(state));

            if (_stateSound != null)
                _stateSound.PlayOneShot();
        }

        public override void Exit(GameState state)
        {
            if (state.StateCoroutine != null)
            {
                state.StopSpawning();
                state.Runner.Stop(state.StateCoroutine);
                state.StateCoroutine = null;
            }
        }

        private IEnumerator StateCoroutine(GameState spawnState)
        {
            spawnState.StartSpawning();
            yield return new WaitForSeconds(_duration);
            spawnState.StopSpawning();
            spawnState.StateController.SetState(_nextState);
        }
    }
}
