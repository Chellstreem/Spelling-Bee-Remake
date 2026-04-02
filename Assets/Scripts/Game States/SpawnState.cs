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

        public SpawnState(GameStateDefinition definition, GameContext context)
        : base(definition, context)
        {
            spawnDefinition = definition as SpawnStateDefinition;
            Spawner = context.Spawner;
            SpeedController = context.SpeedController;
        }

        public void StartSpawning() => _spawnCoroutine = Context.Runner.Run(spawnDefinition.SpawnCoroutine(Spawner, SpeedController));

        public void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                Context.Runner.StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }
    }
}