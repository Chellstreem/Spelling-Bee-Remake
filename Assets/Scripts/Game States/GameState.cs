using Sound;
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

        public GameState(GameStateDefinition definition) => Definition = definition;

        [Inject]
        public void Construct(GameStateController stateController, AudioSourcePool audioSourcePool,
         ParticlePlayer particlePlayer, CoroutineRunner runner)
        {
            StateController = stateController;
            AudioSourcePool = audioSourcePool;
            ParticlePlayer = particlePlayer;
            Runner = runner;
        }

        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);
        public bool AllowTransitionTo(GameStateType newStateType) => Definition.AllowTransitionTo(newStateType);

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
