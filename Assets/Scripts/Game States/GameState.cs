using UnityEngine;
using VFX;

namespace GameStates
{
    public class GameState
    {
        public GameStateDefinition Definition { get; }
        public GameStateController StateController { get; }
        public CoroutineRunner Runner { get; }
        public AudioSource AudioSource { get; }
        public ParticlePlayer ParticlePlayer { get; }
        public Coroutine StateCoroutine { get; set; }


        public GameState(GameStateDefinition definition, GameStateContext context)
        {
            Definition = definition;
            StateController = context.StateController;
            Runner = context.Runner;
            AudioSource = context.AudioSourcePool.GetSource();
            ParticlePlayer = context.ParticlePlayer;
        }

        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);

        public bool AllowTransitionTo(GameStateType newStateType) => Definition.AllowTransitionTo(newStateType);
    }
}
