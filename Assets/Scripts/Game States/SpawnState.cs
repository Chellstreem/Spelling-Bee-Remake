using Sound;
using Spawn;
using UnityEngine;
using Zenject;

namespace GameStates
{
    public class SpawnState : GameState
    {
        private readonly SpawnStateDefinition spawnDefinition;
        public Spawner Spawner { get; }
        public GameSpeedController SpeedController { get; }

        private Coroutine _spawnCoroutine;

        public SpawnState(GameStateDefinition definition, GameStateController stateController, CoroutineRunner runner,
         Spawner spawner, GameSpeedController speedController, AudioSource audioSource)
        : base(definition, stateController, runner, audioSource)
        {
            spawnDefinition = definition as SpawnStateDefinition;
            Spawner = spawner;
            SpeedController = speedController;
        }

        public void StartSpawning() => _spawnCoroutine = Runner.Run(spawnDefinition.SpawnCoroutine(Spawner, SpeedController));

        public void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                Runner.StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }
    }
}