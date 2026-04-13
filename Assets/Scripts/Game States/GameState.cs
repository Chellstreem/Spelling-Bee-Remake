using Sound;
using UnityEngine;
using VFX;
using Zenject;

namespace GameStates
{
    public class GameState
    {
        public GameStateDefinition Definition { get; }
        public CoroutineRunner Runner { get; private set; }
        public GameStateController StateController { get; private set; }
        public AudioSourcePool AudioSourcePool { get; private set; }
        public ParticlePlayer ParticlePlayer { get; private set; }

        public Coroutine StateCoroutine { get; set; }
        public AudioSource CurrentSource { get; set; }

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
    }
}
