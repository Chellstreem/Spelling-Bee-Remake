using Spawn;
using UnityEngine;
using Zenject;

namespace GameStates
{
    public class SpawnState : GameState
    {
        private readonly SpawnStateDefinition spawnDefinition;
        public Spawner Spawner { get; private set; }
        public GameSpeedController SpeedController { get; private set; }

        private Coroutine _spawnCoroutine;

        public SpawnState(GameStateDefinition definition, GameStateController stateController, CoroutineRunner runner, Spawner spawner, GameSpeedController speedController)
        : base(definition, stateController, runner)
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