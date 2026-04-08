using Sound;
using Spawn;
using UnityEngine;
using VFX;
using Zenject;

namespace GameStates
{
    public class SpawnState : GameState
    {
        private readonly SpawnStateDefinition spawnDefinition;
        public UnitSpawner UnitSpawner { get; private set; }
        public GameSpeedController SpeedController { get; private set; }
        private Coroutine _spawnCoroutine;

        public SpawnState(GameStateDefinition definition) : base(definition)
        {
            spawnDefinition = definition as SpawnStateDefinition;
        }

        [Inject]
        public void Construct(UnitSpawner spawner, GameSpeedController speedController)
        {
            UnitSpawner = spawner;
            SpeedController = speedController;
        }

        public void StartSpawning() => _spawnCoroutine = Runner.Run(spawnDefinition.SpawnCoroutine(UnitSpawner, SpeedController));

        public void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                Runner.Stop(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }
    }
}