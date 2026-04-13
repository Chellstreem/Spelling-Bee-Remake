using Sound;
using Spawn;
using UnityEngine;
using Zenject;

namespace GameStates
{
    public class SpawnState : GameState
    {
        public SpawnStateDefinition SpawnDefinition { get; }
        public UnitSpawner UnitSpawner { get; private set; }
        public GameSpeedController SpeedController { get; private set; }
        public Coroutine SpawnCoroutine { get; set; }

        public SpawnState(SpawnStateDefinition definition) : base(definition)
        {
            SpawnDefinition = definition;
        }

        [Inject]
        public void Construct(UnitSpawner spawner, GameSpeedController speedController)
        {
            UnitSpawner = spawner;
            SpeedController = speedController;
        }
    }
}
