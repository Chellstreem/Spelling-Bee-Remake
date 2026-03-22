using System.Collections;
using Sound;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Missile State", menuName = "Game States/Missile State")]
    public class MissileState : SpawnStateDefinition
    {
        [SerializeField] private float _duration = 5f;
        [SerializeField] private GameStateType _nextState = GameStateType.Interactive;
        [SerializeField] private SoundUnit _stateSound;

        public override GameStateType StateType => GameStateType.Missile;

        public override void Enter(GameState state)
        {
            Debug.Log("Entering Missile State...");

            SpawnState spawnState = state as SpawnState;
            state.StateCoroutine = state.Runner.Run(MissileCoroutine(spawnState));

            _stateSound.PlayOneShot();
        }

        public override void Exit(GameState state)
        {
            Debug.Log("Exiting Missile State...");
            SpawnState spawnState = state as SpawnState;

            if (state.StateCoroutine != null)
            {
                spawnState.StopSpawning();
                state.Runner.Stop(state.StateCoroutine);
                state.StateCoroutine = null;
            }
        }

        private IEnumerator MissileCoroutine(SpawnState spawnState)
        {
            spawnState.StartSpawning();
            yield return new WaitForSeconds(_duration);
            spawnState.StopSpawning();
            spawnState.StateController.SetState(_nextState);
        }
    }
}
