using UnityEngine;
using Zenject;

namespace GameStates
{
    public abstract class GameState : ScriptableObject
    {
        [SerializeField] private bool _allowMoving = false;

        [Tooltip("States that can be transitioned to from this state")]
        [SerializeField] private GameStateType[] _allowedTransitions;

        public abstract GameStateType StateType { get; }
        public bool AllowMoving => _allowMoving;

        [Inject]
        public abstract void Initialize(GameStateController stateController, CoroutineRunner runner);

        public abstract void Enter();
        public abstract void Exit();

        public bool AllowTransitionTo(GameStateType newStateType)
        {
            if (_allowedTransitions.Length == 0)
                return false;

            foreach (var stateType in _allowedTransitions)
                if (stateType == newStateType)
                    return true;

            return false;
        }
    }
}