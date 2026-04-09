using System.Collections;
using Spawn;
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

        [Header("Spawn Settings")]
        [SerializeField] protected SpawnFlowInfo[] _spawnFlowInfos;

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

        public virtual IEnumerator SpawnCoroutine(UnitSpawner spawner, GameSpeedController speedController)
        {
            float[] timers = new float[_spawnFlowInfos.Length];

            for (int i = 0; i < timers.Length; i++)
                timers[i] = -_spawnFlowInfos[i].SpawnDelay;

            while (true)
            {
                float deltaTime = Time.deltaTime;

                for (int i = 0; i < _spawnFlowInfos.Length; i++)
                {
                    timers[i] += deltaTime;

                    if (timers[i] < 0f)
                        continue;

                    float interval = _spawnFlowInfos[i].SpawnDistance / speedController.CurrentSpeed;

                    if (timers[i] >= interval)
                    {
                        var binder = GetRandomType(_spawnFlowInfos[i]);

                        if (Random.value <= binder.SpawnProbabilty)
                            spawner.SpawnObject(binder.Type);

                        timers[i] -= interval;
                    }
                }

                yield return null;
            }
        }

        private SpawnBinder GetRandomType(SpawnFlowInfo flow)
        {
            float randomValue = Random.value;

            var binders = flow.Binders;

            for (int i = 0; i < binders.Length; i++)
            {
                randomValue -= binders[i].Weight;

                if (randomValue <= 0f)
                    return binders[i];
            }

            return binders[^1];
        }

        private void OnValidate()
        {
            if (_spawnFlowInfos == null)
                return;

            foreach (var flow in _spawnFlowInfos)
            {
                float sum = 0f;

                foreach (var binder in flow.Binders)
                    sum += binder.Weight;

                if (!Mathf.Approximately(sum, 1f))
                    Debug.LogWarning($"Spawn Flow weight sum is {sum}, but should be 1", this);
            }
        }
    }
}

