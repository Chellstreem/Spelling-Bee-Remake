using Spawn;
using UnityEngine;
using Zenject;

namespace GameStates
{
    public class SpawnState : GameState
    {
        public Spawner Spawner { get; private set; }
        public GameSpeedController SpeedController { get; private set; }
        public Coroutine SpawnCoroutine { get; set; }

        public SpawnState(GameStateDefinition definition, GameStateController stateController, CoroutineRunner runner, Spawner spawner, GameSpeedController speedController)
        : base(definition, stateController, runner)
        {
            Spawner = spawner;
            SpeedController = speedController;
        }
    }
}