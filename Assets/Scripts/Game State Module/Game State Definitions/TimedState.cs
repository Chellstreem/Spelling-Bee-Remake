using Cysharp.Threading.Tasks;
using SoundModule;
using UnityEngine;
using System.Threading;
using System;

namespace GameStateModule
{
    [CreateAssetMenu(fileName = "Timed State", menuName = "Game/Game States/Timed State")]
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
            state.StateCTS = new CancellationTokenSource();

            RunStateAsync(state, state.StateCTS.Token).Forget();

            if (_stateSound != null)
                _stateSound.PlayOneShot();
        }

        public override void Exit(GameState state)
        {
            var spawnState = state as SpawnState;

            StopSpawning(spawnState);

            state.StateCTS?.Cancel();
            state.StateCTS?.Dispose();
            state.StateCTS = null;
        }

        private async UniTaskVoid RunStateAsync(GameState state, CancellationToken token)
        {
            var spawnState = state as SpawnState;
            StartSpawning(spawnState);

            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_duration), cancellationToken: token);
                StopSpawning(spawnState);
                state.StateController.SetState(_nextState);
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}