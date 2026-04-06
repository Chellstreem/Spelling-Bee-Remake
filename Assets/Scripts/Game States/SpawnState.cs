using Sound;
using Spawn;
using UnityEngine;
using Zenject;

namespace GameStates
{
    public class SpawnState : GameState
    {
        private readonly SpawnStateDefinition spawnDefinition;
        public UnitSpawner Spawner { get; }
        public GameSpeedController SpeedController { get; }
        public CoroutineRunner Runner { get; }
        private Coroutine _spawnCoroutine;

        public SpawnState(GameStateDefinition definition, GameServices context)
        : base(definition, context)
        {
            spawnDefinition = definition as SpawnStateDefinition;
            Spawner = context.Get<UnitSpawner>();
            SpeedController = context.Get<GameSpeedController>();
            Runner = context.Get<CoroutineRunner>();
        }

        public void StartSpawning() => _spawnCoroutine = Runner.Run(spawnDefinition.SpawnCoroutine(Spawner, SpeedController));

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