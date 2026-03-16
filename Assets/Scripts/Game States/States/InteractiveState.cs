using UnityEngine;
using Zenject;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Interactive State", menuName = "Game States/Interactive State")]
    public class InteractiveState : SpawnStateDefinition
    {
        public override GameStateType StateType => GameStateType.Interactive;

        public override void Enter(GameState state)
        {
            Debug.Log("Entering Interactive State...");

            SpawnState spawnState = state as SpawnState;
            spawnState.StartSpawning();
        }

        public override void Exit(GameState state)
        {
            Debug.Log("Exiting Interactive State...");

            SpawnState spawnState = state as SpawnState;
            spawnState.StopSpawning();
        }
    }
}
