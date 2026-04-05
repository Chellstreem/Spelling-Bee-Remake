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
        public CoroutineRunner Runner { get; }
        private Coroutine _spawnCoroutine;

        public SpawnState(GameStateDefinition definition, GameContext context)
        : base(definition, context)
        {
            spawnDefinition = definition as SpawnStateDefinition;
            Spawner = context.Get<Spawner>();
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