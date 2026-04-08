using Sound;
using UnityEngine;
using VFX;

namespace GameStates
{
    public abstract class GameStateDefinition : ScriptableObject
    {
        [SerializeField] private GameStateType _stateType;
        [SerializeField] private bool _enableInput = true;
        [SerializeField] private bool _isCursorVisible = false;
        [SerializeField] private bool _isMovingState = false;
        [SerializeField] private bool _killUnits = false;
        [SerializeField] private CameraState _cameraState;

        [Tooltip("States that can be transitioned to from this state")]
        [SerializeField] private GameStateType[] _allowedTransitions;

        public GameStateType StateType => _stateType;
        public bool EnableInput => _enableInput;
        public bool IsCursorVisible => _isCursorVisible;
        public bool IsMovingState => _isMovingState;
        public bool KillUnits => _killUnits;
        public CameraState CameraState => _cameraState;

        public abstract void Enter(GameState state);
        public abstract void Exit(GameState state);

        public virtual GameState CreateGameState() => new(this);

        public bool AllowTransitionTo(GameStateType newStateType)
        {
            if (_allowedTransitions.Length == 0)
                return false;

            foreach (var stateType in _allowedTransitions)
                if (stateType == newStateType)
                    return true;

            return false;
        }

        protected void PlayVisualEffect(ParticleEffectInfo info, GameState state)
        {
            if (info.Type == ParticleType.None)
                return;

            state.ParticlePlayer.Play(info.Type, info.Position, info.Scale);
        }
    }
}
