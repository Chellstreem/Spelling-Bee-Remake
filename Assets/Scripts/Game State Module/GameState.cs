using System.Threading;
using SoundModule;
using UnityEngine;
using VFXModule;
using Zenject;

namespace GameStateModule
{
    public class GameState
    {
        public GameStateDefinition Definition { get; }
        public GameStateController StateController { get; private set; }
        public AudioSourcePool AudioSourcePool { get; private set; }
        public ParticlePlayer ParticlePlayer { get; private set; }

        public CancellationTokenSource StateCTS { get; set; }
        public AudioSource CurrentSource { get; set; }

        public GameState(GameStateDefinition definition) => Definition = definition;

        [Inject]
        public void Construct(GameStateController stateController, AudioSourcePool audioSourcePool,
         ParticlePlayer particlePlayer)
        {
            StateController = stateController;
            AudioSourcePool = audioSourcePool;
            ParticlePlayer = particlePlayer;
        }

        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);
        public bool AllowTransitionTo(GameStateType newStateType) => Definition.AllowTransitionTo(newStateType);
    }
}
