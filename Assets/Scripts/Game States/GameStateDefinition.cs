using Sound;
using UnityEngine;
using VFX;

namespace GameStates
{
    public abstract class GameStateDefinition : ScriptableObject
    {
        [Tooltip("Identifier for this state type")]
        [SerializeField] private GameStateType _stateType;
        [Tooltip("Whether player input is enabled during this state")]
        [SerializeField] private bool _enableInput = true;
        [Tooltip("Whether the cursor is visible during this state")]
        [SerializeField] private bool _isCursorVisible = false;
        [Tooltip("Whether the world is in moving mode during this state")]
        [SerializeField] private bool _isMovingState = false;
        [Tooltip("Whether units should be killed/cleared when entering this state")]
        [SerializeField] private bool _killUnits = false;
        [Tooltip("Optional camera state to apply when entering this game state")]
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

        public void PlayStateSound(SoundUnit unit, bool isLoop, GameState state)
        {
            state.CurrentSource = state.AudioSourcePool.GetSource();
            unit.Play(state.CurrentSource, isLoop);
        }

        public void StopSound(GameState state)
        {
            if (state.CurrentSource == null)
                return;

            state.CurrentSource.Stop();
        }

        protected void PlayVisualEffect(ParticleEffectInfo info, GameState state)
        {
            if (info.Type == ParticleType.None)
                return;

            state.ParticlePlayer.Play(info.Type, info.Position, info.Scale);
        }
    }
}

