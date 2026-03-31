using UnityEngine;
using VFX;

namespace GameStates
{
    public abstract class GameStateDefinition : ScriptableObject
    {
        [SerializeField] private GameStateType _stateType;
        [SerializeField] private bool _isMovingState = false;
        [SerializeField] private bool _killUnits = false;

        [Tooltip("States that can be transitioned to from this state")]
        [SerializeField] private GameStateType[] _allowedTransitions;

        public GameStateType StateType => _stateType;
        public bool IsMovingState => _isMovingState;
        public bool KillUnits => _killUnits;

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

        public virtual GameState CreateGameState(GameStateContext context) => new(this, context);

        protected void PlayVisualEffect(ParticleEffectInfo info, GameState state)
        {
            if (info.Type == ParticleType.None)
                return;

            state.ParticlePlayer.Play(info.Type, info.Position, info.Scale);
        }
    }
}
