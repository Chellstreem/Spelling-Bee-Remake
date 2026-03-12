using Spawn;
using UnityEngine;
using Zenject;

namespace GameStates
{
    public abstract class GameStateDefinition : ScriptableObject
    {
        [SerializeField] private bool _allowMoving = false;

        [Tooltip("States that can be transitioned to from this state")]
        [SerializeField] private GameStateType[] _allowedTransitions;

        public abstract GameStateType StateType { get; }
        public bool AllowMoving => _allowMoving;

        public abstract void Enter(GameState state);
        public abstract void Exit(GameState state);

        public bool AllowTransitionTo(GameStateType newStateType)
        {
            if (_allowedTransitions.Length == 0)
                return false;

            foreach (var stateType in _allowedTransitions)
                if (stateType == newStateType)
                    return true;

            return false;
        }

        public virtual GameState CreateGameState(GameStateController stateController, CoroutineRunner runner, Spawner spawner,
         GameSpeedController speedController) => new(this, stateController, runner);
    }
}