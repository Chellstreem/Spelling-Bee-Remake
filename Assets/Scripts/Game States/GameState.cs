using Sound;
using Spawn;
using UnityEngine;
using VFX;
using Zenject;

namespace GameStates
{
    public class GameState
    {
        private AudioSource _currentSource;
        public GameStateDefinition Definition { get; }
        public GameStateController StateController { get; private set; }
        public AudioSourcePool AudioSourcePool { get; private set; }
        public ParticlePlayer ParticlePlayer { get; private set; }
        public CoroutineRunner Runner { get; private set; }
        public Coroutine StateCoroutine { get; set; }
        public UnitSpawner UnitSpawner { get; private set; }
        public GameSpeedController SpeedController { get; private set; }
        private Coroutine _spawnCoroutine;

        public GameState(GameStateDefinition definition) => Definition = definition;

        [Inject]
        public void Construct(GameStateController stateController, AudioSourcePool audioSourcePool,
         ParticlePlayer particlePlayer, CoroutineRunner runner, UnitSpawner spawner, GameSpeedController speedController)
        {
            StateController = stateController;
            AudioSourcePool = audioSourcePool;
            ParticlePlayer = particlePlayer;
            Runner = runner;
            UnitSpawner = spawner;
            SpeedController = speedController;
        }

        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);
        public bool AllowTransitionTo(GameStateType newStateType) => Definition.AllowTransitionTo(newStateType);
        public void StartSpawning() => _spawnCoroutine = Runner.Run(Definition.SpawnCoroutine(UnitSpawner, SpeedController));

        public void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                Runner.Stop(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        public void PlaySound(SoundUnit unit, bool isLoop)
        {
            _currentSource = AudioSourcePool.GetSource();
            unit.Play(_currentSource, isLoop);
        }

        public void StopSound()
        {
            if (_currentSource == null)
                return;

            _currentSource.Stop();
        }
    }
}
